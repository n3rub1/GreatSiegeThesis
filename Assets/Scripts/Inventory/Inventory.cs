using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : Singleton<Inventory>
{
    [Header("Config")]
    [SerializeField] private int inventorySize; // 9 max slots
    [SerializeField] private InventoryItem[] inventoryItems;

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

        if(item.IsStackable && itemIndexes.Count > 0)
        {
            foreach(int index in itemIndexes)
            {
                int maxStack = item.MaxStack;

                if(inventoryItems[index].Quantity < maxStack)
                {
                    inventoryItems[index].Quantity = inventoryItems[index].Quantity + quantity;
                    if(inventoryItems[index].Quantity > maxStack)
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

        if(remainingAmount > 0)
        {
            AddItem(item, remainingAmount);
        }

    }

    private void AddItemFreeSlot(InventoryItem item, int quantity)
    {
        for(int i = 0; i < inventorySize; i++)
        {
            if (inventoryItems[i] != null) continue;
            inventoryItems[i] = item.CopyItem();
            inventoryItems[i].Quantity = quantity;
            InventoryUI.Instance.DrawItem(inventoryItems[i], i);
            return;
        }
    }

    private List<int> CheckItemStock(string itemId)
    {
        List<int> itemIndexes = new List<int>();
        for(int i = 0; i < inventoryItems.Length; i++)
        {
            if (inventoryItems[i] == null) continue;
            if(inventoryItems[i].ID == itemId)
            {
                itemIndexes.Add(i);
            }

        }

        return itemIndexes;

    }

}
