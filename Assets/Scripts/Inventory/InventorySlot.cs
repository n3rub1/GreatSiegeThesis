using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class InventorySlot : MonoBehaviour
{

    public static event Action<int> OnSlotSelectedEvent;

    [Header("Config")]
    [SerializeField] private Image itemIcon;
    [SerializeField] private Image quantityImage;
    [SerializeField] private TextMeshProUGUI itemQuantityTMP;
    [SerializeField] private Button button;

    public int index { get; set; }

    public void ClickSlot()
    {
        OnSlotSelectedEvent?.Invoke(index);
        Debug.Log("Selected: " + index);
    }

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

    public void Init(System.Action<int> onClickCallback)
    {
        button.onClick.AddListener(() => onClickCallback(index));
    }
}
