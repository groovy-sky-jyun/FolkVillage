using System.Collections;
using System.Collections.Generic;
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
    public Transform parent;
    int table_number;
    int message_count;
    string FriendIDURL = "http://localhost/friendID.php";
    string MessageNumberDB = "http://localhost/messageNumber.php";
    string MessageRecordDB = "http://localhost/messageRecord.php";
    string MessageUpdateDB = "http://localhost/messageRecordUpdate.php";
    string[][] recordTable;
    string[][] recordTable2;
    public GameObject contentObj;
    string friend_id;
    string user_id;
    string name;
    string[] str2;

    //화면 활성화되면 table number 가져온다 -> messagerecord DB에서 내용 가져와서 보여준다.
    private void OnEnable()//누구와의 채팅목록인지 알아야하기 때문
    {
       user_id=PlayerPrefs.GetString("user_id");
       name = PlayerPrefs.GetString("messageFriend");

        //만약 content의 자식 오브젝트(prefab)가 1개이상이라면 다 지움
        int count = contentObj.gameObject.transform.childCount;
        if (count > 0)//이전의 생성된 prefab이 있다는 뜻
        {
            for (int i = 0; i < count; i++)
            {
                GameObject destory_obj = contentObj.gameObject.transform.GetChild(i).gameObject;
                Destroy(destory_obj);
            }
        }

        //친구 아이디 게임 내 저장
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
            //table number와  message_count 가져온다.
            StartCoroutine(FetchListDB(user_id, friend_id));
        }
        else
            Debug.Log("FriendID 가져오기 실패");
       
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
            string[] arr = text.Split(',');
            table_number = int.Parse(arr[0]);
            message_count = int.Parse(arr[1]);
            //record prefab 생성하는 함수 작성
            //messagerecordDB에서 table number가 같은 행을 다 가져온다.
            StartCoroutine(FetchRecordtDB(table_number, user_id, friend_id));
        }
        else
            Debug.Log("message number 가져오기 실패");
    }

    IEnumerator FetchRecordtDB(int number,string user_id, string friend_id)
    {

        WWWForm form = new WWWForm();
        form.AddField("numberPost", number);


        UnityWebRequest www = UnityWebRequest.Post(MessageRecordDB, form);

        yield return www.SendWebRequest();
        string text = www.downloadHandler.text;
        if (text != "fail")
        {
            //str[0]="id,text,time,order,check" 문장이 들어가 있다.
            string[]str=text.Split("/");
            recordTable = new string[message_count][];
            for (int i=0;i< message_count; i++)
            {
                //arr[0]=id, arr[1]=text, arr[2]=time, arr[3]=order, arr[4]=check
                string[] arr = str[i].Split(',');
               recordTable[i] = arr; //message_order순으로 배열에 추가/default=1이라서 -1해줌
            }//int.Parse(arr[3])-1
        }
        else
            Debug.Log("message number 가져오기 실패");

        for (int i = 0; i < message_count; i++)
        {
            if (recordTable[i][0]== user_id)//내가 보낸 프리팹 전송
            {
                GameObject instance = Instantiate(prefab_me, parent);
                instance.GetComponentInChildren<Text>().text = recordTable[i][1];//문자 내용 입력
            }
            else
            {
                GameObject instance = Instantiate(prefab_you, parent);
                instance.GetComponentInChildren<Text>().text = recordTable[i][1];//문자 내용 입력
            }
           
        }
        ////////////////////기본세팅 다끝남///////////////////////////
        CheckDB();
    }

    private void CheckDB()
    {
        
        StartCoroutine(CheckRecordDB(table_number, user_id, friend_id));
    }
    IEnumerator CheckRecordDB(int number, string user_id, string friend_id)
    {
        while (true)
        {
            WWWForm form = new WWWForm();
            form.AddField("numberPost", number);


            UnityWebRequest www = UnityWebRequest.Post(MessageUpdateDB, form);
            //Debug.Log("CheckRecordDB start");
            yield return www.SendWebRequest();
            string text = www.downloadHandler.text;
            text= text.Replace("\n","");
            text=text.Trim();

            if (text != "fail")
            {
               
                //str[0]="id,text,time,order,check" 문장이 들어가 있다.
                str2 = text.Split("/");

               
                recordTable2 = new string[str2.Length-1][];
                for (int i = 0; i < str2.Length-1; i++)
                {
                    //arr[0]=id, arr[1]=text, arr[2]=time, arr[3]=order, arr[4]=check
                    string[] arr = str2[i].Split(',');
                    
                    recordTable2[i] = arr; //message_order순으로 배열에 추가/default=1이라서 -1해줌
                }
               
                for (int i = 0; i < str2.Length - 1; i++)
                {
                    if (recordTable2[i][0] == user_id)//내가 보낸 프리팹 전송
                    {
                        GameObject instance2 = Instantiate(prefab_me, parent);
                        instance2.GetComponentInChildren<Text>().text = recordTable2[i][1];//문자 내용 입력
                    }
                    else if (recordTable2[i][0]== friend_id)
                    {
                        GameObject instance2 = Instantiate(prefab_you, parent);
                        instance2.GetComponentInChildren<Text>().text = recordTable2[i][1];//문자 내용 입력
                    }

                }
                text = "fail";
            }
            else
                Debug.Log("message number 가져오기 실패");
        }
    }
}
