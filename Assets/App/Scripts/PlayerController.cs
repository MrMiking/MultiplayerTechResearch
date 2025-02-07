using Photon.Pun;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float moveSpeed = 10.0f;

    [Header("References")]
    [SerializeField] private PhotonView photonView;

    [Header("RSE")]
    [SerializeField] private RSE_Move onMove;
    [SerializeField] private RSE_Attack onAttack;

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
}