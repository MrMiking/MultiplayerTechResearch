using Photon.Pun;
using Photon.Realtime;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Launcher : MonoBehaviourPunCallbacks 
{
    [Header("RSE")]
    [SerializeField] private RSE_ConnectToMaster onConnectToMaster;
    [SerializeField] private RSE_TryCreateRoom onTryCreateRoom;
    [SerializeField] private RSE_TryJoinRoom onTryJoinRoom;
    [SerializeField] private RSE_StartGame onStartGame;
    [SerializeField] private RSE_LeaveRoom onLeaveRoom;
    [SerializeField] private RSE_PlayerEnteredRoom onPlayerEnteredRoom;

    [Header("Events")]
    public UnityEvent onLoading;
    public UnityEvent onJoinedLobby;
    public UnityEvent onJoinedRoom;

    public override void OnEnable()
    {
        base.OnEnable();

        onConnectToMaster.action += ConnectToMaster;

        onTryCreateRoom.action += TryCreateRoom;
        onTryJoinRoom.action += TryJoinRoom;
        onLeaveRoom.action += LeaveRoom;

        onStartGame.action += StartGame;
    }

    public override void OnDisable()
    {
        base.OnDisable();

        onConnectToMaster.action -= ConnectToMaster;

        onTryCreateRoom.action -= TryCreateRoom;
        onTryJoinRoom.action -= TryJoinRoom;
        onLeaveRoom.action -= LeaveRoom;

        onStartGame.action -= StartGame;
    }

    #region LOBBY
    private void ConnectToMaster()
    {
        Debug.Log("Connecting to Master");
        onLoading.Invoke();
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master");
        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby");
        onJoinedLobby.Invoke();
        PhotonNetwork.NickName = "Player" + Random.Range(0, 1000).ToString("1000");
    }
    #endregion

    #region ROOM
    public void TryCreateRoom(string roomName)
    {
        Debug.Log("TryCreateRoom");
        onLoading.Invoke();
        PhotonNetwork.CreateRoom(roomName);
    }

    public void TryJoinRoom(string roomName)
    {
        Debug.Log("TryJoinRoom");
        onLoading.Invoke();
        PhotonNetwork.JoinRoom(roomName);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Room Created/Joined !");
        onJoinedRoom.Invoke();

        Player[] players = PhotonNetwork.PlayerList;

        for (int i = 0; i < players.Count(); i++)
        {
            onPlayerEnteredRoom.Call(players[i]);
        }
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Room Join Failed : " + message);
        onJoinedLobby.Invoke();
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Room Creation Failed : " + message);
        onJoinedLobby.Invoke();
    }

    public void LeaveRoom()
    {
        onLoading.Invoke();
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        onJoinedLobby.Invoke();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        onPlayerEnteredRoom.Call(newPlayer);
    }
    #endregion

    public void StartGame()
    {
        PhotonNetwork.LoadLevel(1);
    }
}