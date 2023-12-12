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

public class BadWordJSON : MonoBehaviour
{
    public BadWordData badWordList;
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
        string jsonData = JsonUtility.ToJson(badWordList, true);
        // �����͸� ������ ��� ����
        string path = Path.Combine(Application.dataPath, "./Scripts/BadWord/BadWordData.json");
        // ���� ���� �� ����
        File.WriteAllText(path, jsonData);
    }

    [ContextMenu("From Json Data")]
    public void LoadPlayerDataFromJson()
    {
        // �����͸� �ҷ��� ��� ����
        string path = Path.Combine(Application.dataPath, "./Scripts/BadWord/BadWordData.json");
        // ������ �ؽ�Ʈ�� string���� ����
        string jsonData = File.ReadAllText(path);
        // �� Json�����͸� ������ȭ�Ͽ� itemrData �־���
        badWordList = JsonUtility.FromJson<BadWordData>(jsonData);
        
    }
    public BadWordData getBadWordList()
    {
        return badWordList;
    }

}

[System.Serializable]
 public class BadWordData
{
    public string[] badWord;

}

