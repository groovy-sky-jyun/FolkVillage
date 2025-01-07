using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;
using Text = UnityEngine.UI.Text;

public class FetchTableNumber : MonoBehaviour
{
    public GameObject prefab_you;
    public GameObject prefab_me;
    public GameObject prefab_heart_you;
    public GameObject prefab_heart_me;
    public Transform parent;
    int table_number;
    int message_count;
    string FriendIDURL = "http://localhost/folkVillage/phoneFriend/friendID.php";
    string MessageNumberDB = "http://localhost/folkVillage/phoneMessage/messageNumber.php";
    string MessageRecordDB = "http://localhost/folkVillage/phoneMessage/messageRecord.php";
    string MessageUpdateDB = "http://localhost/folkVillage/phoneMessage/messageRecordUpdate.php";
    string[][] recordTable;
    string[][] recordTable2;
    public GameObject contentObj;
    string friend_id;
    string user_id;
    string name;
    string[] str2;

    //ȭ�� Ȱ��ȭ
    //1. ��ȭ ��� ���̵� ������
    //2. db table number ������ 
    private void OnEnable()
    {
       user_id=PlayerPrefs.GetString("user_id");
       

        //���� content�� �ڽ� ������Ʈ(prefab)�� 1���̻��̶�� �� ����
        int count = contentObj.gameObject.transform.childCount;
        if (count > 0)//������ ������ prefab�� �ִٴ� ��
        {
            for (int i = 0; i < count; i++)
            {
                GameObject destory_obj = contentObj.gameObject.transform.GetChild(i).gameObject;
                Destroy(destory_obj);
            }
        }

        //ģ�� ���̵� ���� �� ����
        StartCoroutine(FriendDB(name));
       
       
    }
    
    IEnumerator FriendDB(string friendNickName)
    {
        WWWForm form = new WWWForm();
        form.AddField("nicknamePost", friendNickName);


        UnityWebRequest www = UnityWebRequest.Post(FriendIDURL, form);

        yield return www.SendWebRequest();
        string text = www.downloadHandler.text;
        if (text != "fail")
        {
            PlayerPrefs.SetString("FriendID", text);
            friend_id = text;
            //table number��  message_count �����´�.
            StartCoroutine(FetchListDB(user_id, friend_id));
            Debug.Log("friend id : " + text);
        }
        else
            Debug.Log("FriendID �������� ����");
       
    }

    IEnumerator FetchListDB(string user_id, string friend_id) {

        WWWForm form = new WWWForm(); 
        form.AddField("userIDPost", user_id);
        form.AddField("friendIDPost", friend_id);


        UnityWebRequest www = UnityWebRequest.Post(MessageNumberDB, form);

        yield return www.SendWebRequest();
        string text = www.downloadHandler.text;
        if (text != "fail")
        {
            int Trimresult = 0;
            string[] arr = text.Split(',');
   
            int.TryParse(arr[0], out Trimresult);
            if(Trimresult > 0)
            {
                table_number = Trimresult;
            }
            int.TryParse(arr[1], out Trimresult);
            if (Trimresult >= 0)
            {
                message_count = Trimresult;
            }
   
            // message_count�� 0�̻��̸� ���� ����� �����Ƿ� record prefab �����ϴ� �Լ� �ۼ�
            //messagerecordDB���� table number�� ���� ���� �� �����´�.
            if(message_count > 0)
            {
                StartCoroutine(FetchRecordtDB(table_number, user_id, friend_id));
            }
            
        }
        else
            Debug.Log("message number �������� ����");
    }

    IEnumerator FetchRecordtDB(int number,string user_id, string friend_id)
    {

        WWWForm form = new WWWForm();
        form.AddField("numberPost", number);
        

        UnityWebRequest www = UnityWebRequest.Post(MessageRecordDB, form);

        yield return www.SendWebRequest();
        string text = www.downloadHandler.text;
        Debug.Log("messageRecord Test: " + text);
        if (text != "fail")
        {
            //str[0]="id,text,time,order,check" ������ �� �ִ�.
            string[]str=text.Split("/");
            recordTable = new string[str.Length - 1][];
            for (int i=0;i< message_count; i++)
            {
                //arr[0]=sender_id, arr[1]=message_text, arr[2]=message_time, arr[3]=message_order, arr[4]=read_check
                string[] arr = str[i].Split(',');
               recordTable[i] = arr; //message_order������ �迭�� �߰�/default=1�̶� -1����
                
            }//int.Parse(arr[3])-1
        }
        else
            Debug.Log("message number �������� ����");

        for (int i = 0; i < message_count; i++)
        {
            recordTable[i][0] = recordTable[i][0].Trim();
            Debug.Log(recordTable[i][0]);
            if (recordTable[i][0]== user_id)//���� ���� ������ ����
            {
                if (recordTable[i][1] == "sendHeart_gift")//���� ���� ���� ������
                {
                    GameObject instance_gift = Instantiate(prefab_heart_me, parent);
                }
                else//���� ���� �޽��� ������
                {
                    GameObject instance = Instantiate(prefab_me, parent);
                    instance.GetComponentInChildren<Text>().text = recordTable[i][1];//���� ���� �Է�
                }
            }
               
            else
            {
                if (recordTable[i][1] == "sendHeart_gift")//��밡 ���� ���� ������
                {
                    GameObject instance_gift = Instantiate(prefab_heart_you, parent);
                }
                else
                {//��밡 ���� �޽��� ������
                    GameObject instance = Instantiate(prefab_you, parent);
                    instance.GetComponentInChildren<Text>().text = recordTable[i][1];//���� ���� �Է�
                }
            }
           
        }
        ////////////////////�⺻���� �ٳ���///////////////////////////
        CheckDB();
    }

    private void CheckDB()
    {
        
        StartCoroutine(CheckRecordDB(table_number, user_id));
    }
    IEnumerator CheckRecordDB(int number, string user_id)
    {
        while (true)
        {
            WWWForm form = new WWWForm();
            form.AddField("numberPost", number);
            form.AddField("userIDPost", user_id);

            UnityWebRequest www = UnityWebRequest.Post(MessageUpdateDB, form);
            //Debug.Log("CheckRecordDB start");
            yield return www.SendWebRequest();
            string text = www.downloadHandler.text;
            text=text.Trim();

            if (text != "fail"&&text!="\n")
            {
               
                //str[0]="sender_id,text,time,order,check" ������ �� �ִ�.
                str2 = text.Split("/");

                Debug.Log("Str2 test: " + str2[0]+ "str2.length(): "+(str2.Length-1));
                recordTable2 = new string[str2.Length-1][];
                for (int i = 0; i < str2.Length-1; i++)
                {
                    //arr[0]=id, arr[1]=text, arr[2]=time, arr[3]=order, arr[4]=check
                    string[] arr = str2[i].Split(',');
                    
                    recordTable2[i] = arr; //message_order������ �迭�� �߰�/default=1�̶� -1����
                }
               
                for (int i = 0; i < str2.Length - 1; i++)
                {
                    //���� ���� ������ ������ ����
                   GameObject instance2 = Instantiate(prefab_you, parent);
                   instance2.GetComponentInChildren<Text>().text = recordTable2[i][1];//���� ���� �Է�
                }
                text = "fail";
            }
            else
                Debug.Log("message number �������� ����");
        }
    }
}
