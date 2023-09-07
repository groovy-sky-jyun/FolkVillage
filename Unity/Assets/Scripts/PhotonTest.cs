using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonTest : MonoBehaviourPunCallbacks
{
    public UnityEngine.UI.Text participant;
    public UnityEngine.UI.Text log;
    string nickname;
    // Start is called before the first frame update
    void Start()
    {

        Screen.SetResolution(1080, 1920, false);//pc 실행 시 해상도 설정
        PhotonNetwork.ConnectUsingSettings();//포톤 연결 설정
        nickname=  PlayerPrefs.GetString("name");

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
