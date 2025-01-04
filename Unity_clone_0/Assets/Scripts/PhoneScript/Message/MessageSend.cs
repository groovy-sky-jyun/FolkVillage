using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MessageSend : MonoBehaviour
{

    public InputField message;
    string messageText;
    string user_id;
    string friend_id;
    string MessageSendURL = "http://localhost/messageSend.php";
    public Transform parent;
    public GameObject prefab_me;


    // Update is called once per frame
    public void  SendMessageBtn()
    {
        if (message.text.Equals("")) { Debug.Log("Empty"); return; }

        //inputfield 가져오기
        messageText = message.text;
        //아이디 가져오기
        user_id= PlayerPrefs.GetString("user_id");
        friend_id= PlayerPrefs.GetString("FriendID");

        //DB에 저장 
        StartCoroutine(CheckListDB(user_id, friend_id, messageText));

        

    }
    IEnumerator CheckListDB(string user_id, string friend_id, string msg)
    {
       
        WWWForm form = new WWWForm();
        form.AddField("userIdPost", user_id);
        form.AddField("friendIdPost", friend_id);
        form.AddField("textPost", msg);
        form.AddField("timePost", DateTime.Now.ToString(("yyyy-MM-dd-HH-mm-ss")));
        UnityWebRequest www = UnityWebRequest.Post(MessageSendURL, form);

        yield return www.SendWebRequest();
        string text = www.downloadHandler.text;
        //내가 문자 보내는 프리팹 생성
        GameObject instance2 = Instantiate(prefab_me, parent);
        instance2.GetComponentInChildren<Text>().text = msg;//문자 내용 입력
        //inputfield 초기화
        message.text = "";
    }
}
