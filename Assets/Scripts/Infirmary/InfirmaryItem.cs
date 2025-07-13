using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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

    [Header("Crafting Requirements")]
    public string requiredResource;
    public int requiredAmount;

}
