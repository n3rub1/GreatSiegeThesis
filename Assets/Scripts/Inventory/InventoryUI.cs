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

    [Header("Description Panel")]
    [SerializeField] private GameObject descriptionPanel;
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemDescription;

    [Header("Inventory Panel")]
    [SerializeField] private GameObject inventoryPanel;

    private List<InventorySlot> slotList = new List<InventorySlot>();

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
        }
    }

    public void ShowItemDescription(int index)
    {
        if (Inventory.Instance.InventoryItems[index] == null) return;
        descriptionPanel.SetActive(true);

        itemIcon.sprite = Inventory.Instance.InventoryItems[index].Icon;
        itemName.text = Inventory.Instance.InventoryItems[index].Name;
        itemDescription.text = Inventory.Instance.InventoryItems[index].Description;

    }

    public void DrawItem(InventoryItem item, int index)
    {
        InventorySlot slot = slotList[index];
        slot.ShowSlotInformation(true);
        slot.UpdateSlot(item);
    }

    public void CloseInventoryPanel()
    {
        inventoryPanel.SetActive(false);
    }

    public void OpenInventoryPanel()
    {
        inventoryPanel.SetActive(true);
    }

}
