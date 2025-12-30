using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepManager : Singleton<SleepManager>
{

    private bool isPlayerInRangeOfBed = false;
    public SleepInteraction sleepInteraction { get; set; }
    public bool isSleeping = false;


    [Header("Config")]
    [SerializeField] private GameManager gameManager;

    private PlayerActions actions;

    protected override void Awake()
    {
        base.Awake();
        actions = new PlayerActions();
    }

    private void Start()
    {
        actions.Sleep.SleepToEndDay.performed += ctx => SleepToEndDay();
    }

    private void SleepToEndDay()
    {


        if (isPlayerInRangeOfBed && !isSleeping && gameManager.GetDayNumber() < 5)
        {
            GoogleSheetLogger.I.BeginEndOfDayTransition();
            GoogleSheetLogger.I.FlushDay();
            gameManager.SleepAndUpdateDay();
            string lastQuest = gameManager.GetLastQuestOfTheDay();
            QuestUI.Instance.ResetQuests();
            gameManager.DestructionPerDay();
            gameManager.ResetAllSpawns();
            GoogleSheetLogger.I.BeginDay(gameManager.GetDayNumber());

        }
        else if(isPlayerInRangeOfBed && !isSleeping && gameManager.GetDayNumber() == 5)
        {
            GoogleSheetLogger.I.BeginEndOfDayTransition();
            GoogleSheetLogger.I.FlushDay();
            gameManager.GetCaptured();
            gameManager.SleepAndUpdateDay();
            string lastQuest = gameManager.GetLastQuestOfTheDay();
            QuestUI.Instance.ResetQuests();
            gameManager.DestructionPerDay();
            gameManager.ResetAllSpawns();
            GoogleSheetLogger.I.BeginDay(gameManager.GetDayNumber());
        }
        else if(isPlayerInRangeOfBed && !isSleeping && gameManager.GetDayNumber() > 5 && gameManager.GetDayNumber() != 10)
        {
            GoogleSheetLogger.I.BeginEndOfDayTransition();
            GoogleSheetLogger.I.FlushDay();
            gameManager.SleepAndUpdateDay();
            string lastQuest = gameManager.GetLastQuestOfTheDay();
            QuestUI.Instance.ResetQuests();
            gameManager.DestructionPerDay();
            gameManager.ResetAllSpawns();
            GoogleSheetLogger.I.BeginDay(gameManager.GetDayNumber());

        }
        else if (isPlayerInRangeOfBed && !isSleeping && gameManager.GetDayNumber() == 10)
        {
            GoogleSheetLogger.I.BeginEndOfDayTransition();
            GoogleSheetLogger.I.FlushDay();
            gameManager.MoveToVillage();
            gameManager.SleepAndUpdateDay();
            string lastQuest = gameManager.GetLastQuestOfTheDay();
            QuestUI.Instance.ResetQuests();
            gameManager.DestructionPerDay();
            gameManager.ResetAllSpawns();
            GoogleSheetLogger.I.BeginDay(gameManager.GetDayNumber());

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

    public void SetPlayerInRange(bool value)
    {
        isPlayerInRangeOfBed = value;
    }

}
