using Photon.Pun;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform[] spawnPoints;

    public void Start()
    {
        Vector3 spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", playerPrefab.name), spawnPoint, Quaternion.identity);
    }
}