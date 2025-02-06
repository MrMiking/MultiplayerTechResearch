using Photon.Pun;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject playerPrefab;

    public void Start()
    {
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", playerPrefab.name), Vector3.zero, Quaternion.identity);
    }
}