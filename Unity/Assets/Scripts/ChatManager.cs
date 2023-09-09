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
    public Text chatLog;
    public InputField input;
    private ScrollRect scrollRect; //��ũ�ѹ�
    void Start()
    {
        PhotonNetwork.IsMessageQueueRunning = true;
       scrollRect = GameObject.Find("Canvas/WorldChatting/WorldScrollView").GetComponent<ScrollRect>();
    }
    public void SendButtonOnClicked()
    {
        if (input.text.Equals("")) { Debug.Log("Empty"); return; }
        //msg: "[�г���] ä�ø޽���" ���·� scroll view�� ���
        //{0}:player �г���, {1}:����� inputfield.text
 
        string msg = string.Format("[{0}]{1}", PhotonNetwork.LocalPlayer.NickName, input.text);
        
        //RPC: Remote Procedure Call_������� ���� �Լ��� ���ν����� ������ ���
        //���� world�� �������� �ٸ� �������� ReceiveMsg �޼ҵ带 �����ϵ��� �Ѵ�.
        //->�뿡 ��� ����鿡�� �ش� �Լ��� ����ǰ�, Ư���۾��� �����ϸ� ��� ����鿡�� �ݿ��ȴ�.
       photonView.RPC("ReceiveMsg", RpcTarget.OthersBuffered, msg);
       
        //���ο��Ե� �޽����� �������� �Լ� ����
        ReceiveMsg(msg);
    }

    [PunRPC]
    public void ReceiveMsg(string msg)
    {
        //ä�� �α�text�� ���� ���
        chatLog.text += msg+ "\n";
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
