using Photon.Pun;
using Photon.Realtime;
using Unity.VisualScripting;
using UnityEngine;
public class UI_RoomManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject playerInfoPanel;
    [SerializeField] private GameObject playerInfoParentList;
    [Space(5)]
    [SerializeField] private GameObject startButton;

    [Header("RSE")]
    [SerializeField] private RSE_PlayerEnteredRoom onPlayerEnteredRoom;
    [SerializeField] private RSE_LeaveRoom onLeaveRoom;

    private void OnEnable()
    {
        onPlayerEnteredRoom.action += SpawnPlayerUIInfo;
        onPlayerEnteredRoom.action += HideStartButton;

        onLeaveRoom.action += ClearPlayerList;
    }

    private void OnDisable()
    {
        onPlayerEnteredRoom.action -= SpawnPlayerUIInfo;
        onPlayerEnteredRoom.action -= HideStartButton;

        onLeaveRoom.action -= ClearPlayerList;
    }

    private void SpawnPlayerUIInfo(Player playerInfo)
    {
        Instantiate(playerInfoPanel ,playerInfoParentList.transform).GetComponent<PlayerListItem>().SetUp(playerInfo);
    }

    private void HideStartButton(Player playerInfo) => startButton.SetActive(PhotonNetwork.IsMasterClient);
    private void ClearPlayerList()
    {
        Debug.Log("Clear");
        foreach(Transform child in playerInfoParentList.transform) 
            Destroy(child.gameObject); 
    }
}