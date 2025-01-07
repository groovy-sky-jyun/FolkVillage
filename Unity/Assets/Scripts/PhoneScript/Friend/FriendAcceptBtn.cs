using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Networking;

public class FriendAcceptBtn : MonoBehaviour
{
    public Text nickname;
    public Button acceptBtn;

    private string user_id;
    private string friend_id;
    private string getFriend_id = "http://localhost/folkVillage/phoneFriend/friendID.php";
    private string updateApplyfriend = "http://localhost/folkVillage/phoneFriend/friendApplyAccept.php";
    void Start()
    {
        user_id = PlayerPrefs.GetString("user_id");
        acceptBtn.onClick.AddListener(AcceptOnClick);
    }

    public void AcceptOnClick()
    {
        StartCoroutine(setApplyFriendDB());
    }
    IEnumerator setApplyFriendDB()
    {
        string nicknameText = nickname.text;
        // 해당프리팹의 닉네임으로 친구의 아이디를 가져온다.
        if (nicknameText != null)
        {
            WWWForm form = new WWWForm();
            form.AddField("nicknamePost", nicknameText);
            UnityWebRequest www = UnityWebRequest.Post(getFriend_id, form);

            yield return www.SendWebRequest();
            string text = www.downloadHandler.text;
            if (text != "fail")
            {
                friend_id = text.Trim();
            }
            else
            {
                Debug.Log("친구 신청자 아이디 가져오기 실패");
            }
        }
        else
        {
            Debug.Log("친구 신청 프리팹: 닉네임 가져올 수 없음");
        }
        // applyfriend의 are we friend = 1 로 변경
        // friendlist에 항목 추가
        // messagelist에 default 항목 추가
        if (friend_id != null)
        {
            WWWForm form = new WWWForm();
            //친구가 나에게 신청한것을 받는것이므로 friend가 to user
            form.AddField("to_user_idPost", friend_id);
            form.AddField("from_user_idPost", user_id);
            UnityWebRequest www = UnityWebRequest.Post(updateApplyfriend, form);

            yield return www.SendWebRequest();
            string text = www.downloadHandler.text;
            if (text != "fail")
            {
                Debug.Log("친구 수락 성공");
            }
            else
            {
                Debug.Log("친구 수락 실패");
            }
        }
    }
}
