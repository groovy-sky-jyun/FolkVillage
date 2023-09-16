using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using UnityEngine.UI;

public class ChatManager : MonoBehaviourPunCallbacks, IPunObservable
{
    //public List<string> chatList = new List<string>();
    public Button sendBtn;
    public Text WchatLog;
    public Text NchatLog;
    public InputField input;
    private ScrollRect scrollRect; //��ũ�ѹ�
    private bool worldValue;
    private string msg = "";
    private string msgText = "";
    private GameObject sendTrigger;
    void Awake()

    { 
    }
    void Start()
    {
        PhotonNetwork.IsMessageQueueRunning = true;
        scrollRect = GameObject.Find("Canvas/WorldChatting/WorldScrollView").GetComponent<ScrollRect>();
        worldValue = true;//�ʱ� ��üä�� ���
        sendTrigger = GameObject.Find("SendBtnTrigger");

    }
    public void SendButtonOnClicked()
    {

        if (input.text.Equals("")) { Debug.Log("Empty"); return; }
        //bubble.enabled = true;
        //msg: "[�г���] ä�ø޽���" ���·� scroll view�� ���
        //{0}:player �г���, {1}:����� inputfield.text
        msg = string.Format("[{0}]{1}", PhotonNetwork.LocalPlayer.NickName, input.text);
        msgText = input.text;
        if (worldValue)//��üä�� ���
        {
            photonView.RPC("WorldReceiveMsg", RpcTarget.OthersBuffered, msg);
            //���ο��Ե� �޽����� �������� �Լ� ����
            WorldReceiveMsg(msg);
        }
        else//�Ϲ�ä�� ���
        {
            photonView.RPC("NomalReceiveMsg", RpcTarget.OthersBuffered, msg);
            //���ο��Ե� �޽����� �������� �Լ� ����
            NomalReceiveMsg(msg);
           
        }
        //inputfield �ʱ�ȭ
        input.text = "";
        //�޽��� ���� �� �ٷ� �޽��� �Է��� ���ֵ��� ��Ŀ���� InputField�� �ű�
        input.ActivateInputField();

        //scroll bar�� ��ġ�� ���� �Ʒ��� ����
        //1.0f �̸� ���� ���� ����
        scrollRect.verticalNormalizedPosition = 0.0f;
    }
    [PunRPC]
    public void WorldReceiveMsg(string msg)
    {//Log�� ���
       WchatLog.text += msg + "\n";
    }
    [PunRPC]
    public string NomalReceiveMsg(string msg)
    {
        NchatLog.text += msg + "\n";
        return msg;
    }
   
    public string isNomal() //PlayerMove.cs --> �Ϲ�ä�� ��忡 �ؽ�Ʈ�� �ԷµǾ����� Ȯ�� �� �׷��ٸ� ���ڿ� ����
    {
        if (!worldValue && msgText != "")
        {
            return msgText;
        }
        else return null;
    }
   
   
    public void WorldChatOnClicked()
    {
        if(!worldValue)//�Ϲ�ä�ø���ϰ�� -> ��üä������ ����
        {
            worldValue = true;
            input.text = "";   //inputfield �ʱ�ȭ
            msgText = "";
        }
    }
    public void NomalChatOnClicked()
    {
        if (worldValue)//��üä�ø���ϰ�� -> �Ϲ�ä������ ����
        {
            worldValue = false;
            input.text = "";   //inputfield �ʱ�ȭ
            msgText = "";
        }
    }

    public bool IsFocusedInput()
    {
        if (input.isFocused)
        {
            return true;
        }
        return false;
    }

    void Update()
    {
      //GetKeyUp: Ű�� ������ ���� ��, �ѹ� true ��ȯ
      //GetKeyDown: Ű�� ������ ��, �� �ѹ� true ��ȯ
      //input.isFocused�� true�� �ϸ� ���������� ���۾ȵ�
      //KeyCode.Return�� Enter Key�� ���Ѵ�.
        if ((Input.GetKeyDown(KeyCode.Return) && !input.isFocused)|| sendTrigger.GetComponent<SendBtnTrigger>().OnPreCull()) SendButtonOnClicked();
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(msg);
        }
        else
        {
            msg = (string)stream.ReceiveNext();
        }
    }
}
