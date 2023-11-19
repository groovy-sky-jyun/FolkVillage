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
    

    //item type�� ���� �Լ� ����
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
    //type�� ������ ��� ���� ������ ����
    public void ClickWallItem(int num)
    {
        //�����ո��� ��ü�� �ٸ��� �����Ǳ� ������ ������ �Ұ���
        HouseInventoryData inventoryItem = itemListCS.GetComponent<HouseInventoryJSON>().inventoryItem;
        //������ ������ use_check ����
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
        //������ ���������� ���氪 ����
        itemListCS.GetComponent<HouseInventoryJSON>().SavePlayerDataToJson();

        //���� ������Ʈ ��ȯ
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
        //�����ո��� ��ü�� �ٸ��� �����Ǳ� ������ ������ �Ұ���
        HouseInventoryData inventoryItem = itemListCS.GetComponent<HouseInventoryJSON>().inventoryItem;
        //������ ������ use_check ����
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
        //������ ���������� ���氪 ����
        itemListCS.GetComponent<HouseInventoryJSON>().SavePlayerDataToJson();

        //�ٴ� ������Ʈ ��ȯ
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
