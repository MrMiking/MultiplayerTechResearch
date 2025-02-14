using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
public class PlayerListItem : MonoBehaviourPunCallbacks
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI playerNameText;
    private Player player;

    public void SetUp(Player player)
    {
        this.player = player;
        playerNameText.text = player.NickName;
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if(player == otherPlayer)
        {
            Destroy(gameObject);
        }
    }

    public override void OnLeftRoom()
    {
        Destroy(gameObject);
    }
}