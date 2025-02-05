using Photon.Pun;
using UnityEngine;
public class Launcher : MonoBehaviourPunCallbacks
{
    [SerializeField] private RSE_CreateRoom onCreateRoom;

    public override void OnEnable()
    {
        onCreateRoom.action += CreateRoom;
    }

    public override void OnDisable()
    {
        onCreateRoom.action -= CreateRoom;
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
    }
}