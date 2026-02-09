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
    [SerializeField] private DangerSpawn dangerSpawn;
    [SerializeField] private bool hasMultipleScreens;
    [SerializeField] private PlayerMovement player;
    [SerializeField] private AudioSource buttonAudioSource;

    private bool interactable = false;
    private PlayerActions actions;
    private int index = 0;
    private int maxIndex;
    private bool isCurrentlyInteracting = false;

    private void Awake()
    {
        actions = new PlayerActions();
    }

    void Start()
    {
        interactable = false;
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
            isCurrentlyInteracting = false;
        }
    }

    public void OpenTutorialPanel()
    {

        if (!hasMultipleScreens && interactable && !isCurrentlyInteracting)
        {
            buttonAudioSource.Play();
            dayNightCycleManager.StartStopTimer(true);
            multipleTutorialPanels[0].SetActive(true);
            isCurrentlyInteracting = true;
        }
        else if(hasMultipleScreens && interactable && !isCurrentlyInteracting)
        {
            buttonAudioSource.Play();
            dayNightCycleManager.StartStopTimer(true);
            mainPanelForMultipleTutorials.SetActive(true);
            multipleTutorialPanels[0].SetActive(true);
            nextButton.SetActive(true);
            isCurrentlyInteracting = true;
        }

        if (interactable)
        {
            dangerSpawn.StopAllSpawns();
            player.DisableMovement();
        }



    }

    public void NextTutorialPanel()
    {
        buttonAudioSource.Play();

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
        buttonAudioSource.Play();

        dayNightCycleManager.StartStopTimer(false);
        index = 0;

        isCurrentlyInteracting = false;

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

        dangerSpawn.RestartAllSpawns();
        player.EnableMovement();
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
