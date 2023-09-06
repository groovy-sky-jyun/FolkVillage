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

        Screen.SetResolution(1080, 1920, false);//pc ���� �� �ػ� ����
        PhotonNetwork.ConnectUsingSettings();//���� ���� ����
        nickname=  PlayerPrefs.GetString("name");

    }

    public override void OnConnectedToMaster()
    {
        RoomOptions options = new RoomOptions(); //��ɼǼ���
        options.MaxPlayers = 10;//�ִ��ο� ����
        //���� ������ ����, ������ �� ���� ���� ����
        PhotonNetwork.LocalPlayer.NickName = nickname;
        PhotonNetwork.JoinOrCreateRoom("World1", options, null);
    }
    public override void OnJoinedRoom()
    {
        updatePlayer();
        log.text +=  nickname;
        log.text += " ���� �濡 �����Ͽ����ϴ�\n";
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        updatePlayer();
        log.text += newPlayer.NickName;
        log.text += " ���� �����Ͽ����ϴ�\n";
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        updatePlayer();
        log.text += otherPlayer.NickName;
        log.text += " ���� �����Ͽ����ϴ�\n";
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
