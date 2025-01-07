using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Photon.Pun;

public class FriendList : MonoBehaviour
{
    public GameObject prefab;
    public Transform parent;
    public Text alarm_text;
    public GameObject contentObj;

    private string user_id;
    private string FriendSearchURL = "http://localhost/folkVillage/phoneFriend/friendList.php";

    private void Start()
    {
        alarm_text.gameObject.SetActive(true);
    }

    //ģ����� ��ư Ŭ�� -> ģ�� ����Ʈ ��������
    public void FriendBtnOnClick()
    {
        user_id = PlayerPrefs.GetString("user_id");
        //���� content�� �ڽ� ������Ʈ(prefab)�� 1���̻��̶�� �� ����
        int count = contentObj.gameObject.transform.childCount;
        if (count > 0)//������ ������ prefab�� �ִٴ� ��
        {
            for(int i = 0; i < count; i++)
            {
               GameObject destory_obj= contentObj.gameObject.transform.GetChild(i).gameObject;
                Destroy(destory_obj);
            }
        }

        StartCoroutine(FriendDB());
    }
    IEnumerator FriendDB()
    {
        WWWForm form = new WWWForm();
        form.AddField("user_idPost", user_id);

        // ģ�� ��� �� �ִ��� Ȯ�� �� ��������
        UnityWebRequest www = UnityWebRequest.Post(FriendSearchURL, form);

        yield return www.SendWebRequest();
        string text = www.downloadHandler.text;
       
        Debug.Log(text);
       if (text != "null")
        {
            alarm_text.gameObject.SetActive(false);
            string[] arr = text.Split(',');
           for(int i=0; i<arr.Length-1; i++)
            {
                GameObject instance = Instantiate(prefab, parent); // �θ� ����
                instance.GetComponentInChildren<Text>().text=arr[i++];//¦���� �г���
                if (arr[i] == "0") //��Ʈ ���� �Ⱥ��� ����
                {
                    instance.transform.GetChild(0).gameObject.SetActive(true);
                    instance.transform.GetChild(1).gameObject.SetActive(false);
                }
                else
                {
                    instance.transform.GetChild(0).gameObject.SetActive(false);
                    instance.transform.GetChild(1).gameObject.SetActive(true);
                    instance.transform.GetChild(1).gameObject.GetComponent<Button>().interactable = false;
                }
            }
        }
        else
        {
            Debug.Log("ģ�� ��� ����");
            alarm_text.gameObject.SetActive(true);
        }
       

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
