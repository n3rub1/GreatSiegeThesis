using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogManager : Singleton<DialogManager>
{
    [Header("Config")]
    [SerializeField] private GameObject dialogPanel;
    [SerializeField] private Image npcIcon;
    [SerializeField] private TextMeshProUGUI npcNameTMP;
    [SerializeField] private TextMeshProUGUI npcDialogTMP;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GoogleSheetLogger logger;
    [SerializeField] private AudioSource voiceSource;
    [SerializeField] private DayNightCycleManager dayNightCycleManager;
    [SerializeField] private AIOverrideManager aiOverrideManager;

    public NPCInteraction npcSelected { get; set; }

    private bool dialogStarted;
    private PlayerActions actions;
    private Queue<string> dialogQueue = new Queue<string>();

    protected override void Awake()
    {
        base.Awake();
        actions = new PlayerActions();
    }

    private void Start()
    {
        //gameManager = FindObjectOfType<GameManager>();
        actions.Dialog.Interact.performed += ctx => ShowDialog();
        actions.Dialog.Continue.performed += ctx => ContinueDialog();
    }

    public void CloseDialogPanel()
    {
        dialogPanel.SetActive(false);
        dialogStarted = false;
        dialogQueue.Clear();

        dayNightCycleManager.StartStopTimer(false);
    }

    private void LoadDialogFromNPC()
    {
        int currentDay = gameManager.GetDayNumber();
        npcSelected.DialogToShow.dayNumber = currentDay;

        if (currentDay <= 5 && (npcSelected.DialogToShow.Day1Dialog.Length <= 0 || 
            npcSelected.DialogToShow.Day2MetBeforeDialog.Length <= 0|| 
            npcSelected.DialogToShow.Day2FirstTimeDialog.Length <= 0 ||
            npcSelected.DialogToShow.Day3MetBeforeDialog.Length <= 0 ||
            npcSelected.DialogToShow.Day3FirstTimeDialog.Length <= 0 ||
            npcSelected.DialogToShow.Day4MetBeforeDialog.Length <= 0 ||
            npcSelected.DialogToShow.Day4FirstTimeDialog.Length <= 0 ||
            npcSelected.DialogToShow.Day5MetBeforeDialog.Length <= 0 ||
            npcSelected.DialogToShow.Day5FirstTimeDialog.Length <= 0)) return;

        if (currentDay >= 6 && (npcSelected.DialogToShow.Day6Dialog.Length <= 0 ||
                npcSelected.DialogToShow.Day7MetBeforeDialog.Length <= 0 ||
                npcSelected.DialogToShow.Day7FirstTimeDialog.Length <= 0 ||
                npcSelected.DialogToShow.Day8MetBeforeDialog.Length <= 0 ||
                npcSelected.DialogToShow.Day8FirstTimeDialog.Length <= 0 ||
                npcSelected.DialogToShow.Day9MetBeforeDialog.Length <= 0 ||
                npcSelected.DialogToShow.Day9FirstTimeDialog.Length <= 0 ||
                npcSelected.DialogToShow.Day10MetBeforeDialog.Length <= 0 ||
                npcSelected.DialogToShow.Day10FirstTimeDialog.Length <= 0)) return;

        //       foreach(string sentence in npcSelected.DialogToShow.Day1Dialog)
        //       {
        //           dialogQueue.Enqueue(sentence);
        //       }

        switch (currentDay)
        {
            case 1:
                foreach (string sentence in npcSelected.DialogToShow.Day1Dialog)
                    dialogQueue.Enqueue(sentence);
                npcSelected.DialogToShow.metOnday1 = true;
                if(aiOverrideManager.GetAIVoiceActing()) PlayClip(npcSelected.DialogToShow.Day1Audio);
                break;

            case 2:
                if (npcSelected.DialogToShow.metOnday1)
                {
                    foreach (string sentence in npcSelected.DialogToShow.Day2MetBeforeDialog)
                        dialogQueue.Enqueue(sentence);
                    npcSelected.DialogToShow.metOnday2 = true;
                    if (aiOverrideManager.GetAIVoiceActing()) PlayClip(npcSelected.DialogToShow.Day2MetBeforeAudio);
                }
                else
                {
                    foreach (string sentence in npcSelected.DialogToShow.Day2FirstTimeDialog)
                        dialogQueue.Enqueue(sentence);
                    npcSelected.DialogToShow.metOnday2 = true;
                    if (aiOverrideManager.GetAIVoiceActing()) PlayClip(npcSelected.DialogToShow.Day2FirstTimeAudio);
                }
                break;

            case 3:
                if (npcSelected.DialogToShow.metOnday2)
                {
                    foreach (string sentence in npcSelected.DialogToShow.Day3MetBeforeDialog)
                        dialogQueue.Enqueue(sentence);
                    npcSelected.DialogToShow.metOnday3 = true;
                    if (aiOverrideManager.GetAIVoiceActing()) PlayClip(npcSelected.DialogToShow.Day3MetBeforeAudio);
                }
                else
                {
                    foreach (string sentence in npcSelected.DialogToShow.Day3FirstTimeDialog)
                        dialogQueue.Enqueue(sentence);
                    npcSelected.DialogToShow.metOnday3 = true;
                    if (aiOverrideManager.GetAIVoiceActing()) PlayClip(npcSelected.DialogToShow.Day3FirstTimeAudio);
                }
                break;

            case 4:
                if (npcSelected.DialogToShow.metOnday3)
                {
                    foreach (string sentence in npcSelected.DialogToShow.Day4MetBeforeDialog)
                        dialogQueue.Enqueue(sentence);
                    npcSelected.DialogToShow.metOnday4 = true;
                    if (aiOverrideManager.GetAIVoiceActing()) PlayClip(npcSelected.DialogToShow.Day4MetBeforeAudio);
                }
                else
                {
                    foreach (string sentence in npcSelected.DialogToShow.Day4FirstTimeDialog)
                        dialogQueue.Enqueue(sentence);
                    npcSelected.DialogToShow.metOnday4 = true;
                    if (aiOverrideManager.GetAIVoiceActing()) PlayClip(npcSelected.DialogToShow.Day4FirstTimeAudio);
                }
                break;

            case 5:
                if (npcSelected.DialogToShow.metOnday4)
                {
                    foreach (string sentence in npcSelected.DialogToShow.Day5MetBeforeDialog)
                        dialogQueue.Enqueue(sentence);
                    npcSelected.DialogToShow.metOnday5 = true;
                    if (aiOverrideManager.GetAIVoiceActing()) PlayClip(npcSelected.DialogToShow.Day5MetBeforeAudio);
                }
                else
                {
                    foreach (string sentence in npcSelected.DialogToShow.Day5FirstTimeDialog)
                        dialogQueue.Enqueue(sentence);
                    npcSelected.DialogToShow.metOnday5 = true;
                    if (aiOverrideManager.GetAIVoiceActing()) PlayClip(npcSelected.DialogToShow.Day5FirstTimeAudio);
                }
                break;

            case 6:
                foreach (string sentence in npcSelected.DialogToShow.Day6Dialog)
                    dialogQueue.Enqueue(sentence);
                npcSelected.DialogToShow.metOnday6 = true;
                if (aiOverrideManager.GetAIVoiceActing()) PlayClip(npcSelected.DialogToShow.Day6Audio);
                break;

            case 7:
                if (npcSelected.DialogToShow.metOnday6)
                {
                    foreach (string sentence in npcSelected.DialogToShow.Day7MetBeforeDialog)
                        dialogQueue.Enqueue(sentence);
                    npcSelected.DialogToShow.metOnday7 = true;
                    if (aiOverrideManager.GetAIVoiceActing()) PlayClip(npcSelected.DialogToShow.Day7MetBeforeAudio);
                }
                else
                {
                    foreach (string sentence in npcSelected.DialogToShow.Day7FirstTimeDialog)
                        dialogQueue.Enqueue(sentence);
                    npcSelected.DialogToShow.metOnday7 = true;
                    if (aiOverrideManager.GetAIVoiceActing()) PlayClip(npcSelected.DialogToShow.Day7FirstTimeAudio);
                }
                break;

            case 8:
                if (npcSelected.DialogToShow.metOnday7)
                {
                    foreach (string sentence in npcSelected.DialogToShow.Day8MetBeforeDialog)
                        dialogQueue.Enqueue(sentence);
                    npcSelected.DialogToShow.metOnday8 = true;
                    if (aiOverrideManager.GetAIVoiceActing()) PlayClip(npcSelected.DialogToShow.Day8MetBeforeAudio);
                }
                else
                {
                    foreach (string sentence in npcSelected.DialogToShow.Day8FirstTimeDialog)
                        dialogQueue.Enqueue(sentence);
                    npcSelected.DialogToShow.metOnday8 = true;
                    if (aiOverrideManager.GetAIVoiceActing()) PlayClip(npcSelected.DialogToShow.Day8FirstTimeAudio);
                }
                break;

            case 9:
                if (npcSelected.DialogToShow.metOnday8)
                {
                    foreach (string sentence in npcSelected.DialogToShow.Day9MetBeforeDialog)
                        dialogQueue.Enqueue(sentence);
                    npcSelected.DialogToShow.metOnday9 = true;
                    if (aiOverrideManager.GetAIVoiceActing()) PlayClip(npcSelected.DialogToShow.Day9MetBeforeAudio);
                }
                else
                {
                    foreach (string sentence in npcSelected.DialogToShow.Day9FirstTimeDialog)
                        dialogQueue.Enqueue(sentence);
                    npcSelected.DialogToShow.metOnday9 = true;
                    if (aiOverrideManager.GetAIVoiceActing()) PlayClip(npcSelected.DialogToShow.Day9FirstTimeAudio);
                }
                break;

            case 10:
                if (npcSelected.DialogToShow.metOnday9)
                {
                    foreach (string sentence in npcSelected.DialogToShow.Day10MetBeforeDialog)
                        dialogQueue.Enqueue(sentence);
                    npcSelected.DialogToShow.metOnday10 = true;
                    if (aiOverrideManager.GetAIVoiceActing()) PlayClip(npcSelected.DialogToShow.Day10MetBeforeAudio);
                }
                else
                {
                    foreach (string sentence in npcSelected.DialogToShow.Day10FirstTimeDialog)
                        dialogQueue.Enqueue(sentence);
                    npcSelected.DialogToShow.metOnday10 = true;
                    if (aiOverrideManager.GetAIVoiceActing()) PlayClip(npcSelected.DialogToShow.Day10FirstTimeAudio);
                }
                break;
        }
    }

    private void ShowDialog()
    {
        if (npcSelected == null) return;
        if (dialogStarted) return;

        //logger.LogData(gameManager.GetPlayerIDForLogging(), gameManager.GetCurrentTime(), gameManager.GetDayNumber(), "Dialog Start (Dialog Manager)", $"Started dialog with {npcSelected.DialogToShow.Name}");
        //logger.LogData(gameManager.GetPlayerIDForLogging(), gameManager.GetCurrentTime(), gameManager.GetDayNumber(), "Flags (Dialog Manager)", $"day1: {npcSelected.DialogToShow.metOnday1}, day2: {npcSelected.DialogToShow.metOnday2}, day3: {npcSelected.DialogToShow.metOnday3}, day4: {npcSelected.DialogToShow.metOnday4}, day5: {npcSelected.DialogToShow.metOnday5}");
        GoogleSheetLogger.I.Log(gameManager.GetDayNumber(), "Dialog Start (Dialog Manager)", $"Started dialog with {npcSelected.DialogToShow.Name}");
        GoogleSheetLogger.I.Log(gameManager.GetDayNumber(), "Flags (Dialog Manager)", $"day1: {npcSelected.DialogToShow.metOnday1}, day2: {npcSelected.DialogToShow.metOnday2}, day3: {npcSelected.DialogToShow.metOnday3}, day4: {npcSelected.DialogToShow.metOnday4}, day5: {npcSelected.DialogToShow.metOnday5}");

        dialogPanel.SetActive(true);
        LoadDialogFromNPC();
        npcIcon.sprite = npcSelected.DialogToShow.Icon;
        npcNameTMP.text = npcSelected.DialogToShow.Name;
        npcDialogTMP.text = npcSelected.DialogToShow.Greeting;
        dialogStarted = true;

        dayNightCycleManager.StartStopTimer(true);
    }

    private void ContinueDialog()
    {
        if(npcSelected == null)
        {
            dialogQueue.Clear();
            return;
        }

        if(dialogQueue.Count <= 0)
        {
            dialogPanel.SetActive(false);
            dialogStarted = false;
            return;
        }

        npcDialogTMP.text = dialogQueue.Dequeue();
    }

    private void PlayClip(AudioClip clip)
    {
        if (clip == null || voiceSource == null) return;

        voiceSource.Stop();
        voiceSource.clip = clip;
        voiceSource.Play();
    }

    public void StopPlayClip()
    {   
        voiceSource.Stop();
    }

    private void OnEnable()
    {
        actions.Enable();
    }

    private void OnDisable()
    {
        actions.Disable();
    }

}
