using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InfirmaryUI : Singleton<InfirmaryUI>
{

    [Header("Config")]
    [SerializeField] private Inventory inventory;
    [SerializeField] private PlayerXP playerXP;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private int moralePercentageToIncrease = 0;
    [SerializeField] private GoogleSheetLogger logger;

    [Header("Description Panel")]
    [SerializeField] private GameObject descriptionPanel;
    [SerializeField] private Image injuredIcon;
    [SerializeField] private TextMeshProUGUI injuredName;
    [SerializeField] private TextMeshProUGUI injuredDescription;
    [SerializeField] private TextMeshProUGUI healingDescription;
    [SerializeField] private TextMeshProUGUI resourcesInformation;

    [Header("Inventory Panel")]
    [SerializeField] private GameObject infirmaryInjuredPanel;

    [Header("Data")]
    [SerializeField] private List<GameObject> infirmarySlotsImages;
    [SerializeField] private List<InfirmaryItem> allInfirmaryItems;
    [SerializeField] private Image itemImage;
    [SerializeField] private int simpleXP = 0;
    [SerializeField] private int mediumXP = 0;
    [SerializeField] private int complexXP = 0;
    [SerializeField] private int simpleMoralPercentage = 5;
    [SerializeField] private int normalMoralPercentage = 10;
    [SerializeField] private int complexMoralPercentage = 15;

    [SerializeField] private List<InfirmaryItem> day1Injured;
    [SerializeField] private List<InfirmaryItem> day2Injured;
    [SerializeField] private List<InfirmaryItem> day3Injured;
    [SerializeField] private List<InfirmaryItem> day4Injured;
    [SerializeField] private List<InfirmaryItem> day5Injured;

    [SerializeField] private List<InfirmaryItem> day6Injured;
    [SerializeField] private List<InfirmaryItem> day7Injured;
    [SerializeField] private List<InfirmaryItem> day8Injured;
    [SerializeField] private List<InfirmaryItem> day9Injured;
    [SerializeField] private List<InfirmaryItem> day10Injured;


    public InfirmaryInteraction infirmaryInteraction { get; set; }

    private List<InfirmaryItem> infirmaryInjured;
    private PlayerActions actions;
    private string itemUsedToHeal = "Bandage";
    private bool isPlayerInRangeOfLoot = false;
    private int infirmaryArrayValue = 1;

    protected override void Awake()
    {
        base.Awake();
        actions = new PlayerActions();
        SetupInfirmarySlotsForDay(gameManager.GetDayNumber());
    }

    private void Start()
    {

        foreach(InfirmaryItem infirmaryItem in allInfirmaryItems)
        {
            infirmaryItem.isHealed = false;
        }

        actions.Infirmary.FirePotInteraction.performed += ctx => OpenInfirmaryPanel();
    }

    public void SetupInfirmarySlotsForDay(int day)
    {

        for (int i = 0; i < infirmarySlotsImages.Count; i++)
        {
            infirmarySlotsImages[i].GetComponent<Image>().sprite = null;
            infirmarySlotsImages[i].GetComponent<Button>().interactable = false;
        }


        List<InfirmaryItem> currentDayItems = null;

        switch (day)
        {
            case 1:
                currentDayItems = day1Injured;
                break;
            case 2:
                currentDayItems = day2Injured;
                break;
            case 3:
                currentDayItems = day3Injured;
                break;
            case 4:
                currentDayItems = day4Injured;
                break;
            case 5:
                currentDayItems = day5Injured;
                break;
            case 6:
                currentDayItems = day6Injured;
                break;
            case 7:
                currentDayItems = day7Injured;
                break;
            case 8:
                currentDayItems = day8Injured;
                break;
            case 9:
                currentDayItems = day9Injured;
                break;
            case 10:
                currentDayItems = day10Injured;
                break;
            default:
                Debug.LogWarning("No infirmary items set for this day.");
                return;
        }

        infirmaryInjured = new List<InfirmaryItem>(currentDayItems);

        foreach (var injured in infirmaryInjured)
        {
            if (injured.slotNumber < infirmarySlotsImages.Count)
            {
                var slotGO = infirmarySlotsImages[injured.slotNumber];
                var slotImage = slotGO.GetComponent<Image>();

                // Set the icon
                slotImage.sprite = injured.Icon;

                // Reset the color in case it was grayed out from previous day
                slotImage.color = Color.white;

                // Make button interactable
                slotGO.GetComponent<Button>().interactable = !injured.isHealed;
            }
            else
            {
                Debug.LogWarning($"Slot number {injured.slotNumber} is out of range for infirmary slots.");
            }

        }
    }


    public void ShowItemDetails(int slotNumber)
    {
        //InfirmaryItem item = infirmaryInjured[slotNumber];

        InfirmaryItem item = infirmaryInjured.Find(i => i.slotNumber == slotNumber);

        if (item != null)
        {
            injuredIcon.sprite = item.Icon;
            injuredName.text = item.Name;
            injuredDescription.text = item.Description;
            descriptionPanel.SetActive(true);
            itemImage.sprite = item.toolUsed;
        }
        else
        {
            Debug.LogWarning("No item found for slot number: " + slotNumber);
            descriptionPanel.SetActive(false);
        }
    }

    public void Heal()
    {
        int[] levels = playerXP.GetLevels();

        if (InfirmarySlot.CurrentlySelectedSlot == null)
        {
            healingDescription.text = "Select Person to Heal";
            return;
        }

        string requiredItemID = InfirmarySlot.CurrentlySelectedSlot.AssignedItem.requiredResource;
        int requiredAmount = InfirmarySlot.CurrentlySelectedSlot.AssignedItem.requiredAmount;

        if (!Inventory.Instance.HasItem(requiredItemID, requiredAmount))
        {
            //logger.LogData(gameManager.GetPlayerIDForLogging(), gameManager.GetCurrentTime(), gameManager.GetDayNumber(), "Tried Infirmary (Infirmary UI)", $"Player did not have enough bandages to heal: {InfirmarySlot.CurrentlySelectedSlot.AssignedItem.ID}");
            //GoogleSheetLogger.I.Log(gameManager.GetDayNumber(), "Tried Infirmary (Infirmary UI)", $"Player did not have enough bandages to heal: {InfirmarySlot.CurrentlySelectedSlot.AssignedItem.ID}");
            GoogleSheetLogger.I.Log("Tried Infirmary (Infirmary UI)", $"Player did not have enough bandages to heal");

            healingDescription.text = $"Not enough {InfirmarySlot.CurrentlySelectedSlot.AssignedItem.requiredResource}";
            return;
        }

        List<int> bandagesIndexes = Inventory.Instance.CheckItemStock(requiredItemID);
        int amountLeftToRemove = requiredAmount;

        foreach (int index in bandagesIndexes)
        {
            InventoryItem item = Inventory.Instance.InventoryItems[index];

            if (item.Quantity <= amountLeftToRemove)
            {
                amountLeftToRemove -= item.Quantity;
                Inventory.Instance.InventoryItems[index] = null;
                InventoryUI.Instance.DrawItem(null, index);
            }
            else
            {
                item.Quantity -= amountLeftToRemove;
                InventoryUI.Instance.DrawItem(item, index);
                amountLeftToRemove = 0;
            }

            if (amountLeftToRemove <= 0)
                break;
        }

        SetResourcesInformation();
        //healingDescription.text = $"Healed {InfirmarySlot.CurrentlySelectedSlot.AssignedItem.Name} using {InfirmarySlot.CurrentlySelectedSlot.AssignedItem.requiredAmount} {InfirmarySlot.CurrentlySelectedSlot.AssignedItem.requiredResource}";
        healingDescription.text = $"Used {InfirmarySlot.CurrentlySelectedSlot.AssignedItem.requiredAmount} {InfirmarySlot.CurrentlySelectedSlot.AssignedItem.requiredResource}";
        //logger.LogData(gameManager.GetPlayerIDForLogging(), gameManager.GetCurrentTime(), gameManager.GetDayNumber(), "Healed Person (Infirmary UI)", $"Player healed: {InfirmarySlot.CurrentlySelectedSlot.AssignedItem.ID}");
        //GoogleSheetLogger.I.Log(gameManager.GetDayNumber(), "Healed Person (Infirmary UI)", $"Player healed: {InfirmarySlot.CurrentlySelectedSlot.AssignedItem.ID}");
        GoogleSheetLogger.I.Log("Healed Person (Infirmary UI)", $"Player healed item");

        GiveInfirmaryXP(InfirmarySlot.CurrentlySelectedSlot.AssignedItem.slotNumber, levels);
    }

    private void SetResourcesInformation()
    {
        int bandagesLeftInInventory = Inventory.Instance.GetItemQuantity(itemUsedToHeal);
        resourcesInformation.text = $"Total Bandages in Inventory: {bandagesLeftInInventory}";
    }

    private void GiveInfirmaryXP(int index, int[] levels)
    {

        switch (index)
        {
            case 0:
                //logger.LogData(gameManager.GetPlayerIDForLogging(), gameManager.GetCurrentTime(), gameManager.GetDayNumber(), "Healed person in Infirmary (Infirmary UI)", $"Healed a simple wound and got {2 + levels[infirmaryArrayValue]}XP");
                //GoogleSheetLogger.I.Log(gameManager.GetDayNumber(), "Healed person in Infirmary (Infirmary UI)", $"Healed a simple wound and got {2 + levels[infirmaryArrayValue]}XP");
                GoogleSheetLogger.I.Log("Healed person in Infirmary (Infirmary UI)", $"Healed a simple wound");

                playerXP.AddXPMedicine(simpleXP + levels[infirmaryArrayValue]);
                moralePercentageToIncrease = moralePercentageToIncrease + simpleMoralPercentage;
                break;
            case 1:
                //logger.LogData(gameManager.GetPlayerIDForLogging(), gameManager.GetCurrentTime(), gameManager.GetDayNumber(), "Healed person in Infirmary (Infirmary UI)", $"Healed a medium wound and got {5 + levels[infirmaryArrayValue]}XP");
                //GoogleSheetLogger.I.Log(gameManager.GetDayNumber(), "Healed person in Infirmary (Infirmary UI)", $"Healed a medium wound and got {5 + levels[infirmaryArrayValue]}XP");
                GoogleSheetLogger.I.Log("Healed person in Infirmary (Infirmary UI)", $"Healed a medium wound");

                playerXP.AddXPMedicine(mediumXP + levels[infirmaryArrayValue]);
                moralePercentageToIncrease = moralePercentageToIncrease + normalMoralPercentage;
                break;
            case 2:
                //logger.LogData(gameManager.GetPlayerIDForLogging(), gameManager.GetCurrentTime(), gameManager.GetDayNumber(), "Healed person in Infirmary (Infirmary UI)", $"Healed a hard wound and got {10 + levels[infirmaryArrayValue]}XP");
                //GoogleSheetLogger.I.Log(gameManager.GetDayNumber(), "Healed person in Infirmary (Infirmary UI)", $"Healed a hard wound and got {10 + levels[infirmaryArrayValue]}XP");
                GoogleSheetLogger.I.Log("Healed person in Infirmary (Infirmary UI)", $"Healed a complex wound");

                playerXP.AddXPMedicine(complexXP + levels[infirmaryArrayValue]);
                moralePercentageToIncrease = moralePercentageToIncrease + complexMoralPercentage;
                break;
        }

        //InfirmaryItem healedItem = InfirmarySlot.CurrentlySelectedSlot.AssignedItem;
        //if (healedItem != null)
        //{
        //    healedItem.isHealed = true;
        //    infirmaryInjured.Remove(healedItem);

        //    infirmarySlotsImages[healedItem.slotNumber].GetComponent<Button>().interactable = false;
        //    descriptionPanel.SetActive(false);
        //}

        var selected = InfirmarySlot.CurrentlySelectedSlot?.AssignedItem;
        if (selected == null) return;

        int slotNum = selected.slotNumber;

        // Update the canonical item (the one SetupInfirmarySlotsForDay uses)
        var canonical = infirmaryInjured.Find(i => i.slotNumber == slotNum);
        if (canonical != null)
        {
            canonical.isHealed = true;
            infirmaryInjured.Remove(canonical);
        }

        // Disable the UI slot
        infirmarySlotsImages[slotNum].GetComponent<Button>().interactable = false;
        descriptionPanel.SetActive(false);

    }

    public void ResetMoralePercentageToIncrease()
    {
        moralePercentageToIncrease = 0;
    }

    public int GetMoralePercentageToIncrease()
    {
        return moralePercentageToIncrease;
    }

    public void SetPlayerInRange(bool value)
    {
        isPlayerInRangeOfLoot = value;
    }

    private void OnEnable()
    {
        actions.Enable();
    }

    private void OnDisable()
    {
        actions.Disable();
    }

    public void CloseInfirmaryPanel()
    {
       // logger.LogData(gameManager.GetPlayerIDForLogging(), gameManager.GetCurrentTime(), gameManager.GetDayNumber(), "Infirmary Closed (Infirmary UI)", "Player closed the infirmary panel");
        //GoogleSheetLogger.I.Log(gameManager.GetDayNumber(), "Infirmary Closed (Infirmary UI)", "Player closed the infirmary panel");
        GoogleSheetLogger.I.Log("Infirmary Closed (Infirmary UI)", "Player closed the infirmary panel");

        descriptionPanel.SetActive(false);
        infirmaryInjuredPanel.SetActive(false);
    }

    public void OpenInfirmaryPanel()
    {
        if (infirmaryInteraction == null) return;
        //logger.LogData(gameManager.GetPlayerIDForLogging(), gameManager.GetCurrentTime(), gameManager.GetDayNumber(), "Infirmary Opened (Infirmary UI)", "Player opened the infirmary panel");
        //GoogleSheetLogger.I.Log(gameManager.GetDayNumber(), "Infirmary Opened (Infirmary UI)", "Player opened the infirmary panel");
        GoogleSheetLogger.I.Log("Infirmary Opened (Infirmary UI)", "Player opened the infirmary panel");

        healingDescription.text = "Select Soldier to Heal";
        SetResourcesInformation();

        int currentDay = gameManager.GetDayNumber();
        SetupInfirmarySlotsForDay(currentDay);

        infirmaryInjuredPanel.SetActive(true);
    }

}
