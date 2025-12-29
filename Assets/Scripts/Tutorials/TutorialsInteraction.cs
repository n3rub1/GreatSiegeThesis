using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialsInteraction : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private GameObject boxOnTopOfTutorialBoards;
    [SerializeField] private List<GameObject> multipleTutorialPanels;
    [SerializeField] private GameObject mainPanelForMultipleTutorials;
    [SerializeField] private DayNightCycleManager dayNightCycleManager;
    [SerializeField] private GameObject nextButton;
    [SerializeField] private bool hasMultipleScreens;

    private bool interactable = false;
    private PlayerActions actions;
    private int index = 0;
    private int maxIndex;

    private void Awake()
    {
        actions = new PlayerActions();
    }

    void Start()
    {
        maxIndex = multipleTutorialPanels.Count;
        actions.Tutorials.OpenTutorial.performed += ctx => OpenTutorialPanel();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            index = 0;
            interactable = true;
            boxOnTopOfTutorialBoards.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        //if (other.CompareTag("Player"))
        //{
        //    interactable = false;
        //    boxOnTopOfTutorialBoards.SetActive(false);
        //    // dayNightCycleManager.StartStopTimer(false);

        //    if (!hasMultipleScreens) {
        //        multipleTutorialPanels[0].SetActive(false);
        //    }
        //    else
        //    {
        //        mainPanelForMultipleTutorials.SetActive(false);
        //        index = 0;
        //        foreach (GameObject multiplePanel in multipleTutorialPanels)
        //        {
        //            multiplePanel.SetActive(false);
        //        }
        //    }
        //}

        if (other.CompareTag("Player"))
        {
            interactable = false;
            boxOnTopOfTutorialBoards.SetActive(false);
        }
    }

    public void OpenTutorialPanel()
    {
         dayNightCycleManager.StartStopTimer(true);

        if (!hasMultipleScreens && interactable)
        {
            multipleTutorialPanels[0].SetActive(true);
        }
        else if(hasMultipleScreens && interactable)
        {
            mainPanelForMultipleTutorials.SetActive(true);
            multipleTutorialPanels[0].SetActive(true);
            nextButton.SetActive(true);
        }
    }

    public void NextTutorialPanel()
    {
        mainPanelForMultipleTutorials.SetActive(true);

        if (hasMultipleScreens && interactable)
        {
            index++;
            multipleTutorialPanels[index - 1].SetActive(false);
            multipleTutorialPanels[index].SetActive(true);

            if (index == 3)
            {
                index = 0;
                nextButton.SetActive(false);
            }
        }

    }

    public void CloseTutorialPanel()
    {
         dayNightCycleManager.StartStopTimer(false);
        index = 0;

        if (!hasMultipleScreens)
        {
            multipleTutorialPanels[0].SetActive(false);
        }
        else
        {
            mainPanelForMultipleTutorials.SetActive(false);
            foreach (GameObject multiplePanel in multipleTutorialPanels)
            {
                multiplePanel.SetActive(false);
            }
        }
    }

    private void OnEnable()
    {
        actions.Enable();
    }

    private void OnDisable()
    {
        actions.Disable();
    }
}
