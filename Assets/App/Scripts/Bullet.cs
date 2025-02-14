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

    private void Start()
    {
        rb.linearVelocity = transform.up * movespeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger with: " + collision.name);
        if(collision.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(damage);
        }

        PhotonNetwork.Destroy(photonView);
    }
}