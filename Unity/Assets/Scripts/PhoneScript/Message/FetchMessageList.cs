using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using static FetchMessageList;
using static System.Net.Mime.MediaTypeNames;
using Text = UnityEngine.UI.Text;

public class FetchMessageList : MonoBehaviour
{
    public GameObject prefab;
    public Transform parent;
    public GameObject content;
    public Text alarm_text;

    private string user_id;
    private List<string[]> listData = new List<string[]>();
    private List<string[]> recordData = new List<string[]>();
    private string getMessage_list = "http://localhost/folkVillage/phoneMessage/messageList.php";
    private string getMessage_lastest = "http://localhost/folkVillage/phoneMessage/messageLastRecord.php";
    private string getFriend_nickname = "http://localhost/folkVillage/phoneFriend/friendNickname.php";
    [System.Serializable]
    public class MessasgeListData
    {
        public string table_number;
        public string message_count;
    }

    [System.Serializable]
    public class ResponseData
    {
        public string status;
        public MessasgeListData[] data;
    }

    [System.Serializable]
    public class MessasgeRecordData
    {
        public string sender_id;
        public string receiver_id;
        public string message_txt;
        public string message_time;
        public string read_check;
    }

    [System.Serializable]
    public class ResponseRecordData
    {
        public string status;
        public MessasgeRecordData[] data;
    }

    //화면 활성화되면 table list 가져온다 -> 프리팹 생성
    private void OnEnable()
    {
       user_id=PlayerPrefs.GetString("user_id");
        alarm_text.gameObject.SetActive(true);

        //만약 Scroll Viewport의 Content의 자식 오브젝트(prefab)가 1개이상이라면 다 지움
        int count = content.gameObject.transform.childCount;
        if (count > 0)//이전의 생성된 prefab이 있다는 뜻
        {
            for (int i = 0; i < count; i++)
            {
                GameObject destory_obj = content.gameObject.transform.GetChild(i).gameObject;
                Destroy(destory_obj);
            }
        }

        StartCoroutine(FetchListDB(user_id));
    }
    

    IEnumerator FetchListDB(string user_id)
    {
        // messagelist 가져오기
        string jsonData = "{\"userIDPost\": \"" + user_id + "\"}";

        UnityWebRequest www = new UnityWebRequest(getMessage_list, "POST");
        byte[] bodyRow = System.Text.Encoding.UTF8.GetBytes(jsonData);
        www.uploadHandler = new UploadHandlerRaw(bodyRow);
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            string jsonResponse = (www.downloadHandler.text).Trim();
            ResponseData responseData = JsonUtility.FromJson<ResponseData>(jsonResponse);
           
            if (responseData.status == "success")
            {
                listData.Clear();
                foreach (MessasgeListData messageData in responseData.data)
                {
                    string[] arr = new string[2]; //[0]:테이블 번호 [1]:마지막 메시지번호
                    arr[0] = messageData.table_number;
                    arr[1] = messageData.message_count;
                    listData.Add(arr);
                }
           
                StartCoroutine(FetchRecordDB());
            }
            else
            {
                Debug.Log("fail to get a messageList");
            }
        }
        else
        {
            Debug.Log("Access fail");
        }
    }
    
    IEnumerator FetchRecordDB()
    {
        //각 table number마다 마지막 문자 메시지 가져오기
        //data => [0]:테이블 번호 [1]:대화상대 [2]:마지막 메시지번호

        recordData.Clear();
        for (int i = 0; i < listData.Count; i++)
        {
            string jsonData = "{\"tableNumberPost\": \"" + listData[i][0] + "\", \"lastCountPost\": \"" + listData[i][1] + "\"}";

            UnityWebRequest www = new UnityWebRequest(getMessage_lastest, "POST");
            byte[] bodyRow = System.Text.Encoding.UTF8.GetBytes(jsonData);
            www.uploadHandler = new UploadHandlerRaw(bodyRow);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                string jsonResponse = (www.downloadHandler.text).Trim();
                ResponseRecordData responseData = JsonUtility.FromJson<ResponseRecordData>(jsonResponse);
                if (responseData.status == "success")
                {
                    
                    foreach (MessasgeRecordData messageData in responseData.data)
                    {
                        string[] arr = new string[4]; //[0]:상대아이디 [1]:문자내용 [2]:문자시간 [3]:읽었는지
                        if (messageData.sender_id == user_id)
                        {
                            arr[0] = messageData.receiver_id;
                        }
                        else
                        {
                            arr[0] = messageData.sender_id;
                        }
                        arr[1] = messageData.message_txt;
                        arr[2] = messageData.message_time;
                        arr[3] = messageData.read_check;

                        recordData.Add(arr);
                    }

                    StartCoroutine(PreFabSetting());
                }
                else
                {
                    Debug.Log("fail to get a messageRecordLastest");
                }
            }
            else
            {
                Debug.Log("Access fail");
            }
        }
    }


    IEnumerator PreFabSetting()
    {
        alarm_text.gameObject.SetActive(false);

        //닉네임 가져와서 프리팹 생성
        //recordData => [0]:상대아이디 [1]:문자내용 [2]:문자시간 [3]:읽었는지
        for (int i=0;  i<recordData.Count; i++) {
            WWWForm form = new WWWForm();
            form.AddField("friendIDPost", recordData[i][0]);
            UnityWebRequest www = UnityWebRequest.Post(getFriend_nickname, form);

            yield return www.SendWebRequest();
            string nickname = www.downloadHandler.text;

            GameObject instance = Instantiate(prefab, parent);

            //프리팹 닉네임 설정
            Transform nicknameTransform = instance.transform.Find("NickName");
            if (nicknameTransform != null)
            {
                Text nicknameText = nicknameTransform.GetComponent<Text>();
                nicknameText.text = nickname;
            } else { Debug.Log("메시지 리스트 프리팹: fail to found nickname transform"); }

            //프리팹 문자 내용 설정
            Transform textTransform = instance.transform.Find("LastMessageText");
            if (textTransform != null)
            {
                Text message = textTransform.GetComponent<Text>();
                message.text = recordData[i][1];
            }
            else { Debug.Log("메시지 리스트 프리팹: fail to found messagetext transform"); }

            //프리팹 문자 시간 설정
            Transform timeTransform = instance.transform.Find("LastMessageTime");
            if (timeTransform != null)
            {
                Text time = timeTransform.GetComponent<Text>();
                time.text = recordData[i][2];
            }
            else { Debug.Log("메시지 리스트 프리팹: fail to found messagetime transform"); }

            //프리팹 읽음 표시 설정
            Transform alarmTransform = instance.transform.Find("MessageAlarm");
            if (alarmTransform != null)
            {
                if (recordData[i][3] == "0")
                    alarmTransform.gameObject.SetActive(true);
                else 
                    alarmTransform.gameObject.SetActive(false);
            }
            else { Debug.Log("메시지 리스트 프리팹: fail to found alarm transform"); }
        } 
    }
}
