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
        //������ �����ϸ� have=true�� ����
        HouseInventoryData inventoryItem = itemListCS.GetComponent<HouseInventoryJSON>().inventoryItem;
        if (type == 0)//����
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
        //JSON���� ����
        itemListCS.GetComponent<HouseInventoryJSON>().SavePlayerDataToJson();
        itemListCS.GetComponent<HouseInventoryJSON>().LoadPlayerDataFromJson();
    }
}
