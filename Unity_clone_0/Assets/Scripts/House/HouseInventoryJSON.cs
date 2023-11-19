using Newtonsoft.Json.Bson;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class HouseInventoryJSON : MonoBehaviour
{
    public HouseInventoryData inventoryItem;
    // ������Ʈ �޴��� �Ʒ� �Լ��� ȣ���ϴ� To Json Data ��� ��ɾ ������

    private void Start()
    {
        //��������
        LoadPlayerDataFromJson();
    }


    [ContextMenu("To Json")] 
    public void SavePlayerDataToJson()
    {
        // ToJson�� ����ϸ� JSON���·� �����õ� ���ڿ��� �����ȴ�  
        string jsonData = JsonUtility.ToJson(inventoryItem, true);
        // �����͸� ������ ��� ����
        string path = Path.Combine(Application.dataPath, "./Scripts/House/JsonFile/HouseInventoryData.json");
        // ���� ���� �� ����
        File.WriteAllText(path, jsonData);
    }

    [ContextMenu("From Json Data")]
    public void LoadPlayerDataFromJson()
    {
        // �����͸� �ҷ��� ��� ����
        string path = Path.Combine(Application.dataPath, "./Scripts/House/JsonFile/HouseInventoryData.json");
        // ������ �ؽ�Ʈ�� string���� ����
        string jsonData = File.ReadAllText(path);
        // �� Json�����͸� ������ȭ�Ͽ� itemrData �־���
        inventoryItem = JsonUtility.FromJson<HouseInventoryData>(jsonData);
        
        //Debug.Log(inventoryItem.wallItemList[0].item_name);
    }
    public HouseInventoryData getHouseItem()
    {
        return inventoryItem;
    }
}
[System.Serializable]
public struct FurnitureItemDictionary {
    public int type;//0
    public Sprite sprite;
    public bool have;
    public bool use_check;
    public int price;
}
[System.Serializable]
public struct WallItemDictionary
{
    public int type;//1
    public Sprite sprite;
    public int num;
    public bool have;
    public bool use_check;
    public int price;
}
[System.Serializable]
public struct FloorItemDictionary
{
    public int type;//2
    public Sprite sprite;
    public int num;
    public bool have;
    public bool use_check;
    public int price;
}

[System.Serializable]
 public class HouseInventoryData
{
    public FurnitureItemDictionary[] furnitureList;
    public WallItemDictionary[] wallItemList;
    public FloorItemDictionary[] floorItemList;
    public int useWallItem;

    public void SetWallFloor(int wall_num,int floor_num)
    {
        for(int i=0;i< wallItemList.Length; i++)
        {
            if (i == wall_num)
            {
                wallItemList[i].use_check = true;
            }
            else
                wallItemList[i].use_check = false;
        }
        for (int i = 0; i < floorItemList.Length; i++)
        {
            if (i == floor_num)
            {
                floorItemList[i].use_check = true;
            }
            else
                floorItemList[i].use_check = false;
        }

    }

}

