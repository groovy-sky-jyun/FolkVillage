
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;



public class CoinSetting : MonoBehaviour
{
    string user_id;
    string userDB = "http://localhost/coinSetting.php";
    public GameObject coin_txt;
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
            PlayerPrefs.SetString("coin",str);
        }

    }
}
