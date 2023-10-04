using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using Cinemachine;


public class PlayerMove : MonoBehaviourPunCallbacks, IPunObservable
{
    float moveX, moveY;
//    public InputField input;
    public Canvas canvas;
    public Text nickname;
    Animator anim;
    public PhotonView PV;
    public SpriteRenderer SR;
    public Rigidbody2D RB;
    public Text chatText;
    [Header("이동속도 조절")]
    [SerializeField][Range(1f, 30f)] float moveSpeed = 20f;
    private GameObject chatManager;
    private GameObject sendTrigger;
    string msg;
    GameObject[]  playerGroup;

    // Vector3 curPos;

    void Awake()
    {
        anim = GetComponent<Animator>();
        nickname.text = PV.IsMine ? PhotonNetwork.NickName : PV.Owner.NickName;

        if (PV.IsMine)
        {
            // 2D 카메라
            var CM = GameObject.Find("PlayerCameraCS").GetComponent<CinemachineVirtualCamera>();
            CM.Follow = transform;
            CM.LookAt = transform;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        chatManager = GameObject.Find("ChatManagerCS");
        sendTrigger = GameObject.Find("SendBtnTriggerCS");
        transform.GetChild(2).GetChild(0).gameObject.SetActive(false);

        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
           // Debug.Log(PhotonNetwork.PlayerList[i].ActorNumber);
            //Debug.Log("포톤 네트워크 테스트: ");
        }
        /*if (PV.IsMine)
        {
            //AudioListener.volum = 1; //볼륨키워주기
            //Debug.Log(PhotonNetwork.LocalPlayer.ActorNumber);
        }
        else
        {
            GetComponentInChildren<AudioListener>().enabled = false;
        }*/
    }
    // Update is called once per frame
    void Update()
    {
        
        //  if (PV.IsMine && !input.isFocused)//inputfield에 포커스 맞춰져있으면 캐릭터 움직임 멈춤
        if (PV.IsMine&& !chatManager.GetComponent<ChatManager>().IsFocusedInput())
        {
            //Move Value
            moveX = Input.GetAxisRaw("Horizontal") * moveSpeed;
            moveY = Input.GetAxisRaw("Vertical") * (moveSpeed - 0.5f);

            //Animation
            if (moveY != 0)
            {
                //shift key -> 속도 증가
                if (Input.GetButton("Run")) moveY *= 1.4f;
                anim.SetBool("Walk", false);
                anim.SetBool("Climb", true);
                anim.SetBool("Run", false);
            }
            else if (moveX != 0)
            {
                //좌우반전 동기화-->현재 isMine과 같은 RPC를 가진 다른 PC의 캐릭터들 전부 해당 함수 실행
                PV.RPC("FlipXRPC", RpcTarget.AllBuffered, moveX);

                if (Input.GetButton("Run"))
                {   //shift key -> 속도 증가
                    moveX *= 1.5f;
                    anim.SetBool("Walk", false);
                    anim.SetBool("Climb", false);
                    anim.SetBool("Run", true);
                   
                }
                else
                {
                    anim.SetBool("Walk", true);
                    anim.SetBool("Climb", false);
                    anim.SetBool("Run", false);
                }
            }
            else
            {
                anim.SetBool("Walk", false);
                anim.SetBool("Climb", false);
                anim.SetBool("Run", false);
            }

            RB.velocity = new Vector2(moveX, moveY);
            //Debug.Log(Camera.main.WorldToScreenPoint(transform.position)); //좌표찍기
           if (Input.GetKeyDown(KeyCode.Return)|| sendTrigger.GetComponent<SendBtnTrigger>().OnPreCull())
           {
                msg = chatManager.GetComponent<ChatManager>().isNomal();
                //Debug.Log("enter key press");
                if(msg != null)
                {
                    PV.RPC("BubbleChatOn", RpcTarget.AllBuffered);
                }
           }
           playerGroup = GameObject.FindGameObjectsWithTag("Player");
            int localNum = PhotonNetwork.LocalPlayer.ActorNumber;
            for (int i = 0; i < playerGroup.Length; i++)
            {
                int num = PhotonNetwork.PlayerList[i].ActorNumber;
                if ( num!= localNum)
                {
                    float distance = Vector2.Distance(transform.position, playerGroup[i].transform.position);
                    //Debug.Log("distance("+ localNum + ", "+ num + "): " + distance);
                    if (distance > 4f)
                    {
                        playerGroup[i].transform.GetChild(2).GetChild(0).gameObject.SetActive(false);
                        Debug.Log("distance > 4 ");
                    }
                }
            }
            
        }
        /* IsMine이 아닌 것들은 부드럽게 위치 동기화
        else if ((transform.position - curPos).sqrMagnitude >= 100) transform.position = curPos;
        else transform.position = Vector3.Lerp(transform.position, curPos, Time.deltaTime * 10);*/
    }
    
    //좌우반전 동기화
    [PunRPC]
    void FlipXRPC(float axis)
    {
        if (axis < 0)
            SR.flipX = true;
        else SR.flipX = false;
    }
    IEnumerator DelayCloseChatBox(float t)
    {
        yield return new WaitForSeconds(t);
        photonView.RPC("BubbleChatOff", RpcTarget.AllBuffered);
    }
    [PunRPC]
    public void BubbleChatOn()
    {
        StopAllCoroutines();
        Debug.Log("bubble chat set active true");
        chatText.text = msg;
        transform.GetChild(2).GetChild(0).gameObject.SetActive(true);
        StartCoroutine(DelayCloseChatBox(3.0f));
    }
    [PunRPC]
    public void BubbleChatOff()
    {
        Debug.Log("BubbleChatOff 실행");
        transform.GetChild(2).GetChild(0).gameObject.SetActive(false);
        
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(chatText.text);
        }
        else
        {
            chatText.text = (string)stream.ReceiveNext();
        }
    }
}
   

   