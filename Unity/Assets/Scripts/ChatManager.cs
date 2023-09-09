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
    private ScrollRect scrollRect; //스크롤바
    void Start()
    {
        PhotonNetwork.IsMessageQueueRunning = true;
       scrollRect = GameObject.Find("Canvas/WorldChatting/WorldScrollView").GetComponent<ScrollRect>();
    }
    public void SendButtonOnClicked()
    {
        if (input.text.Equals("")) { Debug.Log("Empty"); return; }
        //msg: "[닉네임] 채팅메시지" 형태로 scroll view에 띄움
        //{0}:player 닉네임, {1}:사용자 inputfield.text
 
        string msg = string.Format("[{0}]{1}", PhotonNetwork.LocalPlayer.NickName, input.text);
        
        //RPC: Remote Procedure Call_원격제어를 통해 함수나 프로시저의 실행을 허용
        //같은 world에 참여중인 다른 유저들이 ReceiveMsg 메소드를 실행하도록 한다.
        //->룸에 모든 사람들에게 해당 함수가 실행되고, 특정작업을 실행하면 모든 사람들에게 반영된다.
       photonView.RPC("ReceiveMsg", RpcTarget.OthersBuffered, msg);
       
        //본인에게도 메시지를 띄우기위해 함수 실행
        ReceiveMsg(msg);
    }

    [PunRPC]
    public void ReceiveMsg(string msg)
    {
        //채팅 로그text에 문자 출력
        chatLog.text += msg+ "\n";
        //inputfield 초기화
        input.text = "";
        //메시지 전송 후 바로 메시지 입력할 수있도록 포커스를 InputField로 옮김
        input.ActivateInputField();

        //scroll bar의 위치를 제일 아래로 지정
        //1.0f 이면 제일 위로 지정
        scrollRect.verticalNormalizedPosition = 0.0f;
    }

    void Update()
    {
      //GetKeyUp: 키를 눌렀다 땠을 때, 한번 true 반환
      //GetKeyDown: 키를 눌렀을 때, 딱 한번 true 반환
      //input.isFocused를 true로 하면 엔터했을때 전송안됨
      //KeyCode.Return은 Enter Key를 뜻한다.
        if (Input.GetKeyDown(KeyCode.Return) && !input.isFocused) SendButtonOnClicked();
    }
 
    
}
