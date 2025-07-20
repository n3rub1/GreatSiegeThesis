using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CatInteraction : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private GameObject boxOnTopOfCatPrints;
    [SerializeField] private GameObject catCluePanel;
    [SerializeField] private TextMeshProUGUI clueNumber;
    [SerializeField] private TextMeshProUGUI clueDescription;

    private CatUI catUI;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CatUI.Instance.catInteraction = this;
            boxOnTopOfCatPrints.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CatUI.Instance.catInteraction = null;
            boxOnTopOfCatPrints.SetActive(false);
            catCluePanel.SetActive(false);
        }
    }

    public void OpenCatPanel()
    {
        catCluePanel.SetActive(true);
        clueNumber.text = "Clue #1";
        clueDescription.text = "This is a description of the clue you have just found";
    }
}
