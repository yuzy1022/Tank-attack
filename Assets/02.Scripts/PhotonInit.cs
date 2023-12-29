using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PhotonInit : MonoBehaviourPunCallbacks
{
    public string version = "v1.0";
    public Text logText; //접속 여부 등을 표시할 텍스트 
    private void Awake()
    {
        PhotonNetwork.GameVersion = version; // 게임의 버전
        PhotonNetwork.ConnectUsingSettings(); //포톤 클라우드에 접속을 시도 
    }
    //포톤 클라우드에 정상적으로 접속이 잘 되었다면 호출되는 콜백함수
    public override void OnConnectedToMaster()
    {
        Debug.Log("Entered Lobby !");
        PhotonNetwork.JoinRandomRoom();
    }
    //포톤 클라우드에 접속에 실패했을 때 호출되는 콜백 함수
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Connect Error");
        PhotonNetwork.ConnectUsingSettings(); //포톤 클라우드에 접속을 재시도 
    }
    //무작위 룸 접속에 실패한 경우 호출되는 콜백 함수 
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("No rooms!");
        PhotonNetwork.CreateRoom("My room", new RoomOptions { MaxPlayers = 20 });
    }
    public override void OnJoinedRoom() // 룸에 입학하면 호출되는 콜백 함수
    {
        Debug.Log("Enter Room");
    }
    public void Update()
    {
        logText.text = PhotonNetwork.NetworkClientState.ToString();
    }
}
