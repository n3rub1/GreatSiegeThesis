using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private Image itemIcon;
    [SerializeField] private Image quantityImage;
    [SerializeField] private TextMeshProUGUI itemQuantityTMP;

    public int index { get; set; }

    public void UpdateSlot(InventoryItem item)
    {
        itemIcon.sprite = item.Icon;
        itemQuantityTMP.text = item.Quantity.ToString();
    }

    public void ShowSlotInformation(bool isThereAnItem)
    {
        itemIcon.gameObject.SetActive(isThereAnItem);
        quantityImage.gameObject.SetActive(isThereAnItem);
    }
}
