using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class CallingSceneChange : MonoBehaviour
{

    public void CallingBtnOnClick()
    {
        //ȭ����ȯ
        GameObject CallObj = GameObject.Find("CallingChangeCS");
        CallObj.GetComponent<CallingSetactiveTrueImg>().SetActiveImg();
        GameObject parentObj = GameObject.Find("PhoneCanvas");
        parentObj.SetActive(false);
    }

    

}
