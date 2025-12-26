using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Quest Game Manager")]
    private string questAccepted = "";
    public enum QuestType { Armoury, Infirmary, Cat, Structure, Reset, SleepTime, Dead, Nothing }
    [SerializeField] GameObject fortArmouryTeleport;
    [SerializeField] GameObject fortInfirmaryTeleport;
    [SerializeField] GameObject fortCaveTeleport;
    [SerializeField] GameObject fortBedTeleport;
    [SerializeField] GameObject fortBedTeleportInside;
    [SerializeField] GameObject ottomanArmouryTeleport;
    [SerializeField] GameObject ottomanInfirmaryTeleport;
    [SerializeField] GameObject ottomanEngineerTeleport;
    [SerializeField] GameObject ottomanBedTeleport;

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
    [SerializeField] TeleportPlayer teleportPlayer;
    [SerializeField] GameObject questGiver;
    [SerializeField] Sprite questGiverOttomanSprite;

    [Header("Destruction Per Day Fort")]
    [SerializeField] GameObject afterDayOne;
    [SerializeField] GameObject afterDayTwo;
    [SerializeField] GameObject afterDayThree;
    [SerializeField] GameObject afterDayFour;

    [Header("Destruction Per Day Ottomans")]
    [SerializeField] GameObject afterDayFive;
    [SerializeField] GameObject afterDaySix;
    [SerializeField] GameObject afterDaySeven;
    [SerializeField] GameObject afterDayEight;
    [SerializeField] GameObject afterDayNine;

    [Header("Game Over")]
    [SerializeField] private int gameOverSceneScreen = 5;


    [Header("Day Game Manager")]
    public int dayNumber = 1;

    private SpriteRenderer questGiverSprite;
    private string lastQuest;
    private string playerId;

    public NPCDialog[] npcDialog;

    private void Awake()
    {
        foreach (NPCDialog dialog in npcDialog)
        {
            dialog.ResetData();
        }

        questGiverSprite = questGiver.GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        playerId = SystemInfo.deviceUniqueIdentifier;
        dayNumber = 1;
        fortArmouryTeleport.SetActive(false);
        fortInfirmaryTeleport.SetActive(false);
        fortCaveTeleport.SetActive(false);
        fortBedTeleport.SetActive(false);
        ottomanArmouryTeleport.SetActive(false);
        ottomanInfirmaryTeleport.SetActive(false);
        ottomanEngineerTeleport.SetActive(false);
        ottomanBedTeleport.SetActive(false);

        //GoogleSheetLogger.Instance.LogData(GetPlayerIDForLogging(), GetCurrentTime(), GetDayNumber(), "Game Started (Game Manager)", "Game Started, Game Manager Loaded");
        GoogleSheetLogger.I.StartNewPlaythrough();

        SetQuestAccepted("Nothing");
    }

    public void SleepAndUpdateDay()
    {
        dayNumber += 1;
        int armourPercentageToIncrease = armouryUI.GetArmourPercentageToIncrease();
        int moralePercentageToIncrease = infirmaryUI.GetMoralePercentageToIncrease();
        int structurePercentageToIncrease = structureUI.GetStructurePercentageToIncrease();
        int catPercentageToIncrease = catUI.GetCatPercentageToIncrease();

        List<int> currentPercenatages = new List<int>();

        if (GetLastQuestOfTheDay() == "SleepTime" || GetLastQuestOfTheDay() == "Dead" || GetLastQuestOfTheDay() == "Nothing")
        {

            Debug.Log($"Decrease armour by: 20");
            Debug.Log($"Decrease moral by: 20");
            Debug.Log($"Decrease structure by: 20");
            Debug.Log($"Decrease cat by: 20");

            percentageManager.UpdatePercentages(-20,-20,-20,-20);
        }
        else
        {
            Debug.Log($"Increase armour by: {armourPercentageToIncrease}");
            Debug.Log($"Increase moral by: {moralePercentageToIncrease}");
            Debug.Log($"Increase structure by: {structurePercentageToIncrease}");
            Debug.Log($"Increase cat by: {catPercentageToIncrease}");

            percentageManager.UpdatePercentages(moralePercentageToIncrease, armourPercentageToIncrease, structurePercentageToIncrease, catPercentageToIncrease);
        }

        currentPercenatages = percentageManager.GetPercentages();

        int index = 0;
        foreach(int currentPercentage in currentPercenatages)
        {
            index++;
            if(currentPercentage <= 0 && index < 3)
            {
                GameOver();
            }
        }

        endOfDayManager.ShowPanelAndText(dayNumber);
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
                fortArmouryTeleport.SetActive(true);
                fortInfirmaryTeleport.SetActive(false);
                fortCaveTeleport.SetActive(false);
                fortBedTeleport.SetActive(false);
                ottomanArmouryTeleport.SetActive(true);
                ottomanInfirmaryTeleport.SetActive(false);
                ottomanEngineerTeleport.SetActive(false);
                ottomanBedTeleport.SetActive(false);
                randomQuestSelector.SpawnLootBoxes();
                fortBedTeleportInside.SetActive(true);
                break;
            case "Infirmary":
                fortInfirmaryTeleport.SetActive(true);
                fortArmouryTeleport.SetActive(false);
                fortCaveTeleport.SetActive(false);
                fortBedTeleport.SetActive(false);
                ottomanArmouryTeleport.SetActive(false);
                ottomanInfirmaryTeleport.SetActive(true);
                ottomanEngineerTeleport.SetActive(false);
                ottomanBedTeleport.SetActive(false);
                randomQuestSelector.SpawnLootBoxes();
                fortBedTeleportInside.SetActive(true);
                break;
            case "Cat":
                questAccepted = QuestType.Cat.ToString();
                randomQuestSelector.SpawnLootBoxes();
                randomQuestSelector.SpawnCatClues();
                fortInfirmaryTeleport.SetActive(false);
                fortArmouryTeleport.SetActive(false);
                fortCaveTeleport.SetActive(false);
                fortBedTeleport.SetActive(false);
                ottomanArmouryTeleport.SetActive(false);
                ottomanInfirmaryTeleport.SetActive(false);
                ottomanEngineerTeleport.SetActive(false);
                ottomanBedTeleport.SetActive(false);
                fortBedTeleportInside.SetActive(true);
                break;
            case "Structure":
                questAccepted = QuestType.Structure.ToString();
                randomQuestSelector.SpawnLootBoxes();
                randomQuestSelector.SpawnDebris();
                fortInfirmaryTeleport.SetActive(false);
                fortArmouryTeleport.SetActive(false);
                fortCaveTeleport.SetActive(true);
                fortBedTeleport.SetActive(false);
                ottomanArmouryTeleport.SetActive(false);
                ottomanInfirmaryTeleport.SetActive(false);
                ottomanEngineerTeleport.SetActive(true);
                ottomanBedTeleport.SetActive(false);
                fortBedTeleportInside.SetActive(true);
                break;
            case "SleepTime":
                questAccepted = QuestType.SleepTime.ToString();
                fortInfirmaryTeleport.SetActive(false);
                fortArmouryTeleport.SetActive(false);
                fortCaveTeleport.SetActive(false);
                fortBedTeleport.SetActive(true);
                fortBedTeleportInside.SetActive(true);
                ottomanArmouryTeleport.SetActive(false);
                ottomanInfirmaryTeleport.SetActive(false);
                ottomanEngineerTeleport.SetActive(false);
                ottomanBedTeleport.SetActive(true);
                break;
            case "Dead":
                questAccepted = QuestType.Dead.ToString();
                fortInfirmaryTeleport.SetActive(false);
                fortArmouryTeleport.SetActive(false);
                fortCaveTeleport.SetActive(false);
                fortBedTeleport.SetActive(true);
                fortBedTeleportInside.SetActive(true);
                ottomanArmouryTeleport.SetActive(false);
                ottomanInfirmaryTeleport.SetActive(false);
                ottomanEngineerTeleport.SetActive(false);
                ottomanBedTeleport.SetActive(true);
                break;
            case "Reset":
                questAccepted = QuestType.Reset.ToString();
                fortInfirmaryTeleport.SetActive(false);
                fortArmouryTeleport.SetActive(false);
                fortCaveTeleport.SetActive(false);
                fortBedTeleport.SetActive(false);
                fortBedTeleportInside.SetActive(true);
                ottomanArmouryTeleport.SetActive(false);
                ottomanInfirmaryTeleport.SetActive(false);
                ottomanEngineerTeleport.SetActive(false);
                ottomanBedTeleport.SetActive(true);
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
            GoogleSheetLogger.I.Log(dayNumber, "Quest Accepted (Game Manager)", questAccepted.ToString() == null ? "Null" : questAccepted.ToString());
            BlockOffAreasNotPartOfQuest();
    }

    public void DestructionPerDay()
    {
        Debug.Log("DEST:" + dayNumber);
        if (dayNumber == 2)
        {
            afterDayOne.SetActive(true);
        }else if (dayNumber == 3)
        {
            afterDayTwo.SetActive(true);
        }
        else if (dayNumber == 4)
        {
            afterDayThree.SetActive(true);
        }
        else if (dayNumber == 5)
        {
            afterDayFour.SetActive(true);
        }
        else if (dayNumber == 7)
        {
            afterDayFive.SetActive(false);
        }
        else if (dayNumber == 8)
        {
            afterDaySix.SetActive(false);
        }
        else if (dayNumber == 9)
        {
            afterDaySeven.SetActive(false);
        }
        else if (dayNumber == 10)
        {
            afterDayEight.SetActive(false);
        }
        else if (dayNumber == 11)
        {
            afterDayNine.SetActive(true);
        }
    }

    public void ResetAllSpawns()
    {
        randomQuestSelector.DespawnCatClues();
        randomQuestSelector.DespawnLootBoxes();
        randomQuestSelector.DespawnSpawnDebris();
    }

    public void GetCaptured()
    {
        teleportPlayer.TeleportPlayerManually(new Vector3 (-73, -109, 0));
        TeleportQuestGiver();
    }

    public void GameOver()
    {
        SceneManager.LoadScene(gameOverSceneScreen);
    }

    public void MoveToVillage()
    {
        teleportPlayer.TeleportPlayerManually(new Vector3(-381, -84, 0));
    }

    public void TeleportQuestGiver()
    {
        teleportPlayer.TeleportQuestGiver(new Vector3(-2.808f, -19.418f, 0));
        questGiverSprite.sprite = questGiverOttomanSprite;
    }

    public void EndGame()
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
