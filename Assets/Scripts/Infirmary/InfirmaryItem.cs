using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(menuName = "Infirmary/SaveOrDie")]
public class InfirmaryItem : ScriptableObject
{
    [Header("Config")]
    public string ID;
    public string Name;
    public Sprite Icon;
    [TextArea] public string Description;

    [Header("Info")]
    public int slotNumber;
    public bool isHealed;

    [Header("Image")]
    public Sprite toolUsed;

    [Header("Crafting Requirements")]
    public string requiredResource;
    public int requiredAmount;

}
