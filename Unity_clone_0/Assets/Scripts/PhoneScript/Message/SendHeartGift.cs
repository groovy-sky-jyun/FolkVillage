using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.UI;


public class SendHeartGift : MonoBehaviour
{
    public GameObject sendBefore;
    public GameObject sendAfter;
    public Text friendName;

    private string user_id;
    private string friend_id;
    private string sendHeartDB = "http://localhost/folkVillage/heart/sendHeartGift.php";
    private string getFriend_id = "http://localhost/folkVillage/phoneFriend/friendID.php";


    public void HeartBtnOnClick()
    {
        user_id= PlayerPrefs.GetString("user_id");

        //하트 색상 변경 & 버튼 선택 비활성화
        sendBefore.SetActive(false);
        sendAfter.SetActive(true);
        sendAfter.gameObject.GetComponent<Button>().interactable = false;

        //메시지로 선물 보내기
        string friend_name = friendName.text;
        StartCoroutine(getFriendID(friend_name));
    }
    
    IEnumerator getFriendID(string friend_name)
    {
        //1.닉네임으로 친구 아이디 가져오기
        WWWForm form = new WWWForm();
        form.AddField("nicknamePost", friend_name);
        UnityWebRequest www = UnityWebRequest.Post(getFriend_id, form);

        yield return www.SendWebRequest();
        string response = www.downloadHandler.text;
        Debug.Log(response);
        if (response != "fail")
        {
            friend_id = response;
            StartCoroutine(SendGiftMessage());
        }

    }
    IEnumerator SendGiftMessage()
    {
        //2. 메시지로 선물 보내기
        WWWForm form = new WWWForm();
        form.AddField("user_idPost", user_id);
        form.AddField("friend_idPost", friend_id);
        form.AddField("textPost", "sendHeart_gift");
        UnityWebRequest www = UnityWebRequest.Post(sendHeartDB, form);

        yield return www.SendWebRequest();
        string str = www.downloadHandler.text;
        Debug.Log(str);
    }
}