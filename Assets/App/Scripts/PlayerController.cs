using Photon.Pun;
using TMPro;
using UnityEngine;
public class PlayerController : MonoBehaviour, IDamageable
{
    [Header("Settings")]
    [SerializeField] private float moveSpeed = 10.0f;

    [Header("References")]
    [SerializeField] private PhotonView photonView;

    [Header("RSE")]
    [SerializeField] private RSE_Move onMove;
    [SerializeField] private RSE_Attack onAttack;

    public TMP_Text hpText;

    public float health = 100.0f;

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

    private void Move(Vector2 direction)
    {
        if (!photonView.IsMine) return;
        transform.position += new Vector3(direction.x, direction.y, 0) * moveSpeed * Time.deltaTime;
    }

    private void Attack()
    {
        Debug.Log("Attack");
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        hpText.text = health.ToString();
    }
}