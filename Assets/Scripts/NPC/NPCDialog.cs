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

    [TextArea] public string[] Day6Dialog;
    [TextArea] public string[] Day7FirstTimeDialog;
    [TextArea] public string[] Day7MetBeforeDialog;
    [TextArea] public string[] Day8FirstTimeDialog;
    [TextArea] public string[] Day8MetBeforeDialog;
    [TextArea] public string[] Day9FirstTimeDialog;
    [TextArea] public string[] Day9MetBeforeDialog;
    [TextArea] public string[] Day10FirstTimeDialog;
    [TextArea] public string[] Day10MetBeforeDialog;

    [Header("Audio")]
    public AudioClip Day1Audio;
    public AudioClip Day2FirstTimeAudio;
    public AudioClip Day2MetBeforeAudio;
    public AudioClip Day3FirstTimeAudio;
    public AudioClip Day3MetBeforeAudio;
    public AudioClip Day4FirstTimeAudio;
    public AudioClip Day4MetBeforeAudio;
    public AudioClip Day5FirstTimeAudio;
    public AudioClip Day5MetBeforeAudio;

    public AudioClip Day6Audio;
    public AudioClip Day7FirstTimeAudio;
    public AudioClip Day7MetBeforeAudio;
    public AudioClip Day8FirstTimeAudio;
    public AudioClip Day8MetBeforeAudio;
    public AudioClip Day9FirstTimeAudio;
    public AudioClip Day9MetBeforeAudio;
    public AudioClip Day10FirstTimeAudio;
    public AudioClip Day10MetBeforeAudio;

    [Header("Days")]
    public int dayNumber;

    [Header("Player Met NPC")]
    public bool metOnday1;
    public bool metOnday2;
    public bool metOnday3;
    public bool metOnday4;
    public bool metOnday5;

    public bool metOnday6;
    public bool metOnday7;
    public bool metOnday8;
    public bool metOnday9;
    public bool metOnday10;

    public void ResetData()
    {
        dayNumber = 1;
        metOnday1 = false;
        metOnday2 = false;
        metOnday3 = false;
        metOnday4 = false;
        metOnday5 = false;
        metOnday6 = false;
        metOnday7 = false;
        metOnday8 = false;
        metOnday9 = false;
        metOnday10 = false;
    }
}
