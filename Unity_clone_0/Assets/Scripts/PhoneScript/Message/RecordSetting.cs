using Photon.Pun.Demo.Cockpit;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;
using Text = UnityEngine.UI.Text;

public class RecordSetting : MonoBehaviour
{
    public GameObject content;
    public GameObject prefab_me;
    public GameObject prefab_you;
    public GameObject prefabGift_me;
    public GameObject prefabGift_you;
    public Transform parent;

    private string friendNameText;
    private int tableNum;
    private int messageCount;
    private string user_id;
    private string friend_id;
    private string getFriend_id = "http://localhost/folkVillage/phoneFriend/friendID.php";
    private string getTable_info = "http://localhost/folkVillage/phoneMessage/messageNumber.php";
    private string getMessage_record = "http://localhost/folkVillage/phoneMessage/messageRecord.php";

    //Ȱ��ȭ �Ǹ� ����
    private void OnEnable()
    {
        user_id = PlayerPrefs.GetString("user_id");

        //���� Scroll Viewport�� Content�� �ڽ� ������Ʈ(prefab)�� 1���̻��̶�� �� ����
        int count = content.gameObject.transform.childCount;
        if (count > 0)//������ ������ prefab�� �ִٴ� ��
        {
            for (int i = 0; i < count; i++)
            {
                GameObject destory_obj = content.gameObject.transform.GetChild(i).gameObject;
                Destroy(destory_obj);
            }
        }

        StartCoroutine(getFriendId());
    }


    //���̺� �ѹ��� �����´�. + count����
    //count�� > 0 �̶�� �޽��� ������ ����
    IEnumerator getFriendId()
    {
        //�г������� ģ�� ���̵� �����´�.
        friendNameText = PlayerPrefs.GetString("friend_nickname");
        if (friendNameText != null)
        {
            WWWForm form = new WWWForm();
            form.AddField("nicknamePost", friendNameText);
            UnityWebRequest www = UnityWebRequest.Post(getFriend_id, form);

            yield return www.SendWebRequest();
            string text = www.downloadHandler.text;
            if (text != "fail")
            {
                friend_id = text.Trim();
                PlayerPrefs.SetString("friend_id", friend_id);
                StartCoroutine(getTableInfo());
            }
            else
            {
                Debug.Log("ģ�� ��û�� ���̵� �������� ����");
            }
        }
        else
        {
            Debug.Log("ģ�� �г��� ������ �� ����");
        }
    }

    IEnumerator getTableInfo() { 
        //���̺� number, count��������
        WWWForm form = new WWWForm();
        form.AddField("userIDPost", user_id);
        form.AddField("friendIDPost", friend_id);
        UnityWebRequest www = UnityWebRequest.Post(getTable_info, form);

        yield return www.SendWebRequest();
        string text = www.downloadHandler.text;
        Debug.Log(text);
        if (text != "fail")
        {
            int result = 0;
            string[] arr = text.Split(',');
            int.TryParse(arr[0], out result);
            if (result >= 0)
            {
               tableNum = result;
               PlayerPrefs.SetInt("table_number", tableNum);
            }
            int.TryParse(arr[1], out result);
            if (result >= 0)
            {
                messageCount = result;
                PlayerPrefs.SetInt("message_count", messageCount);
            }

            //������ ��ȭ�� ������ �ִٸ�
            if(messageCount > 0)
            {
                //���� ��ȭ�� ������ ����
                StartCoroutine(setMessageRecord());
            }
        }
        else
            Debug.Log("���̺� ��ȣ �������� ����");
    }

   
    IEnumerator setMessageRecord()
    {
        //�޽��� ������ ����
        string jsonData = "{\"tableNumber\": " + tableNum + "}";

        UnityWebRequest www = new UnityWebRequest(getMessage_record, "POST");
        byte[] bodyRow = System.Text.Encoding.UTF8.GetBytes(jsonData);
        www.uploadHandler = new UploadHandlerRaw(bodyRow);
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            string jsonResponse = (www.downloadHandler.text).Trim();
            Debug.Log(jsonResponse);
            ResponseData responseData = JsonUtility.FromJson<ResponseData>(jsonResponse);

            List<string[]> data = new List<string[]>();
            if (responseData.status == "success")
            {
                foreach (RecordData messageData in responseData.data)
                {
                    string[] arr = new string[2];
                    arr[0] = messageData.sender_id;
                    arr[1] = messageData.message_txt;
                    data.Add(arr);
                }
            }
            

            for(int i=0; i<data.Count; i++)
            {
                string[] arr = data[i];
            
                //���� ������ ���� ������
                if (arr[0] == user_id) {
                    if (arr[1] == "sendHeart_gift")
                    {
                        GameObject instance = Instantiate(prefabGift_me, parent);
                    }
                    else
                    {
                        GameObject instance = Instantiate(prefab_me, parent);
                        instance.GetComponentInChildren<Text>().text = arr[1];
                    }
                }
                else //���� ���� ������
                {
                    if (arr[1] == "sendHeart_gift")
                    {
                        GameObject instance = Instantiate(prefabGift_you, parent);
                    }
                    else
                    {
                        GameObject instance = Instantiate(prefab_you, parent);
                        instance.GetComponentInChildren<Text>().text = arr[1];
                    }
                    
                }
            }
        }
    }
}
[System.Serializable]
public class RecordData
{
    public string sender_id;
    public string message_txt;
}

[System.Serializable]
public class ResponseData
{
    public string status;
    public RecordData[] data;
}