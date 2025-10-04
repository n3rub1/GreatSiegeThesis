using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StructureInteraction : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private GameObject boxOnTopOfRepairableItem;
    [SerializeField] private GameObject boxOnTopOfRepairableItemTimer;
    [SerializeField] private TextMeshProUGUI repairableItemTimerTMP;
    [SerializeField] private int timer = 5;
    [SerializeField] private StructureUI structureUI;

    private Coroutine repairCoroutine;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StructureUI.Instance.structureInteraction = this;
            boxOnTopOfRepairableItem.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StructureUI.Instance.structureInteraction = null;
            boxOnTopOfRepairableItem.SetActive(false);
        }
    }
    public void StartRepair()
    {
        if(repairCoroutine == null)
        {
            Debug.Log("start");
            repairCoroutine = StartCoroutine(RepairHoldCoroutine());
        }

    }

    public void CancelRepair()
    {
        if(repairCoroutine != null)
        {
            Debug.Log("stop");
            StopCoroutine(repairCoroutine);
            repairCoroutine = null;
            boxOnTopOfRepairableItemTimer.SetActive(false);
        }


    }

    private IEnumerator RepairHoldCoroutine()
    {
        float elapsedTime = 0f;

        while (elapsedTime < timer)
        {
            elapsedTime += Time.deltaTime;
            boxOnTopOfRepairableItemTimer.SetActive(true);
            repairableItemTimerTMP.text = Mathf.FloorToInt(elapsedTime).ToString() + $"/{timer}";
            yield return null;
        }
        StructureUI.Instance.structureInteraction = null;

        gameObject.SetActive(false);
        structureUI.StructureRepairedAndIncreasePercentage();

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }

    }

private void OnDestroy()
{

        if (StructureUI.Instance.structureInteraction == this)
   {
        StructureUI.Instance.structureInteraction = null;
   }
    }

}
