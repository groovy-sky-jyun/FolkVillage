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

    [Header("이동속도 조절")]
    [SerializeField]    [Range(1f,30f)] float moveSpeed=20f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        if (!input.isFocused)//inputfield에 포커스 맞춰져있으면 캐릭터 움직임 멈춤
        {

            moveX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
            moveY = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

            transform.position = new Vector2(transform.position.x + moveX, transform.position.y + moveY);
            //Debug.Log(Camera.main.WorldToScreenPoint(transform.position));
            
            nickname.GetComponent<RectTransform>().anchoredPosition = new Vector2(Camera.main.WorldToScreenPoint(transform.position).x-970, Camera.main.WorldToScreenPoint(transform.position).y - 440);

        }
    }


}
