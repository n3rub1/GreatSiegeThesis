using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class ArmourySlot : MonoBehaviour
{
    public static event Action<int> OnSlotSelectedEvent;
    public static ArmourySlot CurrentlySelectedSlot { get; private set; }

    [SerializeField] private Button button;

    public int index { get; set; }
    public ArmouryItem AssignedItem { get; private set; }

    public void ClickSlot(ArmouryItem armouryItem)
    {
        AssignedItem = armouryItem;
        OnSlotSelectedEvent?.Invoke(armouryItem.slotNumber);
        Debug.Log("Selected: " + armouryItem.slotNumber);
        ArmouryUI.Instance.ShowItemDetails(armouryItem.slotNumber);
        index = armouryItem.slotNumber;

        CurrentlySelectedSlot = this;
    }

}
