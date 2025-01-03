using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateHaveJSON : MonoBehaviour
{
    public GameObject itemListCS;

    private void Awake()
    {
        itemListCS.GetComponent<HouseInventoryJSON>().LoadPlayerDataFromJson();
    }
    public void PerchaseHaveUpdate(int type, int num)
    {
        //아이템 구매하면 have=true로 변경
        HouseInventoryData inventoryItem = itemListCS.GetComponent<HouseInventoryJSON>().inventoryItem;
        if (type == 0)//가구
        {
            inventoryItem.furnitureItemList[num].have = true;
        }
        else if(type == 1)
        {
            inventoryItem.wallItemList[num].have = true;
        }
        else if (type == 2)
        {
            inventoryItem.floorItemList[num].have = true;
        }
        //JSON파일 저장
        itemListCS.GetComponent<HouseInventoryJSON>().SavePlayerDataToJson();
        itemListCS.GetComponent<HouseInventoryJSON>().LoadPlayerDataFromJson();
    }
}
