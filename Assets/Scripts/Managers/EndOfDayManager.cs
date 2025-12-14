using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndOfDayManager : MonoBehaviour
{

    [Header("Config")]
    [SerializeField] private List<string> endOfDayDescriptionArmoury;
    [SerializeField] private List<string> endOfDayDescriptionInfirmary;
    [SerializeField] private List<string> endOfDayDescriptionStructure;
    [SerializeField] private List<string> endOfDayDescriptionCat;
    [SerializeField] private float timeForPanelToDisappear;
    [SerializeField] private SleepManager sleepManager;

    [SerializeField] private TextMeshProUGUI endOfDayDescriptionTMP;
    [SerializeField] private TextMeshProUGUI endOfDayNumber;
    [SerializeField] private GameObject endOfDayPanel;
    [SerializeField] private DayNightCycleManager dayNightCycleManager;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GoogleSheetLogger logger;


    public void ShowPanelAndText(int dayNumber)
    {
        sleepManager.isSleeping = true;
        UpdateTextAccordingToPlayerActions(dayNumber);
        endOfDayNumber.text = $"Day {dayNumber}";
        StartCoroutine(ShowAndHidePanel());
    }

    private void UpdateTextAccordingToPlayerActions(int dayNumber)
    {
        string lastQuest = gameManager.GetLastQuestOfTheDay();
        //logger.LogData(gameManager.GetPlayerIDForLogging(), gameManager.GetCurrentTime(), gameManager.GetDayNumber(), "Player Sleep (EndOfDay Manager)", $"Day {dayNumber} ended and player went to sleep");
        GoogleSheetLogger.I.Log(gameManager.GetDayNumber(), "Player Sleep (EndOfDay Manager)", $"Day {dayNumber} ended and player went to sleep");
        GoogleSheetLogger.I.FlushDay();
        GoogleSheetLogger.I.StartDay(gameManager.GetDayNumber() + 1);

        if (lastQuest == "Armoury")
        {
            endOfDayDescriptionTMP.text = endOfDayDescriptionArmoury[dayNumber - 1];
        }
        else if (lastQuest == "Infirmary")
        {
            endOfDayDescriptionTMP.text = endOfDayDescriptionInfirmary[dayNumber - 1];
        }
        else if (lastQuest == "Structure")
        {
            endOfDayDescriptionTMP.text = endOfDayDescriptionStructure[dayNumber - 1];
        }
        else if (lastQuest == "Cat")
        {
            endOfDayDescriptionTMP.text = endOfDayDescriptionCat[dayNumber - 1];
        }
        else if (gameManager.GetQuestAccepted() == "Reset")
        {
            endOfDayDescriptionTMP.text = "Your hands stayed still as rubble piled higher and soldiers bled into the dirt. Maybe the weight of despair froze you, maybe the absence of your cat hollowed you. Tomorrow, perhaps, you will act.";
        }
        else if (gameManager.GetQuestAccepted() == "SleepTime")
        {
            endOfDayDescriptionTMP.text = "Your hands stayed still as rubble piled higher and soldiers bled into the dirt. Maybe the weight of despair froze you, maybe the absence of your cat hollowed you. Tomorrow, perhaps, you will act.";
        }
        else if (gameManager.GetQuestAccepted() == "Dead")
        {
            endOfDayDescriptionTMP.text = "You fell unconscious, someone helped you towards the bed chambers — the fort’s fate continued without your help.";
        }
    }


    IEnumerator ShowAndHidePanel()
    {
        endOfDayPanel.SetActive(true);
        yield return new WaitForSeconds(timeForPanelToDisappear);
        endOfDayPanel.SetActive(false);
        dayNightCycleManager.ResetTime();
        sleepManager.isSleeping = false;
    }

}
