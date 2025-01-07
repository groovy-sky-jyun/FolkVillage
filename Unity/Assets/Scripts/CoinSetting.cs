
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;



public class CoinSetting : MonoBehaviour
{
    public string user_id;
    string userDB = "http://localhost/folkvillage/userinfo/coinSetting.php";
    string updateDB = "http://localhost/folkvillage/userinfo/coinUpdate.php";
    public GameObject coin_txt;
    public int coin;
    void Start()
    {
       user_id= PlayerPrefs.GetString("user_id");

        StartCoroutine(CoinDB(user_id));
    }

    IEnumerator CoinDB(string id)
    {
        WWWForm form = new WWWForm();
        form.AddField("user_idPost", id);
        UnityWebRequest www = UnityWebRequest.Post(userDB, form);

        yield return www.SendWebRequest();
        string str = www.downloadHandler.text;
        Debug.Log(str);
        if (str!="fail")
        {
            coin_txt.GetComponent<Text>().text = str;
            coin = int.Parse(str);
            PlayerPrefs.SetInt("coin", coin);
        }

    }

   public void minusCoin(int price)
    {
        coin -= price;
        PlayerPrefs.SetInt("coin", coin);
        //userinfo �� coin update�ϱ�
        StartCoroutine(CoinUpdateDB(coin));
    }
    IEnumerator CoinUpdateDB(int coin)
    {
        WWWForm form = new WWWForm();
        form.AddField("user_idPost", user_id);
        form.AddField("user_coinPost", coin);
        UnityWebRequest www = UnityWebRequest.Post(updateDB, form);

        yield return www.SendWebRequest();
        string str = www.downloadHandler.text;
        
        if (str != "fail")
        {
            //userinfo db�� ���� �����Ȱ� �ҷ�����
            StartCoroutine(CoinDB(user_id));
            Debug.Log(str);
        }
        else
        {
            Debug.Log("coinUpdatefail: "+str);
        }

    }
}
