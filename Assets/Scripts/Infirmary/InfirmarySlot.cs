using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class InfirmarySlot : MonoBehaviour
{
    public static event Action<int> OnSlotSelectedEvent;
    public static InfirmarySlot CurrentlySelectedSlot { get; private set; }

    [SerializeField] private Button button;

    public int index { get; set; }
    public InfirmaryItem AssignedItem { get; private set; }

    public void ClickSlot(InfirmaryItem infirmaryItem)
    {
        AssignedItem = infirmaryItem;
        OnSlotSelectedEvent?.Invoke(infirmaryItem.slotNumber);
        Debug.Log("Selected: " + infirmaryItem.slotNumber);
        InfirmaryUI.Instance.ShowItemDetails(infirmaryItem.slotNumber);
        index = infirmaryItem.slotNumber;

        CurrentlySelectedSlot = this;
    }

}
