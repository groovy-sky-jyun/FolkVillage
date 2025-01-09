using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.UI;


public class HeartAddCoin : MonoBehaviour
{
    string user_id;
    string friend_id;
    int coin;
    string heartCoinDB = "http://localhost/folkVillage/phoneMessage/friendListUpdateGift.php";
    string checkHeartDB = "http://localhost/folkVillage/phoneMessage/checkAcceptHeart.php";
    public GameObject heartAddBtn;
    GameObject coinText;


    public void Start()
    {
        friend_id = PlayerPrefs.GetString("FriendID");
        user_id = PlayerPrefs.GetString("user_id");
        //accept_heart==1이면
        //버튼 비활성화
        StartCoroutine(CheckHeratDB(user_id, friend_id));
    }
    IEnumerator CheckHeratDB(string user_id, string friend_id)
    {
        WWWForm form = new WWWForm();
        form.AddField("user_idPost", user_id);
        form.AddField("friend_idPost", friend_id);
        UnityWebRequest www = UnityWebRequest.Post(checkHeartDB, form);

        yield return www.SendWebRequest();
        string str = www.downloadHandler.text;
        str=str.Trim();
        if (str != "fail"&&str=="1")
        {
            //버튼 비활성화
            heartAddBtn.gameObject.GetComponent<Button>().interactable = false;
           

        }

    }
    public void HeartAddOnClick()
    {
        coin = PlayerPrefs.GetInt("coin") + 5;
        StartCoroutine(UpdateCoinDB(user_id, friend_id, coin));
    }

    IEnumerator UpdateCoinDB(string user_id, string friend_id, int update_coin)
    {
        WWWForm form = new WWWForm();
        form.AddField("user_idPost", user_id);
        form.AddField("friend_idPost", friend_id);
        form.AddField("user_coin", coin);
        UnityWebRequest www = UnityWebRequest.Post(heartCoinDB, form);

        yield return www.SendWebRequest();
        string str = www.downloadHandler.text;
        if (str != "fail")
        {
            //버튼 비활성화
            heartAddBtn.gameObject.GetComponent<Button>().interactable = false;
            //코인 5증가된걸로 다시 setting
            coinText=GameObject.FindGameObjectWithTag("Coin");
            coinText.GetComponent<Text>().text = update_coin.ToString();
            PlayerPrefs.SetString("coin", update_coin.ToString());

        }

    }
}
