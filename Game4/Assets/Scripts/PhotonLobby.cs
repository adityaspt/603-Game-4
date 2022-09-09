using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonLobby : MonoBehaviourPunCallbacks
{
    public static PhotonLobby lobby;

    public GameObject loadingButton;
    public GameObject startButton;
    public GameObject cancelButton;

    private void Awake()
    {
        lobby = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Player has connected to the photon server");
        PhotonNetwork.AutomaticallySyncScene = true;
        startButton.SetActive(true);
        loadingButton.SetActive(false);
    }

    public void OnStartButtonClicked()
    {
        startButton.SetActive(false);
        cancelButton.SetActive(true);
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Player has failed to connect. There is not available room.");
        CreateRoom();
    }

    void CreateRoom()
    {
        Debug.Log("Creating a room.");
        RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 4 };
        PhotonNetwork.CreateRoom("Test server", roomOps);
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("We created a room!");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Cannot create a new room!");
    }

    public void OnCancelButtonClicked()
    {
        cancelButton.SetActive(false);
        startButton.SetActive(true);
        PhotonNetwork.LeaveRoom();
    }
}
