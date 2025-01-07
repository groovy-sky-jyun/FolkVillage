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
        // �ش��������� �г������� ģ���� ���̵� �����´�.
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
                Debug.Log("ģ�� ��û�� ���̵� �������� ����");
            }
        }
        else
        {
            Debug.Log("ģ�� ��û ������: �г��� ������ �� ����");
        }
        // applyfriend�� are we friend = 1 �� ����
        // friendlist�� �׸� �߰�
        // messagelist�� default �׸� �߰�
        if (friend_id != null)
        {
            WWWForm form = new WWWForm();
            //ģ���� ������ ��û�Ѱ��� �޴°��̹Ƿ� friend�� to user
            form.AddField("to_user_idPost", friend_id);
            form.AddField("from_user_idPost", user_id);
            UnityWebRequest www = UnityWebRequest.Post(updateApplyfriend, form);

            yield return www.SendWebRequest();
            string text = www.downloadHandler.text;
            if (text != "fail")
            {
                Debug.Log("ģ�� ���� ����");
            }
            else
            {
                Debug.Log("ģ�� ���� ����");
            }
        }
    }
}
