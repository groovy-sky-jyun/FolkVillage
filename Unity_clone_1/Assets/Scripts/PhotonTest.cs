using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public Text participant;
    public Text log;
    string nickname;
    [Tooltip("The prefab to use for representing the player")]
    

    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(1080, 1920, false);//pc 실행 시 해상도 설정
        PhotonNetwork.ConnectUsingSettings();//포톤 연결 설정
        nickname=  PlayerPrefs.GetString("name");
        GameObject.Find("Canvas/NomalChatting").SetActive(false);
    }

    public override void OnConnectedToMaster()
    {
        RoomOptions options = new RoomOptions(); //방옵션설정
        options.MaxPlayers = 10;//최대인원 설정
        //방이 있으면 입장, 없으면 방 새로 만들어서 입장
        PhotonNetwork.LocalPlayer.NickName = nickname;
        PhotonNetwork.JoinOrCreateRoom("World1", options, null);
    }
    public override void OnJoinedRoom()
    {
        updatePlayer();
        log.text +=  nickname;
        log.text += " 님이 방에 참가하였습니다\n";
        //Resources/Player프리팹 복제
        PhotonNetwork.Instantiate("Player", Vector2.zero, Quaternion.identity);
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        updatePlayer();
        log.text += newPlayer.NickName;
        log.text += " 님이 입장하였습니다\n";
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        updatePlayer();
        log.text += otherPlayer.NickName;
        log.text += " 님이 퇴장하였습니다\n";
    }
    public void Connect()
    {
        PhotonNetwork.ConnectUsingSettings();
    }
    void updatePlayer()
    {
        participant.text = "";
        for(int i=0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            participant.text += PhotonNetwork.PlayerList[i].NickName;
            participant.text += "\n";
        }

    }
}
