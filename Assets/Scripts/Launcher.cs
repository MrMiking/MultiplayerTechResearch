using Photon.Pun;
using UnityEngine;
using UnityEngine.Events;
public class Launcher : MonoBehaviourPunCallbacks
{
    [SerializeField] private RSE_CreateRoom createRoom;

    public UnityEvent onCreateRoom;
    public UnityEvent onRoomJoined;

    public override void OnEnable()
    {
        createRoom.action += CreateRoom;
    }

    public override void OnDisable()
    {
        createRoom.action -= CreateRoom;
    }

    private void Start()
    {
        Debug.Log("Connecting to Master");
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby");
    }

    public void CreateRoom(string roomName)
    {
        PhotonNetwork.CreateRoom(roomName);
        onCreateRoom.Invoke();
    }

    public override void OnJoinedRoom()
    {
        onRoomJoined.Invoke();
    }
}