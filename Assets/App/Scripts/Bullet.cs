using Photon.Pun;
using UnityEngine;
public class Bullet : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float movespeed;
    [SerializeField] private float damage;

    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private PhotonView photonView;

    private PhotonMessageInfo info;

    private void Start()
    {
        rb.linearVelocity = transform.up * movespeed;
        Invoke(nameof(DestroyBullet), 3);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!photonView.IsMine) return;

        if(collision.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(damage);
        }

        DestroyBullet();
    }

    private void DestroyBullet()
    {
        if (!photonView.IsMine) return;
        PhotonNetwork.Destroy(gameObject);
    }
}