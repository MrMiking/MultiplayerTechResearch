using TMPro;
using UnityEngine;
public class CreateRoomManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TMP_InputField inputField;

    [Header("RSE")]
    [SerializeField] private RSE_CreateRoom onCreateRoom;

    public void CreateNewRoom()
    {
        if (string.IsNullOrEmpty(inputField.text)) onCreateRoom.Call(inputField.text);
    }
}