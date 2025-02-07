using Photon.Pun;
using UnityEngine;
public class PlayerInput : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PhotonView photonView;

    [Header("RSE")]
    [SerializeField] private RSE_Move onMove;
    [SerializeField] private RSE_Attack onAttack;

    private void FixedUpdate()
    {
        if (photonView.IsMine)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            onMove.Call(new Vector2(horizontal, vertical));

            if (Input.GetKey(KeyCode.Mouse0)) onAttack.Call();
        }
    }
}