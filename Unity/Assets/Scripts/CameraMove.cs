using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public GameObject A;
    Transform AT;
    void Start()
    {
        AT = A.transform;
    }
    void Update()
    {
        //transform.position = Vector2.Lerp(transform.position, AT.position, 10f * Time.deltaTime);
        transform.position = Vector2.Lerp(transform.position, AT.position, 20f * Time.deltaTime);
        transform.Translate(0, 0, -10); //ī�޶� ���� z������ �̵�
    }
}
