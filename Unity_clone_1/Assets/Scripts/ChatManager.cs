using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using UnityEngine.UI;


public class ChatManager : MonoBehaviourPunCallbacks
{
    public List<string> chatList = new List<string>();
    public Button sendBtn;
    public Text WchatLog;
    public Text NchatLog;
    public InputField input;
    private ScrollRect scrollRect; //��ũ�ѹ�
    private bool worldValue;

    void Start()
    {
        PhotonNetwork.IsMessageQueueRunning = true;
       scrollRect = GameObject.Find("Canvas/WorldChatting/WorldScrollView").GetComponent<ScrollRect>();
        worldValue = true;//�ʱ� ��üä�� ���
    }
    public void SendButtonOnClicked()
    {
        if (input.text.Equals("")) { Debug.Log("Empty"); return; }
        
        //msg: "[�г���] ä�ø޽���" ���·� scroll view�� ���
        //{0}:player �г���, {1}:����� inputfield.text
        string msg = string.Format("[{0}]{1}", PhotonNetwork.LocalPlayer.NickName, input.text);
        photonView.RPC("ReceiveMsg", RpcTarget.OthersBuffered, msg);

        //���ο��Ե� �޽����� �������� �Լ� ����
        ReceiveMsg(msg);
    }
    public void WorldChatOnClicked()
    {
        if(!worldValue)//�Ϲ�ä�ø���ϰ�� -> ��üä������ ����
        {
            worldValue = true;
            input.text = "";   //inputfield �ʱ�ȭ
        }
    }
    public void NomalChatOnClicked()
    {
        if (worldValue)//��üä�ø���ϰ�� -> �Ϲ�ä������ ����
        {
            worldValue = false;
            input.text = "";   //inputfield �ʱ�ȭ
        }
    }
    [PunRPC]
    public void ReceiveMsg(string msg)
    {
        //Log�� ���
        if(worldValue) { WchatLog.text += msg + "\n"; }
        else { NchatLog.text += msg + "\n";; }

        //inputfield �ʱ�ȭ
        input.text = "";
        //�޽��� ���� �� �ٷ� �޽��� �Է��� ���ֵ��� ��Ŀ���� InputField�� �ű�
        input.ActivateInputField();

        //scroll bar�� ��ġ�� ���� �Ʒ��� ����
        //1.0f �̸� ���� ���� ����
        scrollRect.verticalNormalizedPosition = 0.0f;
    }

    void Update()
    {
      //GetKeyUp: Ű�� ������ ���� ��, �ѹ� true ��ȯ
      //GetKeyDown: Ű�� ������ ��, �� �ѹ� true ��ȯ
      //input.isFocused�� true�� �ϸ� ���������� ���۾ȵ�
      //KeyCode.Return�� Enter Key�� ���Ѵ�.
        if (Input.GetKeyDown(KeyCode.Return) && !input.isFocused) SendButtonOnClicked();
    }
 
    
}
