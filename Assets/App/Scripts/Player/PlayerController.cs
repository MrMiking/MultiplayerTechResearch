using Photon.Pun;
using System.IO;
using UnityEngine;
public class PlayerController : MonoBehaviour, IDamageable
{
    [Header("Settings")]
    [SerializeField] private float currentHealth = 100.0f;
    [SerializeField] private float moveSpeed = 10.0f;

    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private PhotonView photonView;
    [Space(5)]
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private Transform gun;
    [SerializeField] private GameObject bulletPrefab;
    [Space(5)]
    [SerializeField] private GameObject playerUIPrefab;

    [Header("RSE")]
    [SerializeField] private RSE_Move onMove;
    [SerializeField] private RSE_Attack onAttack;
    [SerializeField] private RSE_MoveCursor onMoveCursor;
    [SerializeField] private RSE_SetPlayerTarget setPlayerTarget;

    public float CurrentHealth { get { return currentHealth; } }

    private float attackCooldown = 0;

    private void OnEnable()
    {
        onMove.action += Move;
        onAttack.action += Attack;
        onMoveCursor.action += RotateGun;
    }

    private void OnDisable()
    {
        onMove.action -= Move;
        onAttack.action -= Attack;
        onMoveCursor.action -= RotateGun;
    }

    private void Start()
    {
        if (!photonView.IsMine) return;

        GameObject playerUI = Instantiate(playerUIPrefab);
        setPlayerTarget.Call(this);
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

    private void RotateGun(Vector2 mousePosition)
    {
        if (!photonView.IsMine) return;

        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(
            mousePosition.x, 
            mousePosition.y, 
            Camera.main.nearClipPlane));

        Vector3 rotateDirection = (worldPosition - gun.position).normalized;
        rotateDirection.z = 0;

        float angle = Mathf.Atan2(rotateDirection.y, rotateDirection.x) * Mathf.Rad2Deg;
        gun.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    public void TakeDamage(float damage)
    {
        photonView.RPC(nameof(RPC_TakeDamage), RpcTarget.All, damage);
    }

    [PunRPC]
    private void RPC_TakeDamage(float damage)
    {
        Debug.Log("TakeDamage: " + damage);
        currentHealth -= damage;
        if (currentHealth <= 0) PhotonNetwork.Destroy(gameObject);
    }
}