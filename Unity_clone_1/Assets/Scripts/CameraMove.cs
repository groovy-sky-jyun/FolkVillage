using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    /*
    public Transform target;
    public float lerpSpeed = 1.0f;

    private Vector3 offset;

    private Vector3 targetPos;

    private void Start()
    {
        if (target == null) return;

        offset = transform.position - target.position;
    }

    private void Update()
    {
        if (target == null) return;

        targetPos = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetPos, lerpSpeed * Time.deltaTime);
    }
    */
    
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
        transform.Translate(0, 0, -10); //카메라를 원래 z축으로 이동
    }
}
