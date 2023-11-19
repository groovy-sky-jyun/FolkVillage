using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseItemClick : MonoBehaviour
{
    public GameObject wall1;
    public GameObject wall2;
    public GameObject floor1;
    public GameObject floor2;
    public GameObject floor3;

    public GameObject itemListCS;
    

    //item type에 따라 함수 실행
    public void ClickItem(int type, int num)
    {
        if (type == 1) {
            ClickWallItem(num);
        }
        else if(type == 2)
        {
            ClickFloorItem(num);
        }
    }
    //type이 벽지일 경우 벽지 아이템 변경
    public void ClickWallItem(int num)
    {
        //프리팹마다 객체가 다르게 생성되기 떄문에 공유가 불가능
        HouseInventoryData inventoryItem = itemListCS.GetComponent<HouseInventoryJSON>().inventoryItem;
        //아이템 누르면 use_check 변경
        for (int i=0;i < inventoryItem.wallItemList.Length; i++)
        {
            if (i == num)
            {
                inventoryItem.wallItemList[num].use_check = true;
            }
            else
            {
                inventoryItem.wallItemList[i].use_check = false;
            }
        }
        //아이템 누를때마다 변경값 저장
        itemListCS.GetComponent<HouseInventoryJSON>().SavePlayerDataToJson();

        //벽지 오브젝트 전환
        if (num == 0)
        {
            wall1.SetActive(true);
            wall2.SetActive(false);
        }
        else if(num == 1) 
        {
            wall1.SetActive(false);
            wall2.SetActive(true);
        }
        
    }
    public void ClickFloorItem(int num)
    {
        //프리팹마다 객체가 다르게 생성되기 떄문에 공유가 불가능
        HouseInventoryData inventoryItem = itemListCS.GetComponent<HouseInventoryJSON>().inventoryItem;
        //아이템 누르면 use_check 변경
        for (int i = 0; i < inventoryItem.floorItemList.Length; i++)
        {
            if (i == num)
            {
                inventoryItem.floorItemList[num].use_check = true;
            }
            else
            {
                inventoryItem.floorItemList[i].use_check = false;
            }
        }
        //아이템 누를때마다 변경값 저장
        itemListCS.GetComponent<HouseInventoryJSON>().SavePlayerDataToJson();

        //바닥 오브젝트 전환
        if (num == 0)
        {
            floor1.SetActive(true);
            floor2.SetActive(false);
            floor3.SetActive(false);
        }
        else if (num == 1)
        {
            floor1.SetActive(false);
            floor2.SetActive(true);
            floor3.SetActive(false);
        }
        else if (num == 2)
        {
            floor1.SetActive(false);
            floor2.SetActive(false);
            floor3.SetActive(true);
        }
    }

}
