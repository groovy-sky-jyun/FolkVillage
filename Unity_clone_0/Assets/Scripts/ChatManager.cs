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
    private ScrollRect scrollRect; //스크롤바
    private bool worldValue;
    private string msg = "";
    //public GameObject player;
    private string msgText = "";


    void Awake()

    { 
    }
    void Start()
    {
        PhotonNetwork.IsMessageQueueRunning = true;
        scrollRect = GameObject.Find("Canvas/WorldChatting/WorldScrollView").GetComponent<ScrollRect>();
        worldValue = true;//초기 전체채팅 모드
                          //player 포톤뷰 가져오기
                          //-> 포톤뷰 is mine

       // player.transform.GetComponent<PlayerMove>().BubbleChatOff();
        //transform.GetChild(2).GetChild(0).gameObject.SetActive(false);
        // bubbleText = player.transform.Find("BubbleCanvas/Image/speechBubble").GetComponent<Text>();





    }
    public void SendButtonOnClicked()
    {
    
        /*if (bubble.activeSelf)
                 Debug.Log("setactive true");
             else
                 Debug.Log("setactive false");
         */

        if (input.text.Equals("")) { Debug.Log("Empty"); return; }
        //bubble.enabled = true;
        //msg: "[닉네임] 채팅메시지" 형태로 scroll view에 띄움
        //{0}:player 닉네임, {1}:사용자 inputfield.text
        msg = string.Format("[{0}]{1}", PhotonNetwork.LocalPlayer.NickName, input.text);
        msgText = input.text;
        if (worldValue)//전체채팅 모드
        {
            photonView.RPC("WorldReceiveMsg", RpcTarget.OthersBuffered, msg);
            //본인에게도 메시지를 띄우기위해 함수 실행
            WorldReceiveMsg(msg);
        }
        else//일반채팅 모드
        {
            photonView.RPC("NomalReceiveMsg", RpcTarget.OthersBuffered, msg);
            //본인에게도 메시지를 띄우기위해 함수 실행
            NomalReceiveMsg(msg);
           
        }
        //inputfield 초기화
        input.text = "";
        //메시지 전송 후 바로 메시지 입력할 수있도록 포커스를 InputField로 옮김
        input.ActivateInputField();

        //scroll bar의 위치를 제일 아래로 지정
        //1.0f 이면 제일 위로 지정
        scrollRect.verticalNormalizedPosition = 0.0f;
    }
    [PunRPC]
    public void WorldReceiveMsg(string msg)
    {//Log에 출력
       WchatLog.text += msg + "\n";
    }
    [PunRPC]
    public string NomalReceiveMsg(string msg)
    {
        NchatLog.text += msg + "\n";
        return msg;
    }
    public string isNomal()
    {
        if (!worldValue && msgText != "")
        {
            return msgText;
        }
        else return null;
    }
    

   
    public void WorldChatOnClicked()
    {
        if(!worldValue)//일반채팅모드일경우 -> 전체채팅으로 변경
        {
            worldValue = true;
            input.text = "";   //inputfield 초기화
            msgText = "";
        }
    }
    public void NomalChatOnClicked()
    {
        if (worldValue)//전체채팅모드일경우 -> 일반채팅으로 변경
        {
            worldValue = false;
            input.text = "";   //inputfield 초기화
            msgText = "";
        }
    }
   
   
    void Update()
    {
      //GetKeyUp: 키를 눌렀다 땠을 때, 한번 true 반환
      //GetKeyDown: 키를 눌렀을 때, 딱 한번 true 반환
      //input.isFocused를 true로 하면 엔터했을때 전송안됨
      //KeyCode.Return은 Enter Key를 뜻한다.
        if (Input.GetKeyDown(KeyCode.Return) && !input.isFocused) SendButtonOnClicked();
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
