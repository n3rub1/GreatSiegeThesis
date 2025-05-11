using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteraction : MonoBehaviour
{

    [Header("Config")]
    [SerializeField] private NPCDialog dialogToShow;
    [SerializeField] private GameObject boxOnTopOfNPC;

    public NPCDialog DialogToShow => dialogToShow;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DialogManager.Instance.npcSelected = this;
            boxOnTopOfNPC.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DialogManager.Instance.npcSelected = null;
            DialogManager.Instance.CloseDialogPanel();

            boxOnTopOfNPC.SetActive(false);
        }
    }

}
