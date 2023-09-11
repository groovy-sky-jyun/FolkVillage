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
    [Header("�̵��ӵ� ����")]
    [SerializeField][Range(1f, 30f)] float moveSpeed = 20f;
   // Vector3 curPos;

    void Awake()
    {
        anim = GetComponent<Animator>();
        nickname.text = PV.IsMine ? PhotonNetwork.NickName : PV.Owner.NickName;

        if (PV.IsMine)
        {
            // 2D ī�޶�
            var CM = GameObject.Find("PlayerCamera").GetComponent<CinemachineVirtualCamera>();
            CM.Follow = transform;
            CM.LookAt = transform;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        //  if (PV.IsMine && !input.isFocused)//inputfield�� ��Ŀ�� ������������ ĳ���� ������ ����
        if (PV.IsMine)
        {
            //Move Value
            moveX = Input.GetAxisRaw("Horizontal") * moveSpeed;
            moveY = Input.GetAxisRaw("Vertical") * (moveSpeed - 0.5f);

            //Animation
            if (moveY != 0)
            {
                //shift key -> �ӵ� ����
                if (Input.GetButton("Run")) moveY *= 1.4f;
                anim.SetBool("Walk", false);
                anim.SetBool("Climb", true);
                anim.SetBool("Run", false);
            }
            else if (moveX != 0)
            {
                //�¿���� ����ȭ-->���� isMine�� ���� RPC�� ���� �ٸ� PC�� ĳ���͵� ���� �ش� �Լ� ����
                PV.RPC("FlipXRPC", RpcTarget.AllBuffered, moveX);

                if (Input.GetButton("Run"))
                {   //shift key -> �ӵ� ����
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
            //Debug.Log(Camera.main.WorldToScreenPoint(transform.position)); //��ǥ���
          
        }
        
        /* IsMine�� �ƴ� �͵��� �ε巴�� ��ġ ����ȭ
        else if ((transform.position - curPos).sqrMagnitude >= 100) transform.position = curPos;
        else transform.position = Vector3.Lerp(transform.position, curPos, Time.deltaTime * 10);*/
    }

    //�¿���� ����ȭ
    [PunRPC]
    void FlipXRPC(float axis)
    {
        if (axis < 0)
            SR.flipX = true;
        else SR.flipX = false;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
       /* if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
        }
        else
        {
            curPos = (Vector3)stream.ReceiveNext();
        }*/
    }
}
   

   