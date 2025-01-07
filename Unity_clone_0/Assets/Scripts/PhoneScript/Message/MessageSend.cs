using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MessageSend : MonoBehaviour
{

    public InputField message;
    public Transform parent;
    public GameObject prefab_me;

    private string user_id;
    private string friend_id;
    private int table_number;
    private int message_count;
    string setMessage_send = "http://localhost/folkVillage/phoneMessage/messageSend.php";



    // Update is called once per frame
    public void  SendMessageBtn()
    {
        if (message.text.Equals("")) { Debug.Log("Empty"); return; }

        //inputfield 가져오기
        string messageText = message.text;
        //아이디 가져오기
        user_id= PlayerPrefs.GetString("user_id");
        friend_id= PlayerPrefs.GetString("friend_id");
        table_number = PlayerPrefs.GetInt("table_number");
        message_count = PlayerPrefs.GetInt("message_count");
        //DB에 추가 
        StartCoroutine(AddMessageRecord(messageText));
    }
    IEnumerator AddMessageRecord(string content)
    {
        //메시지 전체 개수 1 증가
        message_count++;
        PlayerPrefs.SetInt("message_count", message_count);

        //content를 messagerecord에 추가 && messagelist의 count update
        WWWForm form = new WWWForm();
        form.AddField("tableNumPost", table_number);
        form.AddField("userIdPost", user_id);
        form.AddField("friendIdPost", friend_id);
        form.AddField("textPost", content);
        form.AddField("messageCountPost", message_count);
        //form.AddField("timePost", DateTime.Now.ToString(("yyyy-MM-dd-HH-mm-ss")));
        UnityWebRequest www = UnityWebRequest.Post(setMessage_send, form);

        yield return www.SendWebRequest();
        string text = www.downloadHandler.text;
        Debug.Log("문자보내기 결과 : "+text);
        if (text != "fail")
        {
            //내가 문자 보내는 프리팹 생성
            GameObject instance2 = Instantiate(prefab_me, parent);
            instance2.GetComponentInChildren<Text>().text = content;

            //inputfield 초기화
            message.text = "";
        }
        else Debug.Log("메시지 보내기 실패");
    }
}
