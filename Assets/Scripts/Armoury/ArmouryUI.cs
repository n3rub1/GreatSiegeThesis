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

    [Header("Description Panel")]
    [SerializeField] private GameObject descriptionPanel;
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemDescription;
    [SerializeField] private TextMeshProUGUI craftingDescription;
    [SerializeField] private TextMeshProUGUI resourcesInformation;

    [Header("Inventory Panel")]
    [SerializeField] private GameObject armouryCraftingPanel;

    [Header("Data")]
    [SerializeField] private List<ArmouryItem> armouryItems;

    public ArmouryInteraction armouryInteraction { get; set; }

    private PlayerActions actions;
    private bool isPlayerInRangeOfLoot = false;
    private int armouryArrayValue = 2;

    protected override void Awake()
    {
        base.Awake();
        actions = new PlayerActions();
    }

    private void Start()
    {
        actions.Armoury.AnvilInteraction.performed += ctx => OpenArmouryPanel();
    }

    public void ShowItemDetails(int slotNumber)
    {
        ArmouryItem item = armouryItems[slotNumber];

        if (item != null)
        {
            itemIcon.sprite = item.Icon;
            itemName.text = item.Name;
            itemDescription.text = item.Description;

            descriptionPanel.SetActive(true);
        }
        else
        {
            Debug.LogWarning("No item found for slot number: " + slotNumber);
            descriptionPanel.SetActive(false);
        }
    }

    public void Craft()
    {
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
            logger.LogData(gameManager.GetPlayerIDForLogging(), gameManager.GetCurrentTime(), gameManager.GetDayNumber(), "Tried Armoury (Armoury UI)", $"Player did not have enough iron to craft item: {ArmourySlot.CurrentlySelectedSlot.AssignedItem.ID}");
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
        craftingDescription.text = $"Crafted a {ArmourySlot.CurrentlySelectedSlot.AssignedItem.ID} using {ArmourySlot.CurrentlySelectedSlot.AssignedItem.requiredAmount} {ArmourySlot.CurrentlySelectedSlot.AssignedItem.requiredResource}";
        logger.LogData(gameManager.GetPlayerIDForLogging(), gameManager.GetCurrentTime(), gameManager.GetDayNumber(), "Crafted Weapon (Armoury UI)", $"Player crafted an item: {ArmourySlot.CurrentlySelectedSlot.AssignedItem.ID}");
        GiveArmouryXP(ArmourySlot.CurrentlySelectedSlot.AssignedItem.slotNumber, levels);
    }

    private void SetResourcesInformation()
    {
        logger.LogData(gameManager.GetPlayerIDForLogging(), gameManager.GetCurrentTime(), gameManager.GetDayNumber(), "Iron Inventory (Armoury UI)", $"Total Number of iron in Inventory: {Inventory.Instance.GetItemQuantity("Iron")}");
        int ironLeftInInventory = Inventory.Instance.GetItemQuantity("Iron");
        resourcesInformation.text = $"Total Iron Scrapes in Inventory: {ironLeftInInventory}";
    }

    private void GiveArmouryXP(int index, int[] levels)
    {

        switch (index)
        {
            case 0:
                logger.LogData(gameManager.GetPlayerIDForLogging(), gameManager.GetCurrentTime(), gameManager.GetDayNumber(), "Crafted item in Armoury (Armoury UI)", $"Crafted a simple item and got {2 + levels[armouryArrayValue]}XP");
                playerXP.AddXPArmour(2 + levels[armouryArrayValue]);
                armourPercentageToIncrease = armourPercentageToIncrease + 5;
                break;
            case 1:
                logger.LogData(gameManager.GetPlayerIDForLogging(), gameManager.GetCurrentTime(), gameManager.GetDayNumber(), "Crafted item in Armoury (Armoury UI)", $"Crafted a simple item and got {5 + levels[armouryArrayValue]}XP");
                playerXP.AddXPArmour(5 + levels[armouryArrayValue]);
                armourPercentageToIncrease = armourPercentageToIncrease + 10;
                break;
            case 2:
                logger.LogData(gameManager.GetPlayerIDForLogging(), gameManager.GetCurrentTime(), gameManager.GetDayNumber(), "Crafted item in Armoury (Armoury UI)", $"Crafted a simple item and got {10 + levels[armouryArrayValue]}XP");
                playerXP.AddXPArmour(10 + levels[armouryArrayValue]);
                armourPercentageToIncrease = armourPercentageToIncrease + 15;
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
        armouryCraftingPanel.SetActive(false);
        logger.LogData(gameManager.GetPlayerIDForLogging(), gameManager.GetCurrentTime(), gameManager.GetDayNumber(), "Anvil Closed (Armoury UI)", "Player closed the armoury panel");
    }

    public void OpenArmouryPanel()
    {
        if (armouryInteraction == null) return;
        logger.LogData(gameManager.GetPlayerIDForLogging(), gameManager.GetCurrentTime(), gameManager.GetDayNumber(), "Anvil Opened (Armoury UI)", "Player opened the armoury panel");
        craftingDescription.text = "Select Item to Craft";
        SetResourcesInformation();
        armouryCraftingPanel.SetActive(true);
    }

}
