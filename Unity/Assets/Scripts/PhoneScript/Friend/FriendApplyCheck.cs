using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FriendApplyCheck : MonoBehaviour
{
    public Text to_user_nickname;
    public Image boy;
    public Image girl;
    public GameObject scroll_view;
    public Text alarm_text;
    public GameObject alarm_image1;
    public GameObject alarm_image2;
    public GameObject alarm_image3;

    string user_id;
    string[] arr;
    string FriendApplyCheckURL = "http://localhost/friendApplyCheck.php";
    string FriendApplyAcceptURL = "http://localhost/friendApplyAccept.php";
    void Start()
    {
        user_id = PlayerPrefs.GetString("user_id");
        Debug.Log(user_id);
        alarm_text.gameObject.SetActive(true);
        scroll_view.gameObject.SetActive(false);
        alarm_image1.gameObject.SetActive(false);
        alarm_image2.gameObject.SetActive(false);
        alarm_image3.gameObject.SetActive(false);
    }

    
    void Update()
    {
       StartCoroutine(FriendApplyCheckDB());
    }

    IEnumerator FriendApplyCheckDB()
    {
        WWWForm form = new WWWForm();
        form.AddField("idPost", user_id);

        UnityWebRequest www = UnityWebRequest.Post(FriendApplyCheckURL, form);

        yield return www.SendWebRequest();
        string text = www.downloadHandler.text;
        
        if ( text != "null") //ģ����û�� �ִٴ� ��
        {
            arr = text.Split(',');
            if (arr[2] == "0")
            {
                //UIȰ��ȭ
                scroll_view.gameObject.SetActive(true);
                alarm_text.gameObject.SetActive(false);
                alarm_image1.gameObject.SetActive(true);
                alarm_image2.gameObject.SetActive(true);
                alarm_image3.gameObject.SetActive(true);

                //�г��� ����
                to_user_nickname.text = arr[0];

                //���� ����
                if (arr[1] == "0")
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
        }
        else//ģ����û ���� ���
        {
            alarm_text.gameObject.SetActive(true);
            scroll_view.gameObject.SetActive(false);
            alarm_image1.gameObject.SetActive(false);
            alarm_image2.gameObject.SetActive(false);
            alarm_image3.gameObject.SetActive(false);
        }
    }
    
    public void AcceptBtnOnClick()
    {
        StartCoroutine(FriendListDB());
    }

    IEnumerator FriendListDB()
    {
        string str = "true";
        WWWForm form = new WWWForm();
        form.AddField("to_user_idPost", arr[3]);
        form.AddField("from_user_idPost", user_id);
        form.AddField("are_we_friendPost", str);
        Debug.Log(arr[3]);
        Debug.Log(user_id);
        Debug.Log(str);
        UnityWebRequest www = UnityWebRequest.Post(FriendApplyAcceptURL, form);

        yield return www.SendWebRequest();
        if (www.downloadHandler.text =="1") //ģ����û �޾Ƽ� ��Ͽ� ����
        {
            Debug.Log("ģ����� �߰� �Ϸ�");
            scroll_view.gameObject.SetActive(false);
        }
        else
            Debug.Log(www.downloadHandler.text);
    }
}
