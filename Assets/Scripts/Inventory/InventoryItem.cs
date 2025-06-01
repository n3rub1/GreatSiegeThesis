using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType { Bandages, Iron, Wood, Food }

[CreateAssetMenu(menuName = "Items/Item")]
public class InventoryItem : ScriptableObject
{
    [Header("Config")]
    public string ID;
    public string Name;
    public Sprite Icon;
    [TextArea] public string Description;

    [Header("Info")]
    public ItemType ItemType;
    public bool IsConsumbale;
    public bool IsStackable;
    public int MaxStack;

    public int Quantity;

    public InventoryItem CopyItem()
    {
        InventoryItem instance = Instantiate(this);
        return instance;
    }

    public virtual bool UseItem()
    {
        return true;
    }

    public virtual void RemoveItem()
    {

    }

}
