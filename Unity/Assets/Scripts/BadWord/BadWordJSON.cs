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
    // 컴포넌트 메뉴에 아래 함수를 호출하는 To Json Data 라는 명령어가 생성됨

    private void Start()
    {
        //가져오기
        LoadPlayerDataFromJson();
    }


    [ContextMenu("To Json")] 
    public void SavePlayerDataToJson()
    {
        // ToJson을 사용하면 JSON형태로 포멧팅된 문자열이 생성된다  
        string jsonData = JsonUtility.ToJson(badWordList, true);
        // 데이터를 저장할 경로 지정
        string path = Path.Combine(Application.dataPath, "./Scripts/BadWord/BadWordData.json");
        // 파일 생성 및 저장
        File.WriteAllText(path, jsonData);
    }

    [ContextMenu("From Json Data")]
    public void LoadPlayerDataFromJson()
    {
        // 데이터를 불러올 경로 지정
        string path = Path.Combine(Application.dataPath, "./Scripts/BadWord/BadWordData.json");
        // 파일의 텍스트를 string으로 저장
        string jsonData = File.ReadAllText(path);
        // 이 Json데이터를 역직렬화하여 itemrData 넣어줌
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

