using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ArmouryUI : Singleton<ArmouryUI>
{

    [Header("Config")]
    [SerializeField] private Inventory inventory;
    [SerializeField] private PlayerXP playerXP;
    [SerializeField] private int armourPercentageToIncrease = 0;
    [SerializeField] private GoogleSheetLogger logger;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private AudioSource buttonAudioSource;

    [Header("Description Panel")]
    [SerializeField] private GameObject descriptionPanel;
    [SerializeField] private Image itemIcon;
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemDescription;
    [SerializeField] private TextMeshProUGUI craftingDescription;
    [SerializeField] private TextMeshProUGUI resourcesInformation;

    [Header("Inventory Panel")]
    [SerializeField] private GameObject armouryCraftingPanel;

    [Header("Data")]
    [SerializeField] private List<GameObject> armourySlotsImages;
    [SerializeField] private int simpleXP = 0;
    [SerializeField] private int mediumXP = 0;
    [SerializeField] private int complexXP = 0;
    [SerializeField] private int simpleMoralPercentage = 5;
    [SerializeField] private int normalMoralPercentage = 10;
    [SerializeField] private int complexMoralPercentage = 15;



    //ALL NEW
    [SerializeField] private List<ArmouryItem> day1armour;
    [SerializeField] private List<ArmouryItem> day2armour;
    [SerializeField] private List<ArmouryItem> day3armour;
    [SerializeField] private List<ArmouryItem> day4armour;
    [SerializeField] private List<ArmouryItem> day5armour;

    [SerializeField] private List<ArmouryItem> day6armour;
    [SerializeField] private List<ArmouryItem> day7armour;
    [SerializeField] private List<ArmouryItem> day8armour;
    [SerializeField] private List<ArmouryItem> day9armour;
    [SerializeField] private List<ArmouryItem> day10armour;



    //[Header("Data")]
    // [SerializeField] private List<ArmouryItem> armouryItems;  THIS COMMENTED

    public ArmouryInteraction armouryInteraction { get; set; }

    private List<ArmouryItem> armouryWeaponsAndArmour;  //NEW 

    private PlayerActions actions;
    private bool isPlayerInRangeOfLoot = false;
    private bool isOpen = false;
    //private int armouryArrayValue = 2;

    protected override void Awake()
    {
        base.Awake();
        actions = new PlayerActions();
        SetupArmourySlotsForDay(gameManager.GetDayNumber());
    }

    private void Start()
    {
        isOpen = false;
        actions.Armoury.AnvilInteraction.performed += ctx => OpenArmouryPanel();
    }


    //NEW LIST
    public void SetupArmourySlotsForDay(int day)
    {

        //for (int i = 0; i < armourySlotsImages.Count; i++)
        //{
        //    armourySlotsImages[i].GetComponent<Image>().sprite = null;
        //    armourySlotsImages[i].GetComponent<Button>().interactable = false;
        //}


        List<ArmouryItem> currentDayItems = null;

        switch (day)
        {
            case 1:
                currentDayItems = day1armour;
                break;
            case 2:
                currentDayItems = day2armour;
                break;
            case 3:
                currentDayItems = day3armour;
                break;
            case 4:
                currentDayItems = day4armour;
                break;
            case 5:
                currentDayItems = day5armour;
                break;
            case 6:
                currentDayItems = day6armour;
                break;
            case 7:
                currentDayItems = day7armour;
                break;
            case 8:
                currentDayItems = day8armour;
                break;
            case 9:
                currentDayItems = day9armour;
                break;
            case 10:
                currentDayItems = day10armour;
                break;
            default:
                Debug.LogWarning("No armoury items set for this day.");
                return;
        }

        armouryWeaponsAndArmour = new List<ArmouryItem>(currentDayItems);

        foreach (var weapon in armouryWeaponsAndArmour)
        {
            if (weapon.slotNumber < armourySlotsImages.Count)
            {
                var slotGO = armourySlotsImages[weapon.slotNumber];
                var slotImage = slotGO.GetComponent<Image>();

                // Set the icon
                slotImage.sprite = weapon.Icon;

                //// Reset the color in case it was grayed out from previous day
                //slotImage.color = Color.white;

                //// Make button interactable
                //slotGO.GetComponent<Button>().interactable = !weapon.isHealed;
            }
            else
            {
                Debug.LogWarning($"Slot number {weapon.slotNumber} is out of range for infirmary slots.");
            }

        }
    }

    public void ShowItemDetails(int slotNumber)
    {

        //ArmouryItem item = armouryItems[slotNumber];
        ArmouryItem item = armouryWeaponsAndArmour.Find(i => i.slotNumber == slotNumber);

        if (item != null)
        {
            buttonAudioSource.Play();

            itemIcon.sprite = item.Icon;
            itemName.text = item.Name;
            itemDescription.text = item.Description;
            itemImage.sprite = item.weaponImage;

            descriptionPanel.SetActive(true);
        }
        else
        {
            Debug.LogWarning("No item found for slot number: " + slotNumber);
            descriptionPanel.SetActive(false);
        }
    }

    public void HideItemDetails()
    {
        if (isOpen)
        {
            buttonAudioSource.Play();
        }

        descriptionPanel.SetActive(false);
    }

    public void Craft()
    {
        buttonAudioSource.Play();

        int[] levels = playerXP.GetLevels();

        if (ArmourySlot.CurrentlySelectedSlot == null)
        {
            craftingDescription.text = "Select Item to Craft";
            return;
        }

        string requiredItemID = ArmourySlot.CurrentlySelectedSlot.AssignedItem.requiredResource;
        int requiredAmount = ArmourySlot.CurrentlySelectedSlot.AssignedItem.requiredAmount;

        if (!Inventory.Instance.HasItem(requiredItemID, requiredAmount))
        {
            craftingDescription.text = $"Not enough {ArmourySlot.CurrentlySelectedSlot.AssignedItem.requiredResource}";
            //logger.LogData(gameManager.GetPlayerIDForLogging(), gameManager.GetCurrentTime(), gameManager.GetDayNumber(), "Tried Armoury (Armoury UI)", $"Player did not have enough iron to craft item: {ArmourySlot.CurrentlySelectedSlot.AssignedItem.ID}");
            //GoogleSheetLogger.I.Log(gameManager.GetDayNumber(), "Tried Armoury (Armoury UI)", $"Player did not have enough iron to craft item: {ArmourySlot.CurrentlySelectedSlot.AssignedItem.ID}");
            GoogleSheetLogger.I.Log("Armoury (Armoury UI)", $"Player did not have enough iron to craft item");

            return;
        }

        List<int> ironIndexes = Inventory.Instance.CheckItemStock(requiredItemID);
        int amountLeftToRemove = requiredAmount;

        foreach (int index in ironIndexes)
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
        //craftingDescription.text = $"Crafted a {ArmourySlot.CurrentlySelectedSlot.AssignedItem.Name} using {ArmourySlot.CurrentlySelectedSlot.AssignedItem.requiredAmount} {ArmourySlot.CurrentlySelectedSlot.AssignedItem.requiredResource}";
        craftingDescription.text = $"Used {ArmourySlot.CurrentlySelectedSlot.AssignedItem.requiredAmount} {ArmourySlot.CurrentlySelectedSlot.AssignedItem.requiredResource}";
        //logger.LogData(gameManager.GetPlayerIDForLogging(), gameManager.GetCurrentTime(), gameManager.GetDayNumber(), "Crafted Weapon (Armoury UI)", $"Player crafted an item: {ArmourySlot.CurrentlySelectedSlot.AssignedItem.ID}");
        //GoogleSheetLogger.I.Log(gameManager.GetDayNumber(), "Crafted Weapon (Armoury UI)", $"Player crafted an item: {ArmourySlot.CurrentlySelectedSlot.AssignedItem.ID}");
        GoogleSheetLogger.I.Log("Crafted Weapon (Armoury UI)", $"Player crafted an item");

        GiveArmouryXP(ArmourySlot.CurrentlySelectedSlot.AssignedItem.slotNumber, levels);
    }

    private void SetResourcesInformation()
    {
        //logger.LogData(gameManager.GetPlayerIDForLogging(), gameManager.GetCurrentTime(), gameManager.GetDayNumber(), "Iron Inventory (Armoury UI)", $"Total Number of iron in Inventory: {Inventory.Instance.GetItemQuantity("Iron")}");
        //GoogleSheetLogger.I.Log(gameManager.GetDayNumber(), "Iron Inventory (Armoury UI)", $"Total Number of iron in Inventory: {Inventory.Instance.GetItemQuantity("Iron")}");
        GoogleSheetLogger.I.Log("Iron Inventory (Armoury UI)", $"Total Number of iron in Inventory: {Inventory.Instance.GetItemQuantity("Iron")}");

        int ironLeftInInventory = Inventory.Instance.GetItemQuantity("Iron");
        resourcesInformation.text = $"Total Iron Scrapes in Inventory: {ironLeftInInventory}";
    }

    private void GiveArmouryXP(int index, int[] levels)
    {

        switch (index)
        {
            case 0:
                //logger.LogData(gameManager.GetPlayerIDForLogging(), gameManager.GetCurrentTime(), gameManager.GetDayNumber(), "Crafted item in Armoury (Armoury UI)", $"Crafted a simple item and got {2 + levels[armouryArrayValue]}XP");
                //GoogleSheetLogger.I.Log(gameManager.GetDayNumber(), "Crafted item in Armoury (Armoury UI)", $"Crafted a simple item and got {2 + levels[armouryArrayValue]}XP");
                GoogleSheetLogger.I.Log("Crafted item in Armoury (Armoury UI)", "Crafted a simple item");

                playerXP.AddXPArmour(simpleXP);
                armourPercentageToIncrease = armourPercentageToIncrease + simpleMoralPercentage;
                break;
            case 1:
                //logger.LogData(gameManager.GetPlayerIDForLogging(), gameManager.GetCurrentTime(), gameManager.GetDayNumber(), "Crafted item in Armoury (Armoury UI)", $"Crafted a simple item and got {5 + levels[armouryArrayValue]}XP");
                //GoogleSheetLogger.I.Log(gameManager.GetDayNumber(), "Crafted item in Armoury (Armoury UI)", $"Crafted a normal item and got {5 + levels[armouryArrayValue]}XP");
                GoogleSheetLogger.I.Log("Crafted item in Armoury (Armoury UI)", $"Crafted a normal item");

                playerXP.AddXPArmour(mediumXP);
                armourPercentageToIncrease = armourPercentageToIncrease + normalMoralPercentage;
                break;
            case 2:
                //logger.LogData(gameManager.GetPlayerIDForLogging(), gameManager.GetCurrentTime(), gameManager.GetDayNumber(), "Crafted item in Armoury (Armoury UI)", $"Crafted a simple item and got {10 + levels[armouryArrayValue]}XP");
                //GoogleSheetLogger.I.Log(gameManager.GetDayNumber(), "Crafted item in Armoury (Armoury UI)", $"Crafted a complex item and got {10 + levels[armouryArrayValue]}XP");
                GoogleSheetLogger.I.Log("Crafted item in Armoury (Armoury UI)", $"Crafted a complex item");

                playerXP.AddXPArmour(complexXP);
                armourPercentageToIncrease = armourPercentageToIncrease + complexMoralPercentage;
                break;
        }

    }

    public void ResetArmourPercentageToIncrease()
    {
        armourPercentageToIncrease = 0;
    }

    public int GetArmourPercentageToIncrease()
    {
        return armourPercentageToIncrease;
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

    public void CloseArmouryPanel()
    {
        if (isOpen)
        {
            buttonAudioSource.Play();
            isOpen = false;
        }


        HideItemDetails();
        armouryCraftingPanel.SetActive(false);

        //logger.LogData(gameManager.GetPlayerIDForLogging(), gameManager.GetCurrentTime(), gameManager.GetDayNumber(), "Anvil Closed (Armoury UI)", "Player closed the armoury panel");
        //GoogleSheetLogger.I.Log(gameManager.GetDayNumber(), "Anvil Closed (Armoury UI)", "Player closed the armoury panel");
        GoogleSheetLogger.I.Log("Anvil Closed (Armoury UI)", "Player closed the armoury panel");

    }

    public void OpenArmouryPanel()
    {

        if (armouryInteraction == null) return;

        buttonAudioSource.Play();
        isOpen = true;

        //logger.LogData(gameManager.GetPlayerIDForLogging(), gameManager.GetCurrentTime(), gameManager.GetDayNumber(), "Anvil Opened (Armoury UI)", "Player opened the armoury panel");
        //GoogleSheetLogger.I.Log(gameManager.GetDayNumber(), "Anvil Opened (Armoury UI)", "Player opened the armoury panel");
        GoogleSheetLogger.I.Log("Anvil Opened (Armoury UI)", "Player opened the armoury panel");

        craftingDescription.text = "Select Item to Craft";
        SetResourcesInformation();

        int currentDay = gameManager.GetDayNumber();
        SetupArmourySlotsForDay(currentDay);

        armouryCraftingPanel.SetActive(true);
    }

}
