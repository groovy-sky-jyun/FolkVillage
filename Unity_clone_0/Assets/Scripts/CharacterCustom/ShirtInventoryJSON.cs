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

public class ShirtInventoryJSON : MonoBehaviour
{
    public CustomInventoryData inventoryItem;
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
        string path = Path.Combine(Application.dataPath, "./Scripts/CharacterCustom/JsonFile/CustomInventoryData.json");
        // ���� ���� �� ����
        File.WriteAllText(path, jsonData);
    }

    [ContextMenu("From Json Data")]
    public void LoadPlayerDataFromJson()
    {
        // �����͸� �ҷ��� ��� ����
        string path = Path.Combine(Application.dataPath, "./Scripts/CharacterCustom/JsonFile/CustomInventoryData.json");
        // ������ �ؽ�Ʈ�� string���� ����
        string jsonData = File.ReadAllText(path);
        // �� Json�����͸� ������ȭ�Ͽ� itemrData �־���
        inventoryItem = JsonUtility.FromJson<CustomInventoryData>(jsonData);
        
        //Debug.Log(inventoryItem.wallItemList[0].item_name);
    }
    public CustomInventoryData getCustomItem()
    {
        return inventoryItem;
    }
}
[System.Serializable]
public struct FaceItemDictionary
{
    public int type;//0
    public int num;
    public string name;
    public string sprite_name;
    public bool have;
    public bool use_check;
    public int price;
}
[System.Serializable]
public struct HairItemDictionary
{
    public int type;//1
    public int num;
    public string name;
    public string sprite_name;
    public bool have;
    public bool use_check;
    public int price;
}
[System.Serializable]
public struct ShirtItemDictionary 
{
    public int type;//2
    public int num;
    public string name;
    public string sprite_name;
    public bool have;
    public bool use_check;
    public int price;
}
[System.Serializable]
public struct PantsItemDictionary
{
    public int type;//3
    public string name;
    public string sprite_name;
    public int num;
    public bool have;
    public bool use_check;
    public int price;
}
[System.Serializable]
 public class CustomInventoryData
{
    public FaceItemDictionary[] faceItemList;
    public HairItemDictionary[] hairItemList;
    public ShirtItemDictionary[] shirtItemList;
    public PantsItemDictionary[] pantsItemList;
}

