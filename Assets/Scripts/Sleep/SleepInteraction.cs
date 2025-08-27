using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SleepInteraction : MonoBehaviour
{

    [Header("Config")]
    [SerializeField] private GameObject boxOnTopOfBed;
    [SerializeField] private SleepManager sleepManager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SleepManager.Instance.sleepInteraction = this;
            boxOnTopOfBed.SetActive(true);
            sleepManager.SetPlayerInRange(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            LootManager.Instance.lootSelected = null;
            boxOnTopOfBed.SetActive(false);
            sleepManager.SetPlayerInRange(false);
        }
    }
}

