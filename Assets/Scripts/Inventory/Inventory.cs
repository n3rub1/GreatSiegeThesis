using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : Singleton<Inventory>
{
    [Header("Config")]
    [SerializeField] private int inventorySize; // 9 max slots
    [SerializeField] private InventoryItem[] inventoryItems;
    [SerializeField] private InventorySlot[] slotUIElements;

    public int InventorySize => inventorySize;
    public InventoryItem[] InventoryItems => inventoryItems;

    private void Start()
    {
        inventoryItems = new InventoryItem[inventorySize];
    }

    public void AddItem(InventoryItem item, int quantity)
    {
        if (item == null || quantity <= 0) return;
        List<int> itemIndexes = CheckItemStock(item.ID);

        if (item.IsStackable && itemIndexes.Count > 0)
        {
            foreach (int index in itemIndexes)
            {
                int maxStack = item.MaxStack;

                if (inventoryItems[index].Quantity < maxStack)
                {
                    inventoryItems[index].Quantity = inventoryItems[index].Quantity + quantity;
                    if (inventoryItems[index].Quantity > maxStack)
                    {
                        int diffence = inventoryItems[index].Quantity - maxStack;
                        inventoryItems[index].Quantity = maxStack;
                        AddItem(item, diffence);
                    }

                    InventoryUI.Instance.DrawItem(inventoryItems[index], index);
                    return;

                }
            }
        }

        int quantityToAdd = quantity > item.MaxStack ? item.MaxStack : quantity;

        AddItemFreeSlot(item, quantityToAdd);
        int remainingAmount = quantity - quantityToAdd;

        if (remainingAmount > 0)
        {
            AddItem(item, remainingAmount);
        }

    }

    private void AddItemFreeSlot(InventoryItem item, int quantity)
    {
        for (int i = 0; i < inventorySize; i++)
        {
            if (inventoryItems[i] != null) continue;
            inventoryItems[i] = item.CopyItem();
            inventoryItems[i].Quantity = quantity;
            InventoryUI.Instance.DrawItem(inventoryItems[i], i);
            return;
        }
    }

    public List<int> CheckItemStock(string itemId)
    {
        List<int> itemIndexes = new List<int>();
        for (int i = 0; i < inventoryItems.Length; i++)
        {
            if (inventoryItems[i] == null) continue;
            if (inventoryItems[i].ID == itemId)
            {
                itemIndexes.Add(i);
            }

        }

        return itemIndexes;

    }

    public void GetAllInventoryInformation(string itemID)
    {
        List<int> indexes = CheckItemStock(itemID);

        foreach (int index in indexes)
        {
            InventoryItem item = inventoryItems[index];
            if (item != null)
            {
                Debug.Log($"Item at index {index}: Quantity = {item.Quantity}");
            }
        }
    }

    public int GetItemQuantity(string itemID)
    {
        int totalQuantity = 0;

        foreach (InventoryItem item in inventoryItems)
        {
            if (item != null && item.ID == itemID)
            {
                totalQuantity += item.Quantity;
            }
        }

        return totalQuantity;
    }

    public bool HasItem(string itemID, int amountToCheckInInventory)
    {
        int total = 0;
        foreach (InventoryItem item in inventoryItems)
        {
            if (item != null && item.ID == itemID)
            {
                total = total + item.Quantity;
                if (total >= amountToCheckInInventory)
                {
                    return true;
                }
            }
        }

        return false;
    }


   //public void DecreaseItemStack(int index)
   // {
   //     inventoryItems[index].Quantity--;

   //     if(inventoryItems[index].Quantity <= 0)
   //     {
   //         inventoryItems[index] = null;
   //         InventoryUI.Instance.DrawItem(null, index);
   //     }
   //     else
   //     {
   //         InventoryUI.Instance.DrawItem(inventoryItems[index], index);
   //     }
   // }


}
