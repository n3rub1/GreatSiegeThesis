using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum InteractionType
{
    Armourer, Infirmary, Supplies, Tutorial
}

[CreateAssetMenu(menuName = "NPC Dialog")]
public class NPCDialog : ScriptableObject
{
    [Header("Info")]
    public string Name;
    public Sprite Icon;

    [Header("Interaction")]
    public bool HasInteraction;
    public InteractionType interactionType;

    [Header("Dialog")]
    public string Greeting;
    [TextArea] public string[] Day1Dialog;
    [TextArea] public string[] Day2FirstTimeDialog;
    [TextArea] public string[] Day2MetBeforeDialog;
    [TextArea] public string[] Day3FirstTimeDialog;
    [TextArea] public string[] Day3MetBeforeDialog;
    [TextArea] public string[] Day4FirstTimeDialog;
    [TextArea] public string[] Day4MetBeforeDialog;
    [TextArea] public string[] Day5FirstTimeDialog;
    [TextArea] public string[] Day5MetBeforeDialog;

    [Header("Days")]
    public int dayNumber;

    [Header("Player Met NPC")]
    public bool metOnday1;
    public bool metOnday2;
    public bool metOnday3;
    public bool metOnday4;
    public bool metOnday5;

    public void ResetData()
    {
        dayNumber = 1;
        metOnday1 = false;
        metOnday2 = false;
        metOnday3 = false;
        metOnday4 = false;
        metOnday5 = false;
    }
}
