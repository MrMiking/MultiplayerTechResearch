using UnityEngine;
public class Bullet : MonoBehaviour
{
    [SerializeField] private float damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("qdqs");
        if(collision.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(damage);
        }
        
    }
}