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
    public GameObject applyAlarm;

    private string user_id;
    private string FriendSearchURL = "http://localhost/folkVillage/phoneFriend/friendList.php";
    private string ApplyFriend = "http://localhost/folkVillage/phoneFriend/friendApplyCheckList.php";

    private void Start()
    {
        user_id = PlayerPrefs.GetString("user_id");
        alarm_text.gameObject.SetActive(true);
        applyAlarm.SetActive(false);
        StartCoroutine(CheckApply());
    }
    IEnumerator CheckApply()
    {
        WWWForm form = new WWWForm();
        form.AddField("idPost", user_id);

        // 친구 목록 이 있는지 확인 후 가져오기
        UnityWebRequest www = UnityWebRequest.Post(ApplyFriend, form);

        yield return www.SendWebRequest();
        string text = www.downloadHandler.text;

        Debug.Log(text);
        if (text != "null") //친구 신청이 있다는 뜻
        {
            applyAlarm.SetActive(true);
        }
        else if(text == "fail")
        {
            Debug.Log("failed to check the applyfriend list");
        }
    }

    //친구목록 버튼 클릭 -> 친구 리스트 가져오기
    public void FriendBtnOnClick()
    {
        //만약 content의 자식 오브젝트(prefab)가 1개이상이라면 다 지움
        int count = contentObj.gameObject.transform.childCount;
        if (count > 0)//이전의 생성된 prefab이 있다는 뜻
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

        // 친구 목록 이 있는지 확인 후 가져오기
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
                GameObject instance = Instantiate(prefab, parent); // 부모 지정
                instance.GetComponentInChildren<Text>().text=arr[i++];//짝수는 닉네임
                if (arr[i] == "0") //하트 아직 안보낸 상태
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
            Debug.Log("친구 목록 없음");
            alarm_text.gameObject.SetActive(true);
        }
       

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
