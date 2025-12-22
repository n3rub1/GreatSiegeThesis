using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ArmourCraftingType{ Simple, Normal, Complex }

[CreateAssetMenu(menuName = "Armoury/Crafting")]
public class ArmouryItem : ScriptableObject
{

    [Header("Config")]
    public string ID;
    public string Name;
    public Sprite Icon;
    [TextArea] public string Description;

    [Header("Info")]
    public ArmourCraftingType CraftingType;
    public int slotNumber;

    [Header("Crafting Requirements")]
    public string requiredResource;
    public int requiredAmount;

    [Header("Image")]
    public Sprite weaponImage;
}
