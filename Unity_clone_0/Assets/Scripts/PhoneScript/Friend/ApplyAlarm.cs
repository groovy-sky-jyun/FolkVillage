using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ApplyAlarm : MonoBehaviour
{
    public GameObject alarm1;
    public GameObject alarm2;

    private string user_id;
    private string check_applyFriend = "http://localhost/folkVillage/phoneFriend/friendApplyCheckList.php";
    void Start()
    {
        user_id = PlayerPrefs.GetString("user_id");
        alarm1.SetActive(false);
        alarm2.SetActive(false);
        StartCoroutine(CheckDatabaseChanges());
    }

    IEnumerator CheckDatabaseChanges()
    {
        WWWForm form = new WWWForm();
        form.AddField("user_idPost", user_id);
        UnityWebRequest www = UnityWebRequest.Post(check_applyFriend, form);

        yield return www.SendWebRequest();
        string str = www.downloadHandler.text;
        Debug.Log(str);
        if (str != "fail" && str !="null")
        {
            alarm1.SetActive(true);
            alarm2.SetActive(true);
        }
    }

}
