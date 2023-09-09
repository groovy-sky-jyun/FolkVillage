using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class ChracterMove : MonoBehaviour
{   
    float moveX, moveY;
    public InputField input;
    public Text nickname;
    Animator anim;
    SpriteRenderer spriteRenderer;
   

    [Header("이동속도 조절")]
    [SerializeField]    [Range(1f,30f)] float moveSpeed=20f;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!input.isFocused)//inputfield에 포커스 맞춰져있으면 캐릭터 움직임 멈춤
        {
            //Move Value
            moveX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
            moveY = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

     //Animation
                if (moveY>0|| moveY < 0)
            {
                spriteRenderer.flipX = false;
                anim.SetBool("Walk", false);
                anim.SetBool("Climb", true);
                anim.SetBool("Run", false);
            }
            else if (Input.GetButton("Run"))
            {
                moveX *= 1.5f;
                if(moveX < 0)//좌우반전 빨리달리기
                {
                    spriteRenderer.flipX = true;
                    anim.SetBool("Walk", false);
                    anim.SetBool("Climb", false);
                    anim.SetBool("Run", true);
                }
                if (moveX > 0)//빨리달리기
                {
                    spriteRenderer.flipX = false;
                    anim.SetBool("Walk", false);
                    anim.SetBool("Climb", false);
                    anim.SetBool("Run", true);
                }
            }
            else if (moveX < 0)
            {
                spriteRenderer.flipX = true;
                anim.SetBool("Walk", true);
                anim.SetBool("Climb", false);
                anim.SetBool("Run", false);
            }
            else if (moveX > 0)
            {
                spriteRenderer.flipX = false;
                anim.SetBool("Walk", true);
                anim.SetBool("Climb", false);
                anim.SetBool("Run", false);
            }
            else
            {
                spriteRenderer.flipX = false;
                anim.SetBool("Walk", false);
                anim.SetBool("Climb", false);
                anim.SetBool("Run", false);
            }


                transform.position = new Vector2(transform.position.x + moveX, transform.position.y + moveY);
            //Debug.Log(Camera.main.WorldToScreenPoint(transform.position));
            
            nickname.GetComponent<RectTransform>().anchoredPosition = new Vector2(Camera.main.WorldToScreenPoint(transform.position).x-965, Camera.main.WorldToScreenPoint(transform.position).y - 485);

        }
    }


}
