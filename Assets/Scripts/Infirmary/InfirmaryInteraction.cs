using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfirmaryInteraction : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private GameObject boxOnTopOfFirePot;
    [SerializeField] private InfirmaryUI infirmaryUI;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            InfirmaryUI.Instance.infirmaryInteraction = this;
            boxOnTopOfFirePot.SetActive(true);
            infirmaryUI.SetPlayerInRange(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            InfirmaryUI.Instance.infirmaryInteraction = null;
            boxOnTopOfFirePot.SetActive(false);
            infirmaryUI.SetPlayerInRange(false);
            InfirmaryUI.Instance.CloseInfirmaryPanel();
        }
    }
}
