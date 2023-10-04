using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PhoneTimeSet : MonoBehaviour
{
    public Text time;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time.text= DateTime.Now.ToString(("HH : mm"));
    }
}
