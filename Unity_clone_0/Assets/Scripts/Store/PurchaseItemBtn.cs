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
        //���� ���� 
        obj.GetComponent<CoinSetting>().minusCoin(int.Parse(coin));
        //ȭ�� ��Ӱ� �ϱ�
        block_canvas.SetActive(true);
        //JSON���� have update�ϱ�
        GameObject have_obj = GameObject.Find("UpdateJSON");
        have_obj.GetComponent<UpdateHaveJSON>().PerchaseHaveUpdate(type, num);
    }
}
