using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LootInteraction : MonoBehaviour
{

    [Header("Config")]
    [SerializeField] private GameObject boxOnTopOfLoot;
    [SerializeField] private GameObject lootInformation;
    [SerializeField] private TextMeshProUGUI lootInformationText;
    [SerializeField] private LootManager lootManager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            LootManager.Instance.lootSelected = this;
            boxOnTopOfLoot.SetActive(true);
            lootManager.SetPlayerInRange(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            LootManager.Instance.lootSelected = null;
            boxOnTopOfLoot.SetActive(false);
            lootManager.SetPlayerInRange(false);
        }
    }

    public void ShowLootGained(InventoryItem inventoryItem, int amount)
    {
        lootInformation.SetActive(true);
        lootInformationText.text = "+" + amount + " " + inventoryItem.Name;
        LootManager.Instance.lootSelected = null;
        StartCoroutine(MoveUpAndDestroy());
    }

    private IEnumerator MoveUpAndDestroy()
    {
        float duration = 0.5f;
        float elapsedTime = 0f;

        Vector3 startPos = lootInformation.transform.position;
        Vector3 endPos = startPos + new Vector3(0, 1.0f, 0);

        while (elapsedTime < duration)
        {
            lootInformation.transform.position = Vector3.Lerp(startPos, endPos, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        lootInformation.transform.position = endPos;
        Destroy(gameObject);
    }
}
