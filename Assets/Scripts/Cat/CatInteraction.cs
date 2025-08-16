using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CatInteraction : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private GameObject boxOnTopOfCatPrints;
    [SerializeField] private GameObject catCluePanel;
    [SerializeField] private TextMeshProUGUI clueNumberTMP;
    [SerializeField] private int clueNumber;
    [SerializeField] private List<string> allCatCluesDescriptionTMP;
    [SerializeField] private TextMeshProUGUI catClueDescription;
    [SerializeField] private CatClueManager catClueManager;

    private CatUI catUI;
    private bool interactionHappening = false;

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

            if (interactionHappening)
            {
                Destroy(gameObject);
            }

        }
    }

    public void OpenCatPanel()
    {
        if (!interactionHappening)
        {
            clueNumber = catClueManager.getCatClueNumber();
            catCluePanel.SetActive(true);
            clueNumberTMP.text = $"Clue #{clueNumber + 1}";
            catClueDescription.text = allCatCluesDescriptionTMP[clueNumber];
            catClueManager.setCatClueNumber();
            interactionHappening = true;
        }

    }
}
