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
        if (isPlayerInRangeOfBed && !isSleeping)
        {
            gameManager.sleepAndUpdateDay();
            string lastQuest = gameManager.GetLastQuestOfTheDay();
            QuestUI.Instance.ResetQuests();
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
