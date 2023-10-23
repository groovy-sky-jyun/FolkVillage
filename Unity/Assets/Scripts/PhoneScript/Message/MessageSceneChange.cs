using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MessageSceneChange : MonoBehaviour
{
   
    string friendNickName;
    GameObject main = null;
    GameObject friend_list = null;
    GameObject friend_search = null;
    GameObject friend_add = null;
    GameObject call_list = null;
    GameObject message_list = null;
    GameObject message_record = null;
    public void MessageSendBtnOnClick()
    {
        //클릭한 객체 가져오기
        GameObject messageIconObject = EventSystem.current.currentSelectedGameObject;
        //부모 객체 가져오기
        GameObject parentFriendListBtn = messageIconObject.transform.parent.gameObject;
        //해당 친구 프로필의 닉네임 가져오기->닉네임 설정
        friendNickName = parentFriendListBtn.GetComponentInChildren<Text>().text;
        //친구 닉네임 게임 내 저장
        PlayerPrefs.SetString("messageFriend", friendNickName);
        Debug.Log(friendNickName);
        //화면전환
        GameObject parentObj = GameObject.Find("ChangeScreenCS");

        main = parentObj.transform.Find("MainPanel").gameObject;
        main.SetActive(false);

        friend_list = parentObj.transform.Find("FriendListPanel").gameObject;
        friend_list.SetActive(false);

        friend_search = parentObj.transform.Find("FriendSearchPanel").gameObject;
        friend_search.SetActive(false);

        friend_add = parentObj.transform.Find("FriendAddPanel").gameObject;
        friend_add.SetActive(false);

        call_list = parentObj.transform.Find("CallListPanel").gameObject;
        call_list.SetActive(false);

        message_list = parentObj.transform.Find("MessageListPanel").gameObject;
        message_list.SetActive(false);

        message_record = parentObj.transform.Find("MessageRecordPanel").gameObject;
        message_record.SetActive(true);
        message_record.GetComponent<NickNameSetting>().FriendNickSetting();

    }

    

}
