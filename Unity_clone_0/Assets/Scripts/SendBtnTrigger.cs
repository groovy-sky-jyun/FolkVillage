using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendBtnTrigger : MonoBehaviour
{
    bool value;

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool OnPreCull()
    {
        return value;
    }
    public void PointerDown()
    {
        value=true;
    }

    public void PointerUp()
    {
        value= false;
    }
}
