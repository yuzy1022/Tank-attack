using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PhotonInit : MonoBehaviourPunCallbacks
{
    public string version = "v1.0";
    public Text logText; //���� ���� ���� ǥ���� �ؽ�Ʈ 
    private void Awake()
    {
        PhotonNetwork.GameVersion = version; // ������ ����
        PhotonNetwork.ConnectUsingSettings(); //���� Ŭ���忡 ������ �õ� 
    }
    //���� Ŭ���忡 ���������� ������ �� �Ǿ��ٸ� ȣ��Ǵ� �ݹ��Լ�
    public override void OnConnectedToMaster()
    {
        Debug.Log("Entered Lobby !");
        PhotonNetwork.JoinRandomRoom();
    }
    //���� Ŭ���忡 ���ӿ� �������� �� ȣ��Ǵ� �ݹ� �Լ�
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Connect Error");
        PhotonNetwork.ConnectUsingSettings(); //���� Ŭ���忡 ������ ��õ� 
    }
    //������ �� ���ӿ� ������ ��� ȣ��Ǵ� �ݹ� �Լ� 
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("No rooms!");
        PhotonNetwork.CreateRoom("My room", new RoomOptions { MaxPlayers = 20 });
    }
    public override void OnJoinedRoom() // �뿡 �����ϸ� ȣ��Ǵ� �ݹ� �Լ�
    {
        Debug.Log("Enter Room");
    }
    public void Update()
    {
        logText.text = PhotonNetwork.NetworkClientState.ToString();
    }
}
