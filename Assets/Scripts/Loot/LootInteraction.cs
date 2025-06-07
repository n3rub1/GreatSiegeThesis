using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootInteraction : MonoBehaviour
{

    [Header("Config")]
    [SerializeField] private GameObject boxOnTopOfNPC;

    [SerializeField] private LootManager lootManager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            LootManager.Instance.lootSelected = this;
            boxOnTopOfNPC.SetActive(true);
            lootManager.SetPlayerInRange(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            LootManager.Instance.lootSelected = null;
            boxOnTopOfNPC.SetActive(false);
            lootManager.SetPlayerInRange(false);
        }
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}
