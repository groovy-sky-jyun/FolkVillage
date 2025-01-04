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
    string user_id;
    string friend_id;
    string friend_name;
    string sendHeartDB = "http://localhost/sendHeartGift.php";
    string getNickDB = "http://localhost/friendID.php";
    public GameObject sendBefore;
    public GameObject sendAfter;
    public Text nickname;

    public void HeartBtnOnClick()
    {
        user_id= PlayerPrefs.GetString("user_id");
        //하트 색상 변경 & 버튼 선택 비활성화
        sendBefore.SetActive(false);
        sendAfter.SetActive(true);
        sendAfter.gameObject.GetComponent<Button>().interactable = false;
        //닉네임으로 친구 아이디 가져오기
        friend_name = nickname.text;
        StartCoroutine(GetIdDB(friend_name));
    }
    
    IEnumerator GetIdDB(string friend_name)
    {
        WWWForm form = new WWWForm();
        form.AddField("nicknamePost", friend_name);
        UnityWebRequest www = UnityWebRequest.Post(getNickDB, form);

        yield return www.SendWebRequest();
        string str = www.downloadHandler.text;
        Debug.Log(str);
        if (str != "fail")
        {
            friend_id = str;
            Debug.Log(friend_id);
            //UPDATE send_gift=1 & INSERT messagerecord "sendheart_gift"
            StartCoroutine(SendGiftDB(user_id, friend_id));
        }

    }
    IEnumerator SendGiftDB(string user_id,string friend_id)
    {
        WWWForm form = new WWWForm();
        form.AddField("user_idPost", user_id);
        form.AddField("friend_idPost", friend_id);
        form.AddField("textPost", "sendHeart_gift");
        form.AddField("timePost", DateTime.Now.ToString(("yyyy-MM-dd-HH-mm-ss")));
        UnityWebRequest www = UnityWebRequest.Post(sendHeartDB, form);

        yield return www.SendWebRequest();
        string str = www.downloadHandler.text;
        Debug.Log(str);
    }
}