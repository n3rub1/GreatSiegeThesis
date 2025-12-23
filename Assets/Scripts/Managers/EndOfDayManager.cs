using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndOfDayManager : MonoBehaviour
{

    [Header("Config")]
    [SerializeField] private List<string> endOfDayDescriptionArmoury;
    [SerializeField] private List<string> endOfDayDescriptionInfirmary;
    [SerializeField] private List<string> endOfDayDescriptionStructure;
    [SerializeField] private List<string> endOfDayDescriptionCat;
    [SerializeField] private List<string> endOfDayWhatIsHappening;
    [SerializeField] private List<string> endOfDayTitle;
    [SerializeField] private List<AudioClip> endOfDayWhatIsHappeningAudioClip;
    [SerializeField] private List<Sprite> endOfDayImages;
    [SerializeField] private float timeForPanelToDisappear;
    [SerializeField] private SleepManager sleepManager;

    [Header("Data")]
    [SerializeField] private TextMeshProUGUI endOfDayDescriptionTMP;
    [SerializeField] private TextMeshProUGUI endOfDayWhatIsHappeningTMP;
    [SerializeField] private TextMeshProUGUI endOfDayNumber;
    [SerializeField] private Image endOfDayImage;
    [SerializeField] private GameObject endOfDayPanel;
    [SerializeField] private DayNightCycleManager dayNightCycleManager;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GoogleSheetLogger logger;
    [SerializeField] private AudioSource endOfDayAudioSource;



    public void ShowPanelAndText(int dayNumber)
    {
        sleepManager.isSleeping = true;
        UpdateTextAccordingToPlayerActions(dayNumber);
        UpdateWhatIsHappening(dayNumber);
        UpdateImages(dayNumber);
        endOfDayNumber.text = $"Day {dayNumber}: {endOfDayTitle[dayNumber-1]}";
        StartCoroutine(ShowAndHidePanel());
    }

    private void UpdateTextAccordingToPlayerActions(int dayNumber)
    {
        string lastQuest = gameManager.GetLastQuestOfTheDay();
        //logger.LogData(gameManager.GetPlayerIDForLogging(), gameManager.GetCurrentTime(), gameManager.GetDayNumber(), "Player Sleep (EndOfDay Manager)", $"Day {dayNumber} ended and player went to sleep");
        GoogleSheetLogger.I.Log(gameManager.GetDayNumber(), "Player Sleep (EndOfDay Manager)", $"Day {dayNumber} ended and player went to sleep");
        GoogleSheetLogger.I.FlushDay();
        GoogleSheetLogger.I.StartDay(gameManager.GetDayNumber() + 1);

        Debug.Log("dayNumber:" + dayNumber);

        if(dayNumber < 5)
        {
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
            else if (gameManager.GetQuestAccepted() == "Reset" || gameManager.GetQuestAccepted() == "Nothing")
            {
                endOfDayDescriptionTMP.text = "You stood still today. While you waited, men bled without hands to hold them, weapons stayed dull on the racks, and rubble buried someone no one reached in time. The fort paid for your silence. The cat survived by moving, hiding, choosing. You didn’t. Tomorrow, the cost of doing nothing will be harder to ignore.";
            }
            else if (gameManager.GetQuestAccepted() == "SleepTime")
            {
                endOfDayDescriptionTMP.text = "You stood still today. While you waited, men bled without hands to hold them, weapons stayed dull on the racks, and rubble buried someone no one reached in time. The fort paid for your silence. The cat survived by moving, hiding, choosing. You didn’t. Tomorrow, the cost of doing nothing will be harder to ignore.";
            }
            else if (gameManager.GetQuestAccepted() == "Dead")
            {
                endOfDayDescriptionTMP.text = "You took a blow you couldn’t walk off, and they dragged you to a bunk until you stopped spinning. While you lay there, others swung hammers, moved bodies, and tried to keep the walls standing. You didn’t help anyone today, and the cat searched for safety without you. Tomorrow you’ll wake up owing more than effort.";
            }
        }else if(dayNumber == 5)
        {
            endOfDayDescriptionTMP.text = "You lay down because you couldn’t stand anymore. At some point in the night, the fighting broke through and men came into the room. They didn’t bother killing you—they just dragged you out like you were cargo. Maybe they need workers, maybe they didn’t see you as worth the blade. You didn’t help anyone today, and you never got close to the cat. Now you’re somewhere else, alive, confused, and not in control of anything that happens next.";
        }else if (dayNumber > 5)
        {

        }
    }

    private void UpdateWhatIsHappening(int dayNumber)
    {
        endOfDayWhatIsHappeningTMP.text = endOfDayWhatIsHappening[dayNumber - 1];
        AudioClip clip = endOfDayWhatIsHappeningAudioClip[dayNumber - 1];
        endOfDayAudioSource.clip = clip;
        endOfDayAudioSource.Play();

        timeForPanelToDisappear = clip.length + 5;
    }

    private void UpdateImages(int dayNumber)
    {
        endOfDayImage.sprite = endOfDayImages[dayNumber - 1];
    }


    IEnumerator ShowAndHidePanel()
    {
        endOfDayPanel.SetActive(true);
        yield return new WaitForSeconds(timeForPanelToDisappear);
        endOfDayAudioSource.Stop();
        endOfDayPanel.SetActive(false);
        dayNightCycleManager.ResetTime();
        sleepManager.isSleeping = false;
    }

}
