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

        //이전에 생성한 prefab이 있다면 전부 삭제
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
        // 친구 신청 목록이 있는지 확인
        StartCoroutine(FriendApplyListCheck());
    }

    IEnumerator FriendApplyListCheck()
    {
        WWWForm form = new WWWForm();
        form.AddField("idPost", user_id);

        UnityWebRequest www = UnityWebRequest.Post(checkList, form);

        yield return www.SendWebRequest();
        string text = www.downloadHandler.text;

        //친구 신청이 있는 경우 프리팹 생성
        if(text != "null")
        {
            string[] arr = text.Split(',');
            for(int i = 0; i<arr.Length-1; i += 2) //, 뒤의 공백때문에 echo값이 배열3개가 나옴
            {
                // 프리팹 생성
                GameObject instance = Instantiate(prefab, parent);
                // 닉네임 설정
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
                    Debug.Log("친구신청 닉네임 객체 찾을 수 없음");
                }
                // 성별 설정
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
                    Debug.Log("친구신청 성별 이미지 객체 찾을 수 없음");
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
