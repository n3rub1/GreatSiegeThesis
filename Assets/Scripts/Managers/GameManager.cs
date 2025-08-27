using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Quest Game Manager")]
    private string questAccepted = "";
    public enum QuestType { Armoury, Infirmary, Cat, Structure }
    [SerializeField] GameObject armouryTeleport;
    [SerializeField] GameObject infirmaryTeleport;
    [SerializeField] RandomQuestSelector randomQuestSelector;
    [SerializeField] EndOfDayManager endOfDayManager;


    [Header("Day Game Manager")]
    [SerializeField] private TextMeshProUGUI dayNumberTMP;
    public int dayNumber = 1;



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

    }

    public void sleepAndUpdateDay()
    {
        endOfDayManager.ShowPanelAndText(dayNumber);
        dayNumber += 1;
        UpdateDayText();
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
                randomQuestSelector.SpawnLootBoxes();
                break;
            case "Infirmary":
                infirmaryTeleport.SetActive(true);
                armouryTeleport.SetActive(false);
                randomQuestSelector.SpawnLootBoxes();
                break;
            case "Cat":
                questAccepted = QuestType.Cat.ToString();
                randomQuestSelector.SpawnLootBoxes();
                randomQuestSelector.SpawnCatClues();
                break;
            case "Structure":
                questAccepted = QuestType.Structure.ToString();
                randomQuestSelector.SpawnLootBoxes();
                randomQuestSelector.SpawnDebris();
                break;
        }

    }

    public void SetQuestAccepted(string questName)
    {
        switch (questName)
        {
            case "Armoury":
                questAccepted = QuestType.Armoury.ToString();
                break;
            case "Infirmary":
                questAccepted = QuestType.Infirmary.ToString();
                break;
            case "Cat":
                questAccepted = QuestType.Cat.ToString();
                break;
            case "Structure":
                questAccepted = QuestType.Structure.ToString();
                break;
        }

        BlockOffAreasNotPartOfQuest();

    }

    public string GetQuestAccepted()
    {
        return questAccepted;
    }

    private void UpdateDayText()
    {
        dayNumberTMP.text = $"Day: {getDayNumber()}";
    }

}
