using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.UIElements;
using WebSocketSharp;

public class FriendApplyList : MonoBehaviour
{
    public GameObject prefab;
    public Transform parent;
    public GameObject contentObj;
    public Text alarm_text;

    private string user_id;
    private string checkList = "http://localhost/folkVillage/phoneFriend/friendApplyCheckList.php";

    void Start()
    {
        user_id = PlayerPrefs.GetString("user_id");
        alarm_text.gameObject.SetActive(true);

        //������ ������ prefab�� �ִٸ� ���� ����
        int count = contentObj.gameObject.transform.childCount;
        if (count > 0)
        {
            for (int i = 0; i < count; i++)
            {
                GameObject destory_obj = contentObj.gameObject.transform.GetChild(i).gameObject;
                Destroy(destory_obj);
            }
        }
    }

    void Update()
    {
        // ģ�� ��û ����� �ִ��� Ȯ��
        StartCoroutine(FriendApplyListCheck());
    }

    IEnumerator FriendApplyListCheck()
    {
        WWWForm form = new WWWForm();
        form.AddField("idPost", user_id);

        UnityWebRequest www = UnityWebRequest.Post(checkList, form);

        yield return www.SendWebRequest();
        string text = www.downloadHandler.text;

        //ģ�� ��û�� �ִ� ��� ������ ����
        if(text != "null")
        {
            string[] arr = text.Split(',');
            for(int i = 0; i<arr.Length-1; i += 2) //, ���� ���鶧���� echo���� �迭3���� ����
            {
                // ������ ����
                GameObject instance = Instantiate(prefab, parent);
                // �г��� ����
                Transform nicknameObj = instance.transform.Find("NickName");
                if(nicknameObj != null)
                {
                    Text nickname = nicknameObj.GetComponent<Text>();
                    if(nickname != null)
                    {
                        nickname.text = arr[i];
                    }
                }
                else
                {
                    Debug.Log("ģ����û �г��� ��ü ã�� �� ����");
                }
                // ���� ����
                Transform boy = nicknameObj.Find("boy");
                Transform girl = nicknameObj.Find("girl");
                if (arr[i + 1] == "0" && boy != null && girl != null)
                {
                    boy.gameObject.SetActive(true);
                    girl.gameObject.SetActive(false);

                }
                else if(arr[i + 1] == "1" && boy != null && girl != null)
                {
                    boy.gameObject.SetActive(false);
                    girl.gameObject.SetActive(true);
                }
                else
                {
                    Debug.Log("ģ����û ���� �̹��� ��ü ã�� �� ����");
                }
            }
            alarm_text.gameObject.SetActive(false);
        }
        else
        {
            alarm_text.gameObject.SetActive(true);
        }
    }
}
