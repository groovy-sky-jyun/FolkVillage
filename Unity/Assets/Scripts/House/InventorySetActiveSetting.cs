using ExitGames.Client.Photon;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventorySetActiveSetting : MonoBehaviour
{
    public GameObject inventoryPanel_1;
    public GameObject inventoryPanel_2;
    public GameObject inventoryPanel_3;
    bool open;
    public GameObject houseItemJson;
    public GameObject itemprefab;
    public GameObject itemClickCS;


    public GameObject prefabParent_furniture;
    public Transform parent_furniture;
    public GameObject prefabParent_wall;
    public Transform parent_wall;
    public GameObject prefabParent_floor;
    public Transform parent_floor;

    public GameObject furniturePrefab;
    public Transform furniturePrefab_parent;
    public RectTransform furniturePrefab_trans;

    public GameObject wall1;
    public GameObject wall2;
    public GameObject floor1;
    public GameObject floor2;
    public GameObject floor3;

    //�ʱ�ȭ
    private void Start()
    {
        FurniturePanelOff();
        open = false;
        //������ ��������
        houseItemJson.GetComponent<HouseInventoryJSON>().LoadPlayerDataFromJson();
        HouseInventoryData inventoryItem = houseItemJson.GetComponent<HouseInventoryJSON>().getHouseItem();

        //use_check�� furniture �⺻ ����
        for (int i = 0; i < inventoryItem.furnitureList.Length; i++)
        {
            if (inventoryItem.furnitureList[i].use_check)
            {
                GameObject furniture_prefab = Instantiate(furniturePrefab, furniturePrefab_parent); // �θ� ����
                furniture_prefab.GetComponent<Image>().sprite = inventoryItem.furnitureList[i].sprite;
                furniture_prefab.transform.localPosition = new Vector3(inventoryItem.furnitureList[i].x, inventoryItem.furnitureList[i].y, 0);
                //furniture_prefab.transform.localScale = new Vector2(2, 2);
                furniture_prefab.GetComponent<RectTransform>().sizeDelta = new Vector2(inventoryItem.furnitureList[i].width, inventoryItem.furnitureList[i].height);
                
            }
        }

        //use_check�� wall �⺻ ����
        for(int i=0;i< inventoryItem.wallItemList.Length;i++)
        {
            if (inventoryItem.wallItemList[i].use_check)
            {
                Debug.Log("wall use_check num: "+inventoryItem.wallItemList[i].num);
                if (inventoryItem.wallItemList[i].num == 0)
                {
                    wall1.SetActive(true);
                    wall2.SetActive(false);
                }
                else if (inventoryItem.wallItemList[i].num == 1)
                {
                    wall1.SetActive(false);
                    wall2.SetActive(true);
                }
            }
        }
        //use_check�� floor �⺻ ����
        for (int i = 0; i < inventoryItem.floorItemList.Length; i++)
        {
            if (inventoryItem.floorItemList[i].use_check)
            {
                Debug.Log("floor use_check num: " + inventoryItem.floorItemList[i].num);
                if (inventoryItem.floorItemList[i].num == 0)
                {
                    floor1.SetActive(true);
                    floor2.SetActive(false);
                    floor3.SetActive(false);
                }
                else if (inventoryItem.floorItemList[i].num == 1)
                {
                    floor1.SetActive(false);
                    floor2.SetActive(true);
                    floor3.SetActive(false);
                }
                else if (inventoryItem.floorItemList[i].num == 2)
                {
                    floor1.SetActive(false);
                    floor2.SetActive(false);
                    floor3.SetActive(true);
                }
            }
        }
    }
    //���� ������(�� �����Ҷ�, ��ư �ѹ� �� ������ Ŀ���� ��� �����Ҷ�)
    private void FurniturePanelOff()
    {
        inventoryPanel_1.SetActive(false);
        inventoryPanel_2.SetActive(false);
        inventoryPanel_3.SetActive(false);
        open = false;
    }
    //ȭ�������� ������ư ������ ��
    public void CustomInventoryBtn()
    {
        if (open)//���� ���¿��� ��ư �ѹ� �� ������ ���� �� ����
        {
            //������ ������� ����
            houseItemJson.GetComponent<HouseInventoryJSON>().SavePlayerDataToJson();

            inventoryPanel_1.SetActive(false);
            inventoryPanel_2.SetActive(false);
            inventoryPanel_3.SetActive(false);
            open = false;
        }
        else
        {
            open = true;
            ClickFurnitureCustomBtn();
        }
    }
    //�κ��丮 ���� �ٹ̱� ��ư ������ ��
    public void ClickFurnitureCustomBtn()
    {
        inventoryPanel_1.SetActive(true);
        inventoryPanel_2.SetActive(false);
        inventoryPanel_3.SetActive(false);
        //���� ������ ����
        ItemDestroy();

        //�������� ���Ǿ��� ���� ������ Ŭ������ �� ���� ������Ʈ ������Ѵ�.
        houseItemJson.GetComponent<HouseInventoryJSON>().LoadPlayerDataFromJson();

        //������ ��������
        HouseInventoryData inventoryItem = houseItemJson.GetComponent<HouseInventoryJSON>().getHouseItem();        

       //�������� �����ϰ� �ִٸ� ������ ����
        for (int i=0;i< inventoryItem.furnitureList.Length; i++)
        {
            if (inventoryItem.furnitureList[i].have && !inventoryItem.furnitureList[i].use_check)
            {
                //����ִ� �׸� ������
                GameObject instance = Instantiate(itemprefab, parent_furniture); // �θ� ����
                //�׸� �����տ� �̹��� ���
                instance.GetComponent<Image>().sprite = inventoryItem.furnitureList[i].sprite;
                instance.GetComponent<ItemDrag>().number = i;
                instance.GetComponent<ItemDrag>().type = 0;
            }
        }
    }
    //�κ��丮 ���� �ٹ̱� ��ư ������ ��
    public void ClickWallCustomBtn()
    {
        inventoryPanel_1.SetActive(false);
        inventoryPanel_2.SetActive(true);
        inventoryPanel_3.SetActive(false);
        //���� ������ ����
        ItemDestroy();

        //�������� ���Ǿ��� ���� ������ Ŭ������ �� ���� ������Ʈ ������Ѵ�.
        houseItemJson.GetComponent<HouseInventoryJSON>().LoadPlayerDataFromJson();

        //������ ��������
        HouseInventoryData inventoryItem = houseItemJson.GetComponent<HouseInventoryJSON>().getHouseItem();

        //�������� �����ϰ� �ִٸ� ������ ����
        for (int i = 0; i < inventoryItem.wallItemList.Length; i++)
        {
            if (inventoryItem.wallItemList[i].have)
            {
                //����ִ� �׸� ������
                GameObject instance = Instantiate(itemprefab, parent_wall); // �θ� ����
                //�׸� �����տ� �̹��� ���
                instance.GetComponent<Image>().sprite = inventoryItem.wallItemList[i].sprite;
                //������ �������� nun, type �Ѱ��ֱ�
                GameObject itemCS = instance.transform.GetChild(0).gameObject;
                itemCS.GetComponent<HouseItemPrefabClick>().num = inventoryItem.wallItemList[i].num;
                itemCS.GetComponent<HouseItemPrefabClick>().type = inventoryItem.wallItemList[i].type;
                instance.GetComponent<ItemDrag>().type = 1;
            }
        }
    }
    //�κ��丮 �ٴ� �ٹ̱� ��ư ������ ��
    public void ClickTileCustomBtn()
    {
        inventoryPanel_1.SetActive(false);
        inventoryPanel_2.SetActive(false);
        inventoryPanel_3.SetActive(true);
        //���� ������ ����
        ItemDestroy();

        //�������� ���Ǿ��� ���� ������ Ŭ������ �� ���� ������Ʈ ������Ѵ�.
        houseItemJson.GetComponent<HouseInventoryJSON>().LoadPlayerDataFromJson();

        //������ ��������
        HouseInventoryData inventoryItem = houseItemJson.GetComponent<HouseInventoryJSON>().getHouseItem();

        //�������� �����ϰ� �ִٸ� ������ ����
        for (int i = 0; i < inventoryItem.floorItemList.Length; i++)
        {
            if (inventoryItem.floorItemList[i].have)
            {
                //����ִ� �׸� ������
                GameObject instance = Instantiate(itemprefab, parent_floor); // �θ� ����
                //�׸� �����տ� �̹��� ���
                instance.GetComponent<Image>().sprite = inventoryItem.floorItemList[i].sprite;
                //������ �������� nun, type �Ѱ��ֱ�
                GameObject itemCS = instance.transform.GetChild(0).gameObject;
                itemCS.GetComponent<HouseItemPrefabClick>().num = inventoryItem.floorItemList[i].num;
                itemCS.GetComponent<HouseItemPrefabClick>().type = inventoryItem.floorItemList[i].type;
                instance.GetComponent<ItemDrag>().type = 2;
            }
        }
    }

    public void ItemDestroy()
    {
        //���� content �ڽ� ������Ʈ ����
        int count = prefabParent_furniture.gameObject.transform.childCount;
        if (count > 0)//������ ������ prefab�� �ִٴ� ��
        {
            for (int i = 0; i < count; i++)
            {
                GameObject destory_obj = prefabParent_furniture.gameObject.transform.GetChild(i).gameObject;
                Destroy(destory_obj);
            }
        }

        //���� content �ڽ� ������Ʈ ����
        count = prefabParent_wall.gameObject.transform.childCount;
        if (count > 0)//������ ������ prefab�� �ִٴ� ��
        {
            for (int i = 0; i < count; i++)
            {
                GameObject destory_obj = prefabParent_wall.gameObject.transform.GetChild(i).gameObject;
                Destroy(destory_obj);
            }
        }

        //�ٴ� content �ڽ� ������Ʈ ����
        count = prefabParent_floor.gameObject.transform.childCount;
        if (count > 0)//������ ������ prefab�� �ִٴ� ��
        {
            for (int i = 0; i < count; i++)
            {
                GameObject destory_obj = prefabParent_floor.gameObject.transform.GetChild(i).gameObject;
                Destroy(destory_obj);
            }
        }
    }
}
