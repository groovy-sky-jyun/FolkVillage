using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class PurchaseItemBtn : MonoBehaviour
{
    public GameObject block_canvas;
    public GameObject obj_coin;
    public int type;
    public int num;
    public void PurchaseBtnClick()
    {
        string coin = obj_coin.GetComponent<Text>().text;
        GameObject obj = GameObject.Find("CoinSettingCS");
        //코인 차감 
        obj.GetComponent<CoinSetting>().minusCoin(int.Parse(coin));
        //화면 어둡게 하기
        block_canvas.SetActive(true);
        //JSON파일 have update하기
        GameObject have_obj = GameObject.Find("UpdateJSON");
        have_obj.GetComponent<UpdateHaveJSON>().PerchaseHaveUpdate(type, num);
    }
}
