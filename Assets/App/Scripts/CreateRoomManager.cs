using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
public class CreateRoomManager : MonoBehaviour
{
    [BoxGroup("References")]
    [SerializeField] private TMP_InputField inputField;

    [BoxGroup("RSE")]
    [SerializeField] private RSE_TryCreateRoom onTryCreateRoom;
    [BoxGroup("RSE")]
    [SerializeField] private RSE_TryJoinRoom onTryJoinRoom;

    public void TryCreateRoom()
    {
        if (!string.IsNullOrEmpty(inputField.text)) onTryCreateRoom.Call(inputField.text);
    }

    public void TryJoinRoom()
    {
        if (!string.IsNullOrEmpty(inputField.text)) onTryJoinRoom.Call(inputField.text);
    }
}