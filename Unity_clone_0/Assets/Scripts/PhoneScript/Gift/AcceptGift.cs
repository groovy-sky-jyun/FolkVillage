using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.Networking;

public class AcceptGift : MonoBehaviour
{
    public GameObject afterGift;
    public GameObject beforeGift;
    public Text coinText;

    private int coin;
    private string user_id;
    private string friend_id;
    private string getAccept = "http://localhost/folkVillage/userinfo/getAcceptGift.php";
    private string updateCoin = "http://localhost/folkVillage/userinfo/coinUpdate.php";
    private string updateGift = "http://localhost/folkVillage/userinfo/acceptList.php";
    // Start is called before the first frame update
    void Start()
    {
        user_id = PlayerPrefs.GetString("user_id");
        friend_id = PlayerPrefs.GetString("friend_id");
        StartCoroutine(SetGiftImage());
    }

    IEnumerator SetGiftImage()
    {
        WWWForm form = new WWWForm();
        form.AddField("user_idPost", user_id);
        form.AddField("friend_idPost", friend_id);
        UnityWebRequest www = UnityWebRequest.Post(getAccept, form);

        yield return www.SendWebRequest();
        string str = www.downloadHandler.text;
        Debug.Log("gift default: "+str);
        if (str != "fail")
        {
            if (str == "1") { //선물 받은 상태
                afterGift.gameObject.SetActive(true);
                beforeGift.gameObject.SetActive(false);
            }
            else //선물 안받은 상태
            {
                afterGift.gameObject.SetActive(false);
                beforeGift.gameObject.SetActive(true);
            }
        }
        else
        {
            Debug.Log("failed add coin");
        }

      
    }

    public void AcceptGiftOnClick()
    {
        afterGift.gameObject.SetActive(true);
        beforeGift.gameObject.SetActive(false);

        coin = PlayerPrefs.GetInt("coin");
        coin += 5;
        PlayerPrefs.SetInt("coin", coin);

        StartCoroutine(CoinUpdateDB(coin));
    }

    IEnumerator CoinUpdateDB(int coin)
    {
        WWWForm form = new WWWForm();
        form.AddField("user_idPost", user_id);
        form.AddField("user_coinPost", coin);
        UnityWebRequest www = UnityWebRequest.Post(updateCoin, form);

        yield return www.SendWebRequest();
        string str = www.downloadHandler.text;

        if (str != "fail")
        {
            //화면 우측상단 코인 숫자 변경
            Text coinText = GameObject.Find("Canvas/CoinImg/coinText").GetComponent<Text>();
            coinText.text = coin.ToString();
            StartCoroutine(UpdateAcceptGift());
        }
        else
        {
            Debug.Log("failed add coin");
        }
    }

    IEnumerator UpdateAcceptGift()
    {
        WWWForm form = new WWWForm();
        form.AddField("user_idPost", user_id);
        form.AddField("friend_idPost", friend_id);
        UnityWebRequest www = UnityWebRequest.Post(updateGift, form);

        yield return www.SendWebRequest();
        string str = www.downloadHandler.text;

        Debug.Log(str);
    }
}