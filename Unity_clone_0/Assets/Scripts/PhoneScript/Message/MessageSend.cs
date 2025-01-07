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

        //inputfield ��������
        string messageText = message.text;
        //���̵� ��������
        user_id= PlayerPrefs.GetString("user_id");
        friend_id= PlayerPrefs.GetString("friend_id");
        table_number = PlayerPrefs.GetInt("table_number");
        message_count = PlayerPrefs.GetInt("message_count");
        //DB�� �߰� 
        StartCoroutine(AddMessageRecord(messageText));
    }
    IEnumerator AddMessageRecord(string content)
    {
        //�޽��� ��ü ���� 1 ����
        message_count++;
        PlayerPrefs.SetInt("message_count", message_count);

        //content�� messagerecord�� �߰� && messagelist�� count update
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
        Debug.Log("���ں����� ��� : "+text);
        if (text != "fail")
        {
            //���� ���� ������ ������ ����
            GameObject instance2 = Instantiate(prefab_me, parent);
            instance2.GetComponentInChildren<Text>().text = content;

            //inputfield �ʱ�ȭ
            message.text = "";
        }
        else Debug.Log("�޽��� ������ ����");
    }
}
