using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Quest Game Manager")]
    private string questAccepted = "";
    public enum QuestType { Armoury, Infirmary, Cat, Structure, Reset, SleepTime }
    [SerializeField] GameObject armouryTeleport;
    [SerializeField] GameObject infirmaryTeleport;
    [SerializeField] GameObject caveTeleport;
    [SerializeField] GameObject bedTeleport;
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

    private string lastQuest = "";
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
        armouryTeleport.SetActive(false);
        infirmaryTeleport.SetActive(false);
        caveTeleport.SetActive(false);
        bedTeleport.SetActive(false);
        playerId = SystemInfo.deviceUniqueIdentifier;
        logger.LogData(playerId, System.DateTime.Now.ToString(), "Game Start", 1);
    }

    public void sleepAndUpdateDay()
    {
        endOfDayManager.ShowPanelAndText(dayNumber);
        dayNumber += 1;
        int armourPercentageToIncrease = armouryUI.GetArmourPercentageToIncrease();
        int moralePercentageToIncrease = infirmaryUI.GetMoralePercentageToIncrease();
        int structurePercentageToIncrease = structureUI.GetStructurePercentageToIncrease();
        int catPercentageToIncrease = catUI.GetCatPercentageToIncrease();
        percentageManager.UpdatePercentages(moralePercentageToIncrease, armourPercentageToIncrease, structurePercentageToIncrease, catPercentageToIncrease);
        uiManager.UpdateDayText(getDayNumber());
        armouryUI.ResetArmourPercentageToIncrease();
        infirmaryUI.ResetMoralePercentageToIncrease();
        structureUI.ResetStructurePercentageToIncrease();
    }

    public int getDayNumber()
    {
        return dayNumber;
    }

    public void BlockOffAreasNotPartOfQuest()
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
                break;
            case "Infirmary":
                infirmaryTeleport.SetActive(true);
                armouryTeleport.SetActive(false);
                caveTeleport.SetActive(false);
                bedTeleport.SetActive(false);
                randomQuestSelector.SpawnLootBoxes();
                break;
            case "Cat":
                questAccepted = QuestType.Cat.ToString();
                randomQuestSelector.SpawnLootBoxes();
                randomQuestSelector.SpawnCatClues();
                infirmaryTeleport.SetActive(false);
                armouryTeleport.SetActive(false);
                caveTeleport.SetActive(false);
                bedTeleport.SetActive(false);
                break;
            case "Structure":
                questAccepted = QuestType.Structure.ToString();
                randomQuestSelector.SpawnLootBoxes();
                randomQuestSelector.SpawnDebris();
                infirmaryTeleport.SetActive(false);
                armouryTeleport.SetActive(false);
                caveTeleport.SetActive(true);
                bedTeleport.SetActive(false);
                break;
            case "SleepTime":
                questAccepted = QuestType.SleepTime.ToString();
                infirmaryTeleport.SetActive(false);
                armouryTeleport.SetActive(false);
                caveTeleport.SetActive(false);
                bedTeleport.SetActive(true);
                break;
            case "Reset":
                questAccepted = QuestType.Reset.ToString();
                infirmaryTeleport.SetActive(false);
                armouryTeleport.SetActive(false);
                caveTeleport.SetActive(false);
                bedTeleport.SetActive(false);
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
                logger.LogData(playerId, System.DateTime.Now.ToString(), questAccepted, dayNumber);
                break;
            case "Infirmary":
                questAccepted = QuestType.Infirmary.ToString();
                lastQuest = QuestType.Infirmary.ToString();
                logger.LogData(playerId, System.DateTime.Now.ToString(), questAccepted, dayNumber);
                break;
            case "Cat":
                questAccepted = QuestType.Cat.ToString();
                lastQuest = QuestType.Cat.ToString();
                logger.LogData(playerId, System.DateTime.Now.ToString(), questAccepted, dayNumber);
                break;
            case "Structure":
                questAccepted = QuestType.Structure.ToString();
                lastQuest = QuestType.Structure.ToString();
                logger.LogData(playerId, System.DateTime.Now.ToString(), questAccepted, dayNumber);
                break;
            case "Reset":
                questAccepted = QuestType.Reset.ToString();
                break;
            case "SleepTime":
                questAccepted = QuestType.SleepTime.ToString();
                break;
        }

        BlockOffAreasNotPartOfQuest();

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
