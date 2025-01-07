using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NickNameSetting : MonoBehaviour
{
    public Text friendNickname;
    public void FriendNickSetting()
    {
        friendNickname.text=PlayerPrefs.GetString("friend_nickname");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
