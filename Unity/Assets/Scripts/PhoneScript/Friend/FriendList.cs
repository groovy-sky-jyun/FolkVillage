using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UIElements;
using UnityEngine.UI;
using Photon.Pun;

public class FriendList : MonoBehaviour
{
    public GameObject prefab;
    public Transform parent;
    public Text alarm_text;

    string user_id;
    
    string FriendSearchURL = "http://localhost/friendList.php";

    private void Start()
    {
        alarm_text.gameObject.SetActive(true);
    }
    public void FriendBtnOnClick()
    {
        user_id = PlayerPrefs.GetString("user_id");
        StartCoroutine(FriendDB());
    }
    IEnumerator FriendDB()
    {
        WWWForm form = new WWWForm();
        form.AddField("user_idPost", user_id);


        UnityWebRequest www = UnityWebRequest.Post(FriendSearchURL, form);

        yield return www.SendWebRequest();
        string text = www.downloadHandler.text;
       
        Debug.Log(text);
       if (text != "fail")
        {
            alarm_text.gameObject.SetActive(false);
            string[] arr = text.Split(',');
           for(int i=0; i<arr.Length-1; i++)
            {
                GameObject instance = Instantiate(prefab, parent); // 부모 지정
                instance.GetComponentInChildren<Text>().text=arr[i++];//짝수는 닉네임
                if (arr[i] == "0") //하트 아직 안보낸 상태
                {
                    instance.transform.GetChild(0).gameObject.SetActive(true);
                    instance.transform.GetChild(1).gameObject.SetActive(false);
                }
                else
                {
                    instance.transform.GetChild(0).gameObject.SetActive(false);
                    instance.transform.GetChild(1).gameObject.SetActive(true);
                }
            }
        }
        else
        {
            Debug.Log(text);
            alarm_text.gameObject.SetActive(true);
        }
       

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
