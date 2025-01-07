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

    //화면 활성화
    //1. 대화 상대 아이디 가져옴
    //2. db table number 가져옴 
    private void OnEnable()
    {
       user_id=PlayerPrefs.GetString("user_id");
       

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
            Debug.Log("friend id : " + text);
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
   
            // message_count가 0이상이면 문자 기록이 있으므로 record prefab 생성하는 함수 작성
            //messagerecordDB에서 table number가 같은 행을 다 가져온다.
            if(message_count > 0)
            {
                StartCoroutine(FetchRecordtDB(table_number, user_id, friend_id));
            }
            
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
        Debug.Log("messageRecord Test: " + text);
        if (text != "fail")
        {
            //str[0]="id,text,time,order,check" 문장이 들어가 있다.
            string[]str=text.Split("/");
            recordTable = new string[str.Length - 1][];
            for (int i=0;i< message_count; i++)
            {
                //arr[0]=sender_id, arr[1]=message_text, arr[2]=message_time, arr[3]=message_order, arr[4]=read_check
                string[] arr = str[i].Split(',');
               recordTable[i] = arr; //message_order순으로 배열에 추가/default=1이라서 -1해줌
                
            }//int.Parse(arr[3])-1
        }
        else
            Debug.Log("message number 가져오기 실패");

        for (int i = 0; i < message_count; i++)
        {
            recordTable[i][0] = recordTable[i][0].Trim();
            Debug.Log(recordTable[i][0]);
            if (recordTable[i][0]== user_id)//내가 보낸 프리팹 전송
            {
                if (recordTable[i][1] == "sendHeart_gift")//내가 보낸 선물 프리팹
                {
                    GameObject instance_gift = Instantiate(prefab_heart_me, parent);
                }
                else//내가 보낸 메시지 프리팹
                {
                    GameObject instance = Instantiate(prefab_me, parent);
                    instance.GetComponentInChildren<Text>().text = recordTable[i][1];//문자 내용 입력
                }
            }
               
            else
            {
                if (recordTable[i][1] == "sendHeart_gift")//상대가 보낸 선물 프리팹
                {
                    GameObject instance_gift = Instantiate(prefab_heart_you, parent);
                }
                else
                {//상대가 보낸 메시지 프리팹
                    GameObject instance = Instantiate(prefab_you, parent);
                    instance.GetComponentInChildren<Text>().text = recordTable[i][1];//문자 내용 입력
                }
            }
           
        }
        ////////////////////기본세팅 다끝남///////////////////////////
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
               
                //str[0]="sender_id,text,time,order,check" 문장이 들어가 있다.
                str2 = text.Split("/");

                Debug.Log("Str2 test: " + str2[0]+ "str2.length(): "+(str2.Length-1));
                recordTable2 = new string[str2.Length-1][];
                for (int i = 0; i < str2.Length-1; i++)
                {
                    //arr[0]=id, arr[1]=text, arr[2]=time, arr[3]=order, arr[4]=check
                    string[] arr = str2[i].Split(',');
                    
                    recordTable2[i] = arr; //message_order순으로 배열에 추가/default=1이라서 -1해줌
                }
               
                for (int i = 0; i < str2.Length - 1; i++)
                {
                    //상대방 문자 보내는 프리팹 생성
                   GameObject instance2 = Instantiate(prefab_you, parent);
                   instance2.GetComponentInChildren<Text>().text = recordTable2[i][1];//문자 내용 입력
                }
                text = "fail";
            }
            else
                Debug.Log("message number 가져오기 실패");
        }
    }
}
