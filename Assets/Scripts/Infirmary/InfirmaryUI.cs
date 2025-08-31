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

    [SerializeField] private List<InfirmaryItem> day1Injured;
    [SerializeField] private List<InfirmaryItem> day2Injured;
    [SerializeField] private List<InfirmaryItem> day3Injured;
    [SerializeField] private List<InfirmaryItem> day4Injured;
    [SerializeField] private List<InfirmaryItem> day5Injured;



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
        SetupInfirmarySlotsForDay(gameManager.getDayNumber());
    }

    private void Start()
    {
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
                slotGO.GetComponent<Button>().interactable = true;
            }
            else
            {
                Debug.LogWarning($"Slot number {injured.slotNumber} is out of range for infirmary slots.");
            }

            //if (injured.slotNumber < infirmarySlotsImages.Count)
            //{
            //    Image slotImage = infirmarySlotsImages[injured.slotNumber].GetComponent<Image>();
            //    slotImage.sprite = injured.Icon;
            //}
            //else
            //{
            //    Debug.LogWarning($"Slot number {injured.slotNumber} is out of range for infirmary slots.");
            //}
        }


        //for(int i=0; i<infirmaryInjured.Count; i++)
        //{

        //    infirmarySlotsImages[i].GetComponent<Image>().sprite = infirmaryInjured[i].Icon;

        //}


        //for (int i = 0; i < infirmarySlots.Count; i++)
        //{
        //    //InfirmarySlot slot = infirmaryInjuredPanel.transform.GetChild(i).GetComponent<InfirmarySlot>();
        //    //Debug.Log(slot);
        //    //slot.ClickSlot(currentDayItems[i]);
        //    Debug.Log(infirmarySlots[i]);
        //    infirmarySlots[i].ClickSlot(currentDayItems[i]);
        //}

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
        healingDescription.text = $"Healed {InfirmarySlot.CurrentlySelectedSlot.AssignedItem.Name} using {InfirmarySlot.CurrentlySelectedSlot.AssignedItem.requiredAmount} {InfirmarySlot.CurrentlySelectedSlot.AssignedItem.requiredResource}";
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
                playerXP.AddXPMedicine(2 + levels[infirmaryArrayValue]);
                break;
            case 1:
                playerXP.AddXPMedicine(5 + levels[infirmaryArrayValue]);
                break;
            case 2:
                playerXP.AddXPMedicine(10 + levels[infirmaryArrayValue]);
                break;
        }

        InfirmaryItem healedItem = InfirmarySlot.CurrentlySelectedSlot.AssignedItem;
        if (healedItem != null)
        {
            infirmaryInjured.Remove(healedItem);

            // Clear the UI slot image
            infirmarySlotsImages[healedItem.slotNumber].GetComponent<Image>().sprite = null;

            // Optionally disable the button so it can’t be clicked again
            infirmarySlotsImages[healedItem.slotNumber].GetComponent<Button>().interactable = false;

            // Hide description if it was showing
            descriptionPanel.SetActive(false);
        }

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
        infirmaryInjuredPanel.SetActive(false);
    }

    public void OpenInfirmaryPanel()
    {
        if (infirmaryInteraction == null) return;
        healingDescription.text = "Select Soldier to Heal";
        SetResourcesInformation();

        int currentDay = gameManager.getDayNumber();
        SetupInfirmarySlotsForDay(currentDay);

        infirmaryInjuredPanel.SetActive(true);
    }

}
