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

    //초기화
    private void Start()
    {
        FurniturePanelOff();
        open = false;
        //아이템 가져오기
        houseItemJson.GetComponent<HouseInventoryJSON>().LoadPlayerDataFromJson();
        HouseInventoryData inventoryItem = houseItemJson.GetComponent<HouseInventoryJSON>().getHouseItem();

        //use_check로 furniture 기본 설정
        for (int i = 0; i < inventoryItem.furnitureList.Length; i++)
        {
            if (inventoryItem.furnitureList[i].use_check)
            {
                GameObject furniture_prefab = Instantiate(furniturePrefab, furniturePrefab_parent); // 부모 지정
                furniture_prefab.GetComponent<Image>().sprite = inventoryItem.furnitureList[i].sprite;
                furniture_prefab.transform.localPosition = new Vector3(inventoryItem.furnitureList[i].x, inventoryItem.furnitureList[i].y, 0);
                //furniture_prefab.transform.localScale = new Vector2(2, 2);
                furniture_prefab.GetComponent<RectTransform>().sizeDelta = new Vector2(inventoryItem.furnitureList[i].width, inventoryItem.furnitureList[i].height);
                
            }
        }

        //use_check로 wall 기본 설정
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
        //use_check로 floor 기본 설정
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
    //전부 가리기(씬 시작할때, 버튼 한번 더 눌러서 커스텀 기능 종료할때)
    private void FurniturePanelOff()
    {
        inventoryPanel_1.SetActive(false);
        inventoryPanel_2.SetActive(false);
        inventoryPanel_3.SetActive(false);
        open = false;
    }
    //화면우측상단 가구버튼 눌렀을 때
    public void CustomInventoryBtn()
    {
        if (open)//열린 상태에서 버튼 한번 더 눌리면 저장 및 종료
        {
            //아이템 변경사항 저장
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
    //인벤토리 가구 꾸미기 버튼 눌렀을 때
    public void ClickFurnitureCustomBtn()
    {
        inventoryPanel_1.SetActive(true);
        inventoryPanel_2.SetActive(false);
        inventoryPanel_3.SetActive(false);
        //기존 아이템 삭제
        ItemDestroy();

        //아이템이 사용되었을 수도 있으니 클릭했을 때 마다 업데이트 해줘야한다.
        houseItemJson.GetComponent<HouseInventoryJSON>().LoadPlayerDataFromJson();

        //아이템 가져오기
        HouseInventoryData inventoryItem = houseItemJson.GetComponent<HouseInventoryJSON>().getHouseItem();        

       //아이템을 소유하고 있다면 프리팹 생성
        for (int i=0;i< inventoryItem.furnitureList.Length; i++)
        {
            if (inventoryItem.furnitureList[i].have && !inventoryItem.furnitureList[i].use_check)
            {
                //비어있는 네모 프리팹
                GameObject instance = Instantiate(itemprefab, parent_furniture); // 부모 지정
                //네모 프리팹에 이미지 등록
                instance.GetComponent<Image>().sprite = inventoryItem.furnitureList[i].sprite;
                instance.GetComponent<ItemDrag>().number = i;
                instance.GetComponent<ItemDrag>().type = 0;
            }
        }
    }
    //인벤토리 벽지 꾸미기 버튼 눌렀을 때
    public void ClickWallCustomBtn()
    {
        inventoryPanel_1.SetActive(false);
        inventoryPanel_2.SetActive(true);
        inventoryPanel_3.SetActive(false);
        //기존 아이템 삭제
        ItemDestroy();

        //아이템이 사용되었을 수도 있으니 클릭했을 때 마다 업데이트 해줘야한다.
        houseItemJson.GetComponent<HouseInventoryJSON>().LoadPlayerDataFromJson();

        //아이템 가져오기
        HouseInventoryData inventoryItem = houseItemJson.GetComponent<HouseInventoryJSON>().getHouseItem();

        //아이템을 소유하고 있다면 프리팹 생성
        for (int i = 0; i < inventoryItem.wallItemList.Length; i++)
        {
            if (inventoryItem.wallItemList[i].have)
            {
                //비어있는 네모 프리팹
                GameObject instance = Instantiate(itemprefab, parent_wall); // 부모 지정
                //네모 프리팹에 이미지 등록
                instance.GetComponent<Image>().sprite = inventoryItem.wallItemList[i].sprite;
                //생성된 프리팹의 nun, type 넘겨주기
                GameObject itemCS = instance.transform.GetChild(0).gameObject;
                itemCS.GetComponent<HouseItemPrefabClick>().num = inventoryItem.wallItemList[i].num;
                itemCS.GetComponent<HouseItemPrefabClick>().type = inventoryItem.wallItemList[i].type;
                instance.GetComponent<ItemDrag>().type = 1;
            }
        }
    }
    //인벤토리 바닥 꾸미기 버튼 눌렀을 때
    public void ClickTileCustomBtn()
    {
        inventoryPanel_1.SetActive(false);
        inventoryPanel_2.SetActive(false);
        inventoryPanel_3.SetActive(true);
        //기존 아이템 삭제
        ItemDestroy();

        //아이템이 사용되었을 수도 있으니 클릭했을 때 마다 업데이트 해줘야한다.
        houseItemJson.GetComponent<HouseInventoryJSON>().LoadPlayerDataFromJson();

        //아이템 가져오기
        HouseInventoryData inventoryItem = houseItemJson.GetComponent<HouseInventoryJSON>().getHouseItem();

        //아이템을 소유하고 있다면 프리팹 생성
        for (int i = 0; i < inventoryItem.floorItemList.Length; i++)
        {
            if (inventoryItem.floorItemList[i].have)
            {
                //비어있는 네모 프리팹
                GameObject instance = Instantiate(itemprefab, parent_floor); // 부모 지정
                //네모 프리팹에 이미지 등록
                instance.GetComponent<Image>().sprite = inventoryItem.floorItemList[i].sprite;
                //생성된 프리팹의 nun, type 넘겨주기
                GameObject itemCS = instance.transform.GetChild(0).gameObject;
                itemCS.GetComponent<HouseItemPrefabClick>().num = inventoryItem.floorItemList[i].num;
                itemCS.GetComponent<HouseItemPrefabClick>().type = inventoryItem.floorItemList[i].type;
                instance.GetComponent<ItemDrag>().type = 2;
            }
        }
    }

    public void ItemDestroy()
    {
        //가구 content 자식 오브젝트 삭제
        int count = prefabParent_furniture.gameObject.transform.childCount;
        if (count > 0)//이전의 생성된 prefab이 있다는 뜻
        {
            for (int i = 0; i < count; i++)
            {
                GameObject destory_obj = prefabParent_furniture.gameObject.transform.GetChild(i).gameObject;
                Destroy(destory_obj);
            }
        }

        //벽지 content 자식 오브젝트 삭제
        count = prefabParent_wall.gameObject.transform.childCount;
        if (count > 0)//이전의 생성된 prefab이 있다는 뜻
        {
            for (int i = 0; i < count; i++)
            {
                GameObject destory_obj = prefabParent_wall.gameObject.transform.GetChild(i).gameObject;
                Destroy(destory_obj);
            }
        }

        //바닥 content 자식 오브젝트 삭제
        count = prefabParent_floor.gameObject.transform.childCount;
        if (count > 0)//이전의 생성된 prefab이 있다는 뜻
        {
            for (int i = 0; i < count; i++)
            {
                GameObject destory_obj = prefabParent_floor.gameObject.transform.GetChild(i).gameObject;
                Destroy(destory_obj);
            }
        }
    }
}
