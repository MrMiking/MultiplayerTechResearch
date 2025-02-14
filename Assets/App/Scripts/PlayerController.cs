using Photon.Pun;
using System.Collections;
using System.IO;
using TMPro;
using UnityEngine;
public class PlayerController : MonoBehaviour, IDamageable
{
    [Header("Settings")]
    [SerializeField] private float moveSpeed = 10.0f;

    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private PhotonView photonView;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private GameObject bulletPrefab;

    [Header("RSE")]
    [SerializeField] private RSE_Move onMove;
    [SerializeField] private RSE_Attack onAttack;

    public TMP_Text hpText;

    public float health = 100.0f;

    private float attackCooldown = 0;

    private void OnEnable()
    {
        onMove.action += Move;
        onAttack.action += Attack;
    }

    private void OnDisable()
    {
        onMove.action -= Move;
        onAttack.action -= Attack;
    }

    private void Update()
    {
        attackCooldown -= Time.deltaTime;
    }

    private void Move(Vector2 direction)
    {
        if (!photonView.IsMine) return;

        rb.MovePosition(transform.position + new Vector3(direction.x, direction.y, 0) * moveSpeed * Time.deltaTime);
    }

    private void Attack()
    {
        if(!photonView.IsMine) return;

        if(attackCooldown < 0)
        {
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", bulletPrefab.name), bulletSpawnPoint.position, bulletSpawnPoint.rotation, 0);
            attackCooldown = 2;
        }
    }

    public void TakeDamage(float damage)
    {
        photonView.RPC("RPC_TakeDamage", RpcTarget.All, damage);
    }

    [PunRPC]
    private void RPC_TakeDamage(float damage)
    {
        if(!photonView.IsMine) return;

        Debug.Log("Took Damage: " + damage);
    }
}