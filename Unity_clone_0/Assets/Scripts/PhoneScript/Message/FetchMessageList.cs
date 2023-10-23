using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;
using Text = UnityEngine.UI.Text;

public class FetchMessageList : MonoBehaviour
{
    public GameObject prefab_list;
    string user_id;
    public GameObject contentObj;
    string MessageListDB = "http://localhost/messageList.php";
    string MessageRecordDB = "http://localhost/messageLastRecord.php";
    string FriendinfoDB = "http://localhost/friendNickname.php";
    string[][] listTable;
    public Transform parent;
    public GameObject hint;

    //화면 활성화되면 table list 가져온다 -> messagelist DB에서 내용 가져와서 보여준다.
    private void OnEnable()
    {
       user_id=PlayerPrefs.GetString("user_id");
      
        //만약 Scroll Viewport의 Content의 자식 오브젝트(prefab)가 1개이상이라면 다 지움
        int count = contentObj.gameObject.transform.childCount;
        if (count > 0)//이전의 생성된 prefab이 있다는 뜻
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
        UnityWebRequest www = UnityWebRequest.Post(MessageListDB, form);
        yield return www.SendWebRequest();

        string text = www.downloadHandler.text;
       
        if (text != "fail")
        {   //힌트 문구 가리기
            hint.SetActive(false);
            //str[0]="number,user1_id,user2_id,message_count" 문장이 들어가 있다.
            string[]str=text.Split("/");
            listTable = new string[str.Length-1][];
            for (int i=0;i< str.Length-1; i++)
            {
                //arr[0]=number, arr[1]=user1_id, arr[2]=user2_id, arr[3]=message_count
                string[] arr = str[i].Split(',');
                listTable[i] = arr; 
            }
        }
        else
            Debug.Log("message number 가져오기 실패");

        StartCoroutine(FetchRecordDB(listTable, user_id));
    }
    
    IEnumerator FetchRecordDB(string[][] listTable, string user_id)
    {
       
        for (int i=0; i<listTable.Length;i++) { //list number마다 recordDB접근
            WWWForm form = new WWWForm();
            form.AddField("table_numberPost", listTable[i][0]);
            form.AddField("lastMessage_numberPost", listTable[i][3]);
            UnityWebRequest www = UnityWebRequest.Post(MessageRecordDB, form);
            yield return www.SendWebRequest();
            
            string text = www.downloadHandler.text;
            text = text.Replace("\n", "");
            text = text.Trim();
            Debug.Log("fetchRecord: " + text);
            if (text != "fail")
            {//str[0]=sender_id, str[1]=receuver_id, str[2]=message_text, str[3]=message_time, str[4]=read_check
                string[]str=text.Split(",");
               
                StartCoroutine(PreFabSetting(str,user_id));
            }
            else
                Debug.Log("message record 가져오기 실패");
        }
    }
    IEnumerator PreFabSetting(string[]str,string user_id)
    {//닉네임 가져와서 프리팹 생성
     //str[0]=sender_id, str[1]=receuver_id, str[2]=message_text, str[3]=message_time, str[4]=read_check
        
        WWWForm form = new WWWForm();
        if (str[0] != user_id)
        {//sender_id가 친구 아이디 
            form.AddField("friend_idPost", str[0]);
            Debug.Log("friend_id: " + str[0]);
        }
        else
        {//receuver_id가 친구 아이디
            form.AddField("friend_idPost", str[1]);
            Debug.Log("friend_id: " + str[1]);
        }

        UnityWebRequest www = UnityWebRequest.Post(FriendinfoDB, form);
        yield return www.SendWebRequest();

        //친구 닉네임 userinfo DB에서 가져옴
        string nickname = www.downloadHandler.text;
        Debug.Log(nickname);
        if (nickname != "fail")
        {
            Debug.Log("Prefab 생성");
            //parent obj의 자식으로 프리팹 생성
            GameObject instance = Instantiate(prefab_list, parent);
            GameObject nickname_obj = instance.gameObject.transform.GetChild(2).gameObject;
            nickname_obj.GetComponent<Text>().text=nickname;
            GameObject message_obj = instance.gameObject.transform.GetChild(3).gameObject;
            message_obj.GetComponent<Text>().text = str[2];
            GameObject time_obj = instance.gameObject.transform.GetChild(4).gameObject;
            time_obj.GetComponent<Text>().text = str[3];
            GameObject alarm_obj = instance.gameObject.transform.GetChild(1).gameObject;
            if (str[4] == "1")
            {
                alarm_obj.SetActive(false);
            }
            else
            {
                alarm_obj.SetActive(true);
            }
        }
        else
            Debug.Log("nickname 가져오기 실패");

    }
}
