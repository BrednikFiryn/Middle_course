using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        RoomOptions options = new RoomOptions
        {
            MaxPlayers = 4,
            IsVisible = false
        };
        PhotonNetwork.JoinOrCreateRoom("test", options, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log(PhotonNetwork.CurrentRoom.PlayerCount);
    }
}
