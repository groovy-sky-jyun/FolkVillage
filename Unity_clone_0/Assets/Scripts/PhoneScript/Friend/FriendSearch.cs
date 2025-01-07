using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FriendSearch : MonoBehaviour
{
    public InputField search_nickname;
    public Text friend_name;
    public GameObject friend_lsit;
    public Image boy;
    public Image girl;
    public Text btn_text;

    string FriendSearchURL = "http://localhost/folkVillage/phoneFriend/friendSearch.php";
    string FriendApplyURL = "http://localhost/folkVillage/phoneFriend/friendApply.php";

    string from_user_id;

    public void OnConnectedToServer()
    {
        string search_name = search_nickname.text;
        if (search_name != null)
        {
            StartCoroutine(UserDB(search_name));
        }
    }

    IEnumerator UserDB(string username)
    {
        WWWForm form = new WWWForm();
        form.AddField("nicknamePost", username);
      

        UnityWebRequest www = UnityWebRequest.Post(FriendSearchURL, form);

        yield return www.SendWebRequest();
        string text = www.downloadHandler.text;
        Debug.Log(text);
        if (text != "no user")
        {
            string[] arr = text.Split(',');
            from_user_id = arr[1];//id

            Debug.Log(arr[0]);//gender
            Debug.Log(from_user_id);

            friend_lsit.gameObject.SetActive(true);
            friend_name.text = username;
            if (arr[0] == "0")
            {
                boy.gameObject.SetActive(true);
                girl.gameObject.SetActive(false);
            }

            else
            {
                boy.gameObject.SetActive(false);
                girl.gameObject.SetActive(true);
            }
        }
        else
        {
            Debug.Log(text);
            friend_lsit.gameObject.SetActive(false);
        }
            
    }

    public void ApplyOnConnectedToServer()
    {
        string user_id = PlayerPrefs.GetString("user_id");
        StartCoroutine(FriendApplytDB(user_id, from_user_id));
        
    }

    IEnumerator FriendApplytDB(string user_id, string from_user_id)
    {
        WWWForm form = new WWWForm();
        form.AddField("userIdPost", user_id);
        form.AddField("fromUserIdPost", from_user_id);
        UnityWebRequest www = UnityWebRequest.Post(FriendApplyURL, form);

        yield return www.SendWebRequest();
        if (www.downloadHandler.text == "历厘己傍")
        {
            Debug.Log("历厘己傍");
            btn_text.text = "秒家";
        }
        
    }
}
