using Photon.Pun.Demo.PunBasics;
using UnityEngine;
using UnityEngine.UI;
public class PlayerUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Slider healthSlider;

    [Header("RSE")]
    [SerializeField] private RSE_SetPlayerTarget setPlayerTarget;

    private PlayerController playerController;

    private void OnEnable()
    {
        setPlayerTarget.action += SetTarget;
    }

    private void OnDisable()
    {
        setPlayerTarget.action -= SetTarget;
    }

    public void SetTarget(PlayerController target)
    {
        playerController = target;
        healthSlider.maxValue = playerController.CurrentHealth;
    }

    private void Update()
    {
        if (playerController == null) return;
        healthSlider.value = playerController.CurrentHealth;
    }
}