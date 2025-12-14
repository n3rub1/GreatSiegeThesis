using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Quest Game Manager")]
    private string questAccepted = "";
    public enum QuestType { Armoury, Infirmary, Cat, Structure, Reset, SleepTime, Dead, Nothing }
    [SerializeField] GameObject armouryTeleport;
    [SerializeField] GameObject infirmaryTeleport;
    [SerializeField] GameObject caveTeleport;
    [SerializeField] GameObject bedTeleport;
    [SerializeField] GameObject bedTeleportInside;
    [SerializeField] RandomQuestSelector randomQuestSelector;
    [SerializeField] EndOfDayManager endOfDayManager;
    [SerializeField] GoogleSheetLogger logger;
    [SerializeField] UIManager uiManager;
    [SerializeField] private PercentageManager percentageManager;

    [Header("QuestsObjects")]
    [SerializeField] ArmouryUI armouryUI;
    [SerializeField] InfirmaryUI infirmaryUI;
    [SerializeField] StructureUI structureUI;
    [SerializeField] CatUI catUI;

    [Header("Day Game Manager")]
    public int dayNumber = 1;

    private string lastQuest;
    private string playerId;

    public NPCDialog[] npcDialog;

    private void Awake()
    {
        foreach (NPCDialog dialog in npcDialog)
        {
            dialog.ResetData();
        }
    }

    private void Start()
    {
        playerId = SystemInfo.deviceUniqueIdentifier;
        dayNumber = 1;
        armouryTeleport.SetActive(false);
        infirmaryTeleport.SetActive(false);
        caveTeleport.SetActive(false);
        bedTeleport.SetActive(false);
        GoogleSheetLogger.Instance.LogData(GetPlayerIDForLogging(), GetCurrentTime(), GetDayNumber(), "Game Started (Game Manager)", "Game Started, Game Manager Loaded");
        SetQuestAccepted("Nothing");
    }

    public void sleepAndUpdateDay()
    {
        endOfDayManager.ShowPanelAndText(dayNumber);
        dayNumber += 1;
        int armourPercentageToIncrease = armouryUI.GetArmourPercentageToIncrease();
        int moralePercentageToIncrease = infirmaryUI.GetMoralePercentageToIncrease();
        int structurePercentageToIncrease = structureUI.GetStructurePercentageToIncrease();
        int catPercentageToIncrease = catUI.GetCatPercentageToIncrease();

        if(GetLastQuestOfTheDay() == "SleepTime" || GetLastQuestOfTheDay() == "Dead")
        {
            percentageManager.UpdatePercentages(-20,-20,-20,-20);
        }
        else
        {
            percentageManager.UpdatePercentages(moralePercentageToIncrease, armourPercentageToIncrease, structurePercentageToIncrease, catPercentageToIncrease);
        }

        uiManager.UpdateDayText(GetDayNumber());
        armouryUI.ResetArmourPercentageToIncrease();
        infirmaryUI.ResetMoralePercentageToIncrease();
        structureUI.ResetStructurePercentageToIncrease();

    }

    public string GetPlayerIDForLogging()
    {
        return playerId;
    }

    public string GetCurrentTime()
    {
        return System.DateTime.Now.ToString();
    }

    public int GetDayNumber()
    {
        return dayNumber;
    }

    private void BlockOffAreasNotPartOfQuest()
    {
        string currentQuest = GetQuestAccepted();

        switch (currentQuest)
        {
            case "Armoury":
                armouryTeleport.SetActive(true);
                infirmaryTeleport.SetActive(false);
                caveTeleport.SetActive(false);
                bedTeleport.SetActive(false);
                randomQuestSelector.SpawnLootBoxes();
                bedTeleportInside.SetActive(true);
                break;
            case "Infirmary":
                infirmaryTeleport.SetActive(true);
                armouryTeleport.SetActive(false);
                caveTeleport.SetActive(false);
                bedTeleport.SetActive(false);
                randomQuestSelector.SpawnLootBoxes();
                bedTeleportInside.SetActive(true);
                break;
            case "Cat":
                questAccepted = QuestType.Cat.ToString();
                randomQuestSelector.SpawnLootBoxes();
                randomQuestSelector.SpawnCatClues();
                infirmaryTeleport.SetActive(false);
                armouryTeleport.SetActive(false);
                caveTeleport.SetActive(false);
                bedTeleport.SetActive(false);
                bedTeleportInside.SetActive(true);
                break;
            case "Structure":
                questAccepted = QuestType.Structure.ToString();
                randomQuestSelector.SpawnLootBoxes();
                randomQuestSelector.SpawnDebris();
                infirmaryTeleport.SetActive(false);
                armouryTeleport.SetActive(false);
                caveTeleport.SetActive(true);
                bedTeleport.SetActive(false);
                bedTeleportInside.SetActive(true);
                break;
            case "SleepTime":
                questAccepted = QuestType.SleepTime.ToString();
                infirmaryTeleport.SetActive(false);
                armouryTeleport.SetActive(false);
                caveTeleport.SetActive(false);
                bedTeleport.SetActive(true);
                bedTeleportInside.SetActive(true);
                break;
            case "Dead":
                questAccepted = QuestType.Dead.ToString();
                infirmaryTeleport.SetActive(false);
                armouryTeleport.SetActive(false);
                caveTeleport.SetActive(false);
                bedTeleport.SetActive(true);
                bedTeleportInside.SetActive(false);
                break;
            case "Reset":
                questAccepted = QuestType.Reset.ToString();
                infirmaryTeleport.SetActive(false);
                armouryTeleport.SetActive(false);
                caveTeleport.SetActive(false);
                bedTeleport.SetActive(false);
                bedTeleportInside.SetActive(true);
                break;
        }

    }

    public void SetQuestAccepted(string questName)
    {
        switch (questName)
        {
            case "Armoury":
                questAccepted = QuestType.Armoury.ToString();
                lastQuest = QuestType.Armoury.ToString();
                break;
            case "Infirmary":
                questAccepted = QuestType.Infirmary.ToString();
                lastQuest = QuestType.Infirmary.ToString();
                break;
            case "Cat":
                questAccepted = QuestType.Cat.ToString();
                lastQuest = QuestType.Cat.ToString();
                break;
            case "Structure":
                questAccepted = QuestType.Structure.ToString();
                lastQuest = QuestType.Structure.ToString();
                break;
            case "Reset":
                questAccepted = QuestType.Reset.ToString();
                lastQuest = QuestType.Reset.ToString();
                break;
            case "SleepTime":
                questAccepted = QuestType.SleepTime.ToString();
                break;
            case "Dead":
                questAccepted = QuestType.Dead.ToString();
                lastQuest = QuestType.Dead.ToString();

                break;
            case "Nothing":
                questAccepted = QuestType.Nothing.ToString();
                lastQuest = QuestType.Nothing.ToString();
                break;
        }

        if(lastQuest == QuestType.Nothing.ToString())
        {
            GoogleSheetLogger.Instance.LogData(GetPlayerIDForLogging(), GetCurrentTime(), dayNumber, "Quest Accepted (Game Manager)", questAccepted.ToString() == null ? "" : questAccepted.ToString());
        }
            BlockOffAreasNotPartOfQuest();


    }

    public void GameOver()
    {

    }


    public string GetLastQuestOfTheDay()
    {
        return lastQuest;
    }

    public string GetQuestAccepted()
    {
        return questAccepted;
    }

}
