using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Photon.Voice.AudioUtil;

public class HouseItemPrefabClick : MonoBehaviour
{
    public int type;
    public int num;
   public void ItemClick()
    {
        GameObject itemCS = GameObject.Find("ItemClickCS");
        itemCS.GetComponent<HouseItemClick>().ClickItem(type, num);
    }
}
