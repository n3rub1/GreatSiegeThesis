using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : Singleton<InventoryUI>
{

    [Header("Config")]
    [SerializeField] private InventorySlot slotPrefab;
    [SerializeField] private Transform container;
    [SerializeField] private AudioSource menuClickAudioSource;

    [Header("Description Panel")]
    [SerializeField] private GameObject descriptionPanel;
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemDescription;

    [Header("Inventory Panel")]
    [SerializeField] private GameObject inventoryPanel;

    public InventorySlot CurrentSlot { get; set; }

    private List<InventorySlot> slotList = new List<InventorySlot>();
    private bool isAdditionalDetailsOpen = false;
    private int currentDetailsOpened;

    private void Start()
    {
        InitInventory();
    }

    private void InitInventory()
    {
        for (int i = 0; i < Inventory.Instance.InventorySize; i++)
        {
            InventorySlot slot = Instantiate(slotPrefab, container);
            slot.index = i;
            slot.Init(ShowItemDescription);
            slotList.Add(slot);
            DrawItem(null, i);
        }
    }

    public void ShowItemDescription(int index)
    {
        if (Inventory.Instance.InventoryItems[index] == null) return;

        menuClickAudioSource.Play();

        if(isAdditionalDetailsOpen && currentDetailsOpened == index)
        {
            descriptionPanel.SetActive(false);
            isAdditionalDetailsOpen = false;
            currentDetailsOpened = 10;
        }
        else
        {
            descriptionPanel.SetActive(true);

            itemIcon.sprite = Inventory.Instance.InventoryItems[index].Icon;
            itemName.text = Inventory.Instance.InventoryItems[index].Name;
            itemDescription.text = Inventory.Instance.InventoryItems[index].Description;

            isAdditionalDetailsOpen = true;
            currentDetailsOpened = index;
        }

    }

    public void DrawItem(InventoryItem item, int index)
    {
        InventorySlot slot = slotList[index];
        if(item == null)
        {
            slot.ShowSlotInformation(false);
            return;
        }
        slot.ShowSlotInformation(true);
        slot.UpdateSlot(item);
    }

    public void CloseInventoryPanel()
    {
        menuClickAudioSource.Play();

        descriptionPanel.SetActive(false);
        isAdditionalDetailsOpen = false;
        currentDetailsOpened = 10;
        inventoryPanel.SetActive(false);
    }

    public void OpenInventoryPanel()
    {
        menuClickAudioSource.Play();

        inventoryPanel.SetActive(true);
    }

    private void SlotSelectedCallback(int slotIndex)
    {
        CurrentSlot = slotList[slotIndex];
    }

    private void OnEnable()
    {
        InventorySlot.OnSlotSelectedEvent += SlotSelectedCallback;
    }

    private void OnDisable()
    {
        InventorySlot.OnSlotSelectedEvent -= SlotSelectedCallback;
    }
}
