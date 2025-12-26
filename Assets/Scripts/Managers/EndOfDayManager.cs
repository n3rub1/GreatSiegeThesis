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
    [SerializeField] private TeleportPlayer teleportPlayer;

    [Header("Data")]
    [SerializeField] private TextMeshProUGUI endOfDayDescriptionTMP;
    //[SerializeField] private TextMeshProUGUI endOfDayWhatIsHappeningTMP;
    [SerializeField] private TextMeshProUGUI endOfDayNumber;
    [SerializeField] private Image endOfDayImage;
    [SerializeField] private GameObject endOfDayPanel;
    [SerializeField] private DayNightCycleManager dayNightCycleManager;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GoogleSheetLogger logger;
    [SerializeField] private AudioSource endOfDayAudioSource;
    [SerializeField] private TextMeshProUGUI moralPercentageTMP;
    [SerializeField] private TextMeshProUGUI weaponPercentageTMP;
    [SerializeField] private TextMeshProUGUI structurePercentageTMP;
    [SerializeField] private TextMeshProUGUI catPercentageTMP;
    [SerializeField] private TextMeshProUGUI moralIncreaseDecreasePercentageTMP;
    [SerializeField] private TextMeshProUGUI weaponIncreaseDecreasePercentageTMP;
    [SerializeField] private TextMeshProUGUI structureIncreaseDecreasePercentageTMP;
    [SerializeField] private TextMeshProUGUI catIncreaseDecreasePercentageTMP;



    [Header("Percentage Images")]
    [SerializeField] private List<SpriteRenderer> percentageImages = new List<SpriteRenderer>();
    [SerializeField] private PercentageManager percentageManager;


    public void ShowPanelAndText(int dayNumber)
    {
        dayNumber = dayNumber - 2;

        sleepManager.isSleeping = true;
        UpdateTextAccordingToPlayerActions(dayNumber);
        //UpdateWhatIsHappening(dayNumber);
        UpdateImages(dayNumber);
        SetImageTrasparency();

        List<int> allPercentages = percentageManager.GetPercentages();
        List<int> allIncreaseDecreasePercenatges = percentageManager.GetIncreaseDecreasePercenatageNumbers();

        moralPercentageTMP.text = $"{allPercentages[0].ToString()} %";
        weaponPercentageTMP.text = $"{allPercentages[1].ToString()} %";
        structurePercentageTMP.text = $"{allPercentages[2].ToString()} %";
        catPercentageTMP.text = $"{allPercentages[3].ToString()} %";

        moralIncreaseDecreasePercentageTMP.text = $"{(allIncreaseDecreasePercenatges[0] >= 0 ? "+" : "")}{allIncreaseDecreasePercenatges[0]} %"; ;
        weaponIncreaseDecreasePercentageTMP.text = $"{(allIncreaseDecreasePercenatges[1] >= 0 ? "+" : "")}{allIncreaseDecreasePercenatges[1]} %"; ;
        structureIncreaseDecreasePercentageTMP.text = $"{(allIncreaseDecreasePercenatges[2] >= 0 ? "+" : "")}{allIncreaseDecreasePercenatges[2]} %"; ;
        catIncreaseDecreasePercentageTMP.text = $"{(allIncreaseDecreasePercenatges[3] >= 0 ? "+" : "")}{allIncreaseDecreasePercenatges[3]} %";

        endOfDayNumber.text = $"End of Day {dayNumber + 1}: {endOfDayTitle[dayNumber]}";

        StartCoroutine(ShowAndHidePanel());
    }

    private void UpdateTextAccordingToPlayerActions(int dayNumber)
    {
        string lastQuest = gameManager.GetLastQuestOfTheDay();
        //logger.LogData(gameManager.GetPlayerIDForLogging(), gameManager.GetCurrentTime(), gameManager.GetDayNumber(), "Player Sleep (EndOfDay Manager)", $"Day {dayNumber} ended and player went to sleep");
        GoogleSheetLogger.I.Log(gameManager.GetDayNumber(), "Player Sleep (EndOfDay Manager)", $"Day {dayNumber} ended and player went to sleep");
        GoogleSheetLogger.I.FlushDay();
        GoogleSheetLogger.I.StartDay(gameManager.GetDayNumber() + 1);

        if(dayNumber < 5)
        {
            if (lastQuest == "Armoury")
            {
                endOfDayDescriptionTMP.text = UpdateWhatIsHappening(dayNumber) + "\n\n" + endOfDayDescriptionArmoury[dayNumber];
            }
            else if (lastQuest == "Infirmary")
            {
                endOfDayDescriptionTMP.text = UpdateWhatIsHappening(dayNumber) + "\n\n" + endOfDayDescriptionInfirmary[dayNumber];
            }
            else if (lastQuest == "Structure")
            {
                endOfDayDescriptionTMP.text = UpdateWhatIsHappening(dayNumber) + "\n\n" + endOfDayDescriptionStructure[dayNumber];
            }
            else if (lastQuest == "Cat")
            {
                endOfDayDescriptionTMP.text = UpdateWhatIsHappening(dayNumber) + "\n\n" + endOfDayDescriptionCat[dayNumber];
            }
            else if (gameManager.GetQuestAccepted() == "Reset" || gameManager.GetQuestAccepted() == "Nothing")
            {
                endOfDayDescriptionTMP.text = UpdateWhatIsHappening(dayNumber) + "\n\n" + "You stood still today. While you waited, men bled without hands to hold them, weapons stayed dull on the racks, and rubble buried someone no one reached in time. The fort paid for your silence. The cat survived by moving, hiding, choosing. You didn’t. Tomorrow, the cost of doing nothing will be harder to ignore.";
            }
            else if (gameManager.GetQuestAccepted() == "SleepTime")
            {
                endOfDayDescriptionTMP.text = UpdateWhatIsHappening(dayNumber) + "\n\n" + "You stood still today. While you waited, men bled without hands to hold them, weapons stayed dull on the racks, and rubble buried someone no one reached in time. The fort paid for your silence. The cat survived by moving, hiding, choosing. You didn’t. Tomorrow, the cost of doing nothing will be harder to ignore.";
            }
            else if (gameManager.GetQuestAccepted() == "Dead")
            {
                endOfDayDescriptionTMP.text = UpdateWhatIsHappening(dayNumber) + "\n\n" + "You took a blow you couldn’t walk off, and they dragged you to a bunk until you stopped spinning. While you lay there, others swung hammers, moved bodies, and tried to keep the walls standing. You didn’t help anyone today, and the cat searched for safety without you. Tomorrow you’ll wake up owing more than effort.";
            }
        }else if(dayNumber == 5)
        {
            endOfDayDescriptionTMP.text = UpdateWhatIsHappening(dayNumber) + "\n\n" + "You lay down because you couldn’t stand anymore. At some point in the night, the fighting broke through and men came into the room. They didn’t bother killing you—they just dragged you out like you were cargo. Maybe they need workers, maybe they didn’t see you as worth the blade. You didn’t help anyone today, and you never got close to the cat. Now you’re somewhere else, alive, confused, and not in control of anything that happens next.";
        }else if (dayNumber > 5)
        {

        }

        PlayClip(dayNumber);
    }

    private string UpdateWhatIsHappening(int dayNumber)
    {
         return endOfDayWhatIsHappening[dayNumber];
    }

    private void PlayClip(int dayNumber)
    {
        AudioClip clip = endOfDayWhatIsHappeningAudioClip[dayNumber];
        endOfDayAudioSource.clip = clip;
        endOfDayAudioSource.Play();
        //timeForPanelToDisappear = clip.length + 5;
        timeForPanelToDisappear = 3;  //FOR TESTING ONLY!
    }

    private void UpdateImages(int dayNumber)
    {
        endOfDayImage.sprite = endOfDayImages[dayNumber];
    }

    private void SetImageTrasparency()
    {
        List<int> allPercentages = percentageManager.GetPercentages();
        int count = Mathf.Min(allPercentages.Count, percentageImages.Count);

        for (int i = 0; i < count; i++)
        {
            Color c = percentageImages[i].color;
            c.a = Mathf.Clamp01(allPercentages[i] / 100f);
            percentageImages[i].color = c;
        }

    }

    private float ImageTransparencyCalculation(int percentage)
    {
        float transparencyCalculation =  Mathf.Clamp01(percentage / 100f);
        if (transparencyCalculation == 0)
        {
            return 0.02f;
        }

        return transparencyCalculation;

    }

    private void TeleportForSoundIssue(Vector3 originalPosition, bool firstTime)
    {
        teleportPlayer.TeleportAndRevert(originalPosition, new Vector3(-900, -900, 0), firstTime);
    }


    IEnumerator ShowAndHidePanel()
    {
        endOfDayPanel.SetActive(true);
        Vector3 originalPosition= teleportPlayer.GetCurrentLocation();
        TeleportForSoundIssue(originalPosition, true);
        yield return new WaitForSeconds(timeForPanelToDisappear);
        TeleportForSoundIssue(originalPosition, false);
        endOfDayAudioSource.Stop();
        endOfDayPanel.SetActive(false);
        dayNightCycleManager.ResetTime();
        sleepManager.isSleeping = false;
    }

}
