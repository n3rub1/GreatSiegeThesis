using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmouryInteraction : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private GameObject boxOnTopOfLoot;
    [SerializeField] private ArmouryUI armouryUI;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ArmouryUI.Instance.armouryInteraction = this;
            boxOnTopOfLoot.SetActive(true);
            armouryUI.SetPlayerInRange(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ArmouryUI.Instance.armouryInteraction = null;
            boxOnTopOfLoot.SetActive(false);
            armouryUI.SetPlayerInRange(false);
            ArmouryUI.Instance.CloseArmouryPanel();
        }
    }
}
