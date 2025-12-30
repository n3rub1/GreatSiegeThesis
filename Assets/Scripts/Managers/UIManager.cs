using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{

    [Header("Config")]
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GoogleSheetLogger logger;

    [Header ("Stats")]
    [SerializeField] private PlayerStats stats;

    [Header("Bars")]
    //[SerializeField] private Image healthBar;
    //[SerializeField] private Image staminaBar;
    [SerializeField] private Image xpBarSupplies;
    [SerializeField] private Image xpBarArmour;
    [SerializeField] private Image xpBarMedicine;


    [Header("Text")]
    [SerializeField] private TextMeshProUGUI levelTMP;
    //[SerializeField] private TextMeshProUGUI healthTMP;
    //[SerializeField] private TextMeshProUGUI staminaTMP;
    [SerializeField] private TextMeshProUGUI xpSuppliesTMP;
    [SerializeField] private TextMeshProUGUI xpArmourTMP;
    [SerializeField] private TextMeshProUGUI xpMedicineTMP;
    [SerializeField] private TextMeshProUGUI timeOfDayTMP;
    [SerializeField] private TextMeshProUGUI dayNumberTMP;
    [SerializeField] private TextMeshProUGUI AIVoiceActingTMP;
    [SerializeField] private TextMeshProUGUI currentLocation;

    [Header("Level Up")]
    [SerializeField] private TextMeshProUGUI LevelUpTMP;
    [SerializeField] private GameObject LevelUp;

    [Header("Percentages")]
    [SerializeField] private TextMeshProUGUI moralPercentageTMP;
    [SerializeField] private TextMeshProUGUI weaponsPercentageTMP;
    [SerializeField] private TextMeshProUGUI structurePercentageTMP;
    [SerializeField] private TextMeshProUGUI catPercentageTMP;
    [SerializeField] private int moralPercentage = 100;
    [SerializeField] private int weaponsPercentage = 100;
    [SerializeField] private int structurePercentage = 100;
    [SerializeField] private int catPercentage = 0;
    [SerializeField] private string moraleText = "Morale: ";
    [SerializeField] private string weaponsText = "Weapons: ";
    [SerializeField] private string structureText = "Structure: ";
    [SerializeField] private string catText = "Cat: ";

    private string[] suppliesTexts = new string[]
{
        "Your instincts sharpen — you now scavenge with precision, uncovering more vital supplies.",
        "Your eyes grow keen — hidden caches reveal themselves to your searching hands.",
        "Your senses awaken — every search yields more essential resources.",
        "Your resourcefulness flourishes — scavenging now brings forth treasures from the overlooked."
};

    private string[] armouryTexts = new string[]
    {
        "Your craftsmanship ascends — every strike of the hammer wastes less iron, forging and repairing weapons with greater efficiency.",
        "Your mastery grows — weapons are forged sturdier, faster, and with fewer resources.",
        "Your hands steady — the armoury now thrives under your skilled touch.",
        "Your forge sings — iron bends willingly, creating stronger tools of war."
    };

    private string[] medicineTexts = new string[]
    {
        "Your healer’s eye awakens — you discern wounds with clarity, restoring soldiers with greater care.",
        "Your touch steadies — the injured recover faster under your guidance.",
        "Your knowledge deepens — wounds that once doomed men now mend beneath your skill.",
        "Your gift blossoms — each life you tend endures with greater strength."
    };

    private void Start()
    {
        stats.ResetPlayer();

        moralPercentageTMP.text = moraleText + moralPercentage.ToString() + "%";
        weaponsPercentageTMP.text = weaponsText + weaponsPercentage.ToString() + "%";
        structurePercentageTMP.text = structureText + structurePercentage.ToString() + "%";
        catPercentageTMP.text = catText + catPercentage.ToString() + "%";
    }

    private void Update()
    {
        UpdatePlayerUI();
    }

    private void UpdatePlayerUI()
    {
        //old code
        //healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, stats.Health / stats.MaxHealth, 10f*Time.deltaTime);
        //staminaBar.fillAmount = Mathf.Lerp(staminaBar.fillAmount, stats.Stamina / stats.MaxStamina, 10f * Time.deltaTime);
        //xpBar.fillAmount = Mathf.Lerp(xpBar.fillAmount, stats.CurrentXPArmour / stats.NextLevelXPArmour, 10f * Time.deltaTime);

        //levelTMP.text = $"Level {stats.Level}";
        //healthTMP.text = $"HP {stats.Health} / {stats.MaxHealth}";
        //staminaTMP.text = $"STM {stats.Stamina} / {stats.MaxStamina}";
        xpSuppliesTMP.text = $"XP Supplies {stats.CurrentXPSupplies} / {stats.NextLevelXPSupplies} -- Level {stats.SuppliesLevel}";

        xpArmourTMP.text = $"XP Armoury {stats.CurrentXPArmour} / {stats.NextLevelXPArmour} -- Level {stats.ArmourLevel}";

        xpMedicineTMP.text = $"XP Medicine {stats.CurrentXPMedicine} / {stats.NextLevelXPMedicine} -- Level {stats.MedicineLevel}";
    }

    public void UpdateTime(int timeOfDay)
    {
        if (timeOfDay >= 22)
        {
            timeOfDayTMP.color = new Color(255, 0, 0);
            timeOfDayTMP.text = "It's late.  Get some sleep";
        }
        else
        {
            timeOfDayTMP.color = new Color(0, 0, 0);
            timeOfDayTMP.text = $"Time: {timeOfDay}:00";
        }
    }

    public void UpdateDayText(int dayNumber)
    {
        dayNumberTMP.text = $"Day: {dayNumber}";
    }

    public void UpdatePercentages(int moralPercentage, int weaponsPercentage, int structurePercentage, int catPercentage)
    {
        moralPercentageTMP.text = moraleText + moralPercentage.ToString() + "%";
        weaponsPercentageTMP.text = weaponsText + weaponsPercentage.ToString() + "%";
        structurePercentageTMP.text = structureText + structurePercentage.ToString() + "%";
        catPercentageTMP.text = catText + catPercentage.ToString() + "%";
        //logger.LogData(gameManager.GetPlayerIDForLogging(), gameManager.GetCurrentTime(), gameManager.GetDayNumber(), "XP Supplies (UI Manager)", $"{stats.CurrentXPSupplies} / {stats.NextLevelXPSupplies} -- Level {stats.SuppliesLevel}");
        //logger.LogData(gameManager.GetPlayerIDForLogging(), gameManager.GetCurrentTime(), gameManager.GetDayNumber(), "XP Armoury (UI Manager)", $"{stats.CurrentXPArmour} / {stats.NextLevelXPArmour} -- Level {stats.ArmourLevel}");
        //logger.LogData(gameManager.GetPlayerIDForLogging(), gameManager.GetCurrentTime(), gameManager.GetDayNumber(), "XP Infirmary (UI Manager)", $"{stats.CurrentXPMedicine} / {stats.NextLevelXPMedicine}");
        //logger.LogData(gameManager.GetPlayerIDForLogging(), gameManager.GetCurrentTime(), gameManager.GetDayNumber(), "Percentages (UI Manager)", $"Morale: {moraleText + moralPercentage.ToString() + "%"}");
        //logger.LogData(gameManager.GetPlayerIDForLogging(), gameManager.GetCurrentTime(), gameManager.GetDayNumber(), "Percentages (UI Manager)", $"Weapons: {weaponsText + weaponsPercentage.ToString() + "%"}");
        //logger.LogData(gameManager.GetPlayerIDForLogging(), gameManager.GetCurrentTime(), gameManager.GetDayNumber(), "Percentages (UI Manager)", $"Structure: {structureText + structurePercentage.ToString() + "%"}");
        //logger.LogData(gameManager.GetPlayerIDForLogging(), gameManager.GetCurrentTime(), gameManager.GetDayNumber(), "Percentages (UI Manager)", $"Cat: {catText + catPercentage.ToString() + "%"}");
        //GoogleSheetLogger.I.Log(gameManager.GetDayNumber(), "XP Supplies (UI Manager)", $"{stats.CurrentXPSupplies} / {stats.NextLevelXPSupplies} -- Level {stats.SuppliesLevel}");
        //GoogleSheetLogger.I.Log(gameManager.GetDayNumber(), "XP Armoury (UI Manager)", $"{stats.CurrentXPArmour} / {stats.NextLevelXPArmour} -- Level {stats.ArmourLevel}");
        //GoogleSheetLogger.I.Log(gameManager.GetDayNumber(), "XP Infirmary (UI Manager)", $"{stats.CurrentXPMedicine} / {stats.NextLevelXPMedicine}");
        //GoogleSheetLogger.I.Log(gameManager.GetDayNumber(), "Percentages (UI Manager)", $"Morale: {moraleText + moralPercentage.ToString() + "%"}");
        //GoogleSheetLogger.I.Log(gameManager.GetDayNumber(), "Percentages (UI Manager)", $"Weapons: {weaponsText + weaponsPercentage.ToString() + "%"}");
        //GoogleSheetLogger.I.Log(gameManager.GetDayNumber(), "Percentages (UI Manager)", $"Structure: {structureText + structurePercentage.ToString() + "%"}");
        //GoogleSheetLogger.I.Log(gameManager.GetDayNumber(), "Percentages (UI Manager)", $"Cat: {catText + catPercentage.ToString() + "%"}");

        GoogleSheetLogger.I.Log("Percentages (UI Manager)", $"Morale: {moraleText + moralPercentage.ToString() + "%"}");
        GoogleSheetLogger.I.Log("Percentages (UI Manager)", $"Weapons: {weaponsText + weaponsPercentage.ToString() + "%"}");
        GoogleSheetLogger.I.Log("Percentages (UI Manager)", $"Structure: {structureText + structurePercentage.ToString() + "%"}");
        GoogleSheetLogger.I.Log("Percentages (UI Manager)", $"Cat: {catText + catPercentage.ToString() + "%"}");
    }


    public void ShowLevelUp(string whichType)
    {

        string chosenText = "";

        if (whichType == "Supplies")
        {
            chosenText = suppliesTexts[Random.Range(0, suppliesTexts.Length)];
            //logger.LogData(gameManager.GetPlayerIDForLogging(), gameManager.GetCurrentTime(), gameManager.GetDayNumber(), "Level Up (UI Manager)", $"Leveled up supplies");
            //GoogleSheetLogger.I.Log(gameManager.GetDayNumber(), "Level Up (UI Manager)", $"Leveled up supplies");
        }
        else if (whichType == "Armoury")
        {
            //logger.LogData(gameManager.GetPlayerIDForLogging(), gameManager.GetCurrentTime(), gameManager.GetDayNumber(), "Level Up (UI Manager)", $"Leveled up Armoury");
            //GoogleSheetLogger.I.Log(gameManager.GetDayNumber(), "Level Up (UI Manager)", $"Leveled up Armoury");
            //chosenText = armouryTexts[Random.Range(0, armouryTexts.Length)];
        }
        else if (whichType == "Medicine")
        {
            //logger.LogData(gameManager.GetPlayerIDForLogging(), gameManager.GetCurrentTime(), gameManager.GetDayNumber(), "Level Up (UI Manager)", $"Leveled up Medicine (infirmary)");
            //GoogleSheetLogger.I.Log(gameManager.GetDayNumber(), "Level Up (UI Manager)", $"Leveled up Medicine (infirmary)");
            chosenText = medicineTexts[Random.Range(0, medicineTexts.Length)];
        }

        LevelUpTMP.text = chosenText;
        LevelUp.SetActive(true);
    }

    public void HideLevelUp()
    {
        LevelUp.SetActive(false);
    }

    public void ShowLocationInUI()
    {
        currentLocation.text = $"Location: {gameManager.GetCurrentLocation()}";
    }

    public void ChangeAIVoiceOverText(bool state)
    {
        if (state)
        {
            AIVoiceActingTMP.text = "AI Voice Acting - ON";
        }
        else
        {
            AIVoiceActingTMP.text = "AI Voice Acting - OFF";
        }
    }
}
