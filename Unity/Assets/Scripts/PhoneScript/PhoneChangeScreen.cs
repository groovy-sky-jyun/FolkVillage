using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PhoneChangeScreen : MonoBehaviour
{
    GameObject main = null;
    GameObject friend_list = null;
    GameObject friend_search = null;
    GameObject friend_add = null;
    // GameObject call = null;
    // GameObject message = null;

    void Start()
    {
        BackBtnOnClick();
    }
    public void BackBtnOnClick()
    {
        Debug.Log("backbtnoncalick 함수 실행");
        main = transform.Find("MainPanel").gameObject;
        main.SetActive(true);

        friend_list = transform.Find("FriendListPanel").gameObject;
        friend_list.SetActive(false);

        friend_search = transform.Find("FriendSearchPanel").gameObject;
        friend_search.SetActive(false);

        friend_add = transform.Find("FriendAddPanel").gameObject;
        friend_add.SetActive(false);
    }

   public void FriendListOnClick()
    {
        main = transform.Find("MainPanel").gameObject;
        main.SetActive(false);

        friend_list = transform.Find("FriendListPanel").gameObject;
        friend_list.SetActive(true);

        friend_search = transform.Find("FriendSearchPanel").gameObject;
        friend_search.SetActive(false);

        friend_add = transform.Find("FriendAddPanel").gameObject;
        friend_add.SetActive(false);
    }

    public void FriendSearchOnClick()
    {
        main = transform.Find("MainPanel").gameObject;
        main.SetActive(false);

        friend_list = transform.Find("FriendListPanel").gameObject;
        friend_list.SetActive(false);

        friend_search = transform.Find("FriendSearchPanel").gameObject;
        friend_search.SetActive(true);

        friend_add = transform.Find("FriendAddPanel").gameObject;
        friend_add.SetActive(false);
    }

    public void FriendAddOnClick()
    {
        main = transform.Find("MainPanel").gameObject;
        main.SetActive(false);

        friend_list = transform.Find("FriendListPanel").gameObject;
        friend_list.SetActive(false);

        friend_search = transform.Find("FriendSearchPanel").gameObject;
        friend_search.SetActive(false);

        friend_add = transform.Find("FriendAddPanel").gameObject;
        friend_add.SetActive(true);
    }
    void Update()
    {
        
    }
}
