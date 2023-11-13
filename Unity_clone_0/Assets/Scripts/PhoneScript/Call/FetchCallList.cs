using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;
using Text = UnityEngine.UI.Text;

public class FetchCallList : MonoBehaviour
{
    public GameObject prefab_list;
    string user_id;
    public GameObject contentObj;
    string CallListDB = "http://localhost/callList.php";
    string FriendinfoDB = "http://localhost/friendNickname.php";
    string[][] listTable;
    public Transform parent;
    public GameObject hint;

    //ȭ�� Ȱ��ȭ�Ǹ� table list �����´� -> messagelist DB���� ���� �����ͼ� �����ش�.
    private void OnEnable()
    {
       user_id=PlayerPrefs.GetString("user_id");
      
        //���� Scroll Viewport�� Content�� �ڽ� ������Ʈ(prefab)�� 1���̻��̶�� �� ����
        int count = contentObj.gameObject.transform.childCount;
        if (count > 0)//������ ������ prefab�� �ִٴ� ��
        {
            for (int i = 0; i < count; i++)
            {
                GameObject destory_obj = contentObj.gameObject.transform.GetChild(i).gameObject;
                Destroy(destory_obj);
            }
        }

        StartCoroutine(FetchListDB(user_id));
    }
    
   

    IEnumerator FetchListDB(string user_id)
    {
        WWWForm form = new WWWForm();
        form.AddField("userIDPost", user_id);
        UnityWebRequest www = UnityWebRequest.Post(CallListDB, form);
        yield return www.SendWebRequest();

        string text = www.downloadHandler.text;
        if (text != "fail")
        {   //��Ʈ ���� ������
            hint.SetActive(false);
            //str[0]="number,user1_id,user2_id,accept,calltime" ������ �� �ִ�.
            string[]str=text.Split("/");
            listTable = new string[str.Length-1][];
            for (int i=0;i< str.Length-1; i++)
            {
                //arr[0]=number, arr[1]=sender_id, arr[2]=receiver_id, arr[3]=accept, arr[4]=calltime
                string[] arr = str[i].Split(',');
                listTable[i] = arr;
            }
            StartCoroutine(PreFabSetting(listTable));
        }
        else
            Debug.Log("message number �������� ����");

        
    }
    
    IEnumerator PreFabSetting(string[][] list_table)
    {//�г��� �����ͼ� ������ ����
     //arr[0]=number, arr[1]=sender_id, arr[2]=receiver_id, arr[3]=accept, arr[4]=calltime

        WWWForm form = new WWWForm();
        for(int i=0;i< list_table.Length;i++)
        {
            if (list_table[i][1] != user_id)
            {//sender_id�� ģ�� ���̵� -> ģ���� ������ ��ȭ�� 
                form.AddField("friend_idPost", list_table[i][1]);
                Debug.Log("friend_id: " + list_table[i][1]);
            }
            else
            {//receuver_id�� ģ�� ���̵� -> ���� ģ������ ��
                form.AddField("friend_idPost", list_table[i][2]);
                Debug.Log("friend_id: " + list_table[i][2]);
            }

            UnityWebRequest www = UnityWebRequest.Post(FriendinfoDB, form);
            yield return www.SendWebRequest();

            //ģ�� �г��� userinfo DB���� ������
            string nickname = www.downloadHandler.text;

            if (nickname != "fail")
            {
                Debug.Log("Prefab ����");
                //parent obj�� �ڽ����� ������ ����
                GameObject instance = Instantiate(prefab_list, parent);

                GameObject nickname_obj = instance.gameObject.transform.GetChild(1).gameObject;
                nickname_obj.GetComponent<Text>().text = nickname;

                GameObject time_obj = instance.gameObject.transform.GetChild(2).gameObject;
                time_obj.GetComponent<Text>().text = list_table[i][4];

                GameObject receive_obj = instance.gameObject.transform.GetChild(3).gameObject;
                GameObject transmit_obj = instance.gameObject.transform.GetChild(4).gameObject;
                GameObject absensce_obj = instance.gameObject.transform.GetChild(5).gameObject;

                if (list_table[i][3] == "0")//������ ������
                {
                    receive_obj.SetActive(false);
                    transmit_obj.SetActive(false);
                    absensce_obj.SetActive(true);
                }
                else
                {
                    if (list_table[i][1] == user_id)//��ȭ �� ����� �����
                    {
                        receive_obj.SetActive(false);
                        transmit_obj.SetActive(true);
                        absensce_obj.SetActive(false);
                    }
                    else
                    {
                        receive_obj.SetActive(true);
                        transmit_obj.SetActive(false);
                        absensce_obj.SetActive(false);
                    }
                }
     
            }
            else
                Debug.Log("nickname �������� ����");
        }

    }
}
