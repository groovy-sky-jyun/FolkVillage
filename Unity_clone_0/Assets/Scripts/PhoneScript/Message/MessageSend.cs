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

    // Update is called once per frame
    public void  SendMessageBtn()
    {
        if (message.text.Equals("")) { Debug.Log("Empty"); return; }

        //inputfield ��������
        messageText = message.text;
        //���̵� ��������
        user_id= PlayerPrefs.GetString("user_id");
        friend_id= PlayerPrefs.GetString("FriendID");

        //DB�� ���� 
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

        //inputfield �ʱ�ȭ
        message.text = "";
    }
}
