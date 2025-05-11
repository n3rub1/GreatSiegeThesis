using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum InteractionType
{
    Armourer, Infirmary, Supplies
}

[CreateAssetMenu(menuName = "NPC Dialog")]
public class NPCDialog : ScriptableObject
{
    [Header("Info")]
    public string Name;
    public Sprite Icon;
    public bool hasSeenYou = false;

    [Header("Interaction")]
    public bool HasInteraction;
    public InteractionType interactionType;

    [Header("Dialog")]
    public string Greeting;
    [TextArea] public string[] Dialog;
}
