using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChractorMove : MonoBehaviour
{   
    float moveX, moveY;

    [Header("이동속도 조절")]
    [SerializeField]    [Range(1f,30f)] float moveSpeed=20f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveX=Input.GetAxis("Horizontal") * moveSpeed* Time.deltaTime;
        moveY=Input.GetAxis("Vertical")*moveSpeed* Time.deltaTime;

        transform.position = new Vector2(transform.position.x+moveX, transform.position.y+moveY);
    }


}
