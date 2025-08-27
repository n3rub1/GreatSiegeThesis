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

    public NPCInteraction npcSelected { get; set; }
    public GameManager gameManager;

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
    }

    private void LoadDialogFromNPC()
    {
        int currentDay = gameManager.getDayNumber();
        npcSelected.DialogToShow.dayNumber = currentDay;

        if (npcSelected.DialogToShow.Day1Dialog.Length <= 0 || 
            npcSelected.DialogToShow.Day2MetBeforeDialog.Length <= 0|| 
            npcSelected.DialogToShow.Day2FirstTimeDialog.Length <= 0 ||
            npcSelected.DialogToShow.Day3MetBeforeDialog.Length <= 0 ||
            npcSelected.DialogToShow.Day3FirstTimeDialog.Length <= 0 ||
            npcSelected.DialogToShow.Day4MetBeforeDialog.Length <= 0 ||
            npcSelected.DialogToShow.Day4FirstTimeDialog.Length <= 0 ||
            npcSelected.DialogToShow.Day5MetBeforeDialog.Length <= 0 ||
            npcSelected.DialogToShow.Day5FirstTimeDialog.Length <= 0) return;

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
                break;

            case 2:
                if (npcSelected.DialogToShow.metOnday1)
                {
                    foreach (string sentence in npcSelected.DialogToShow.Day2MetBeforeDialog)
                        dialogQueue.Enqueue(sentence);
                    npcSelected.DialogToShow.metOnday2 = true;
                }
                else
                {
                    foreach (string sentence in npcSelected.DialogToShow.Day2FirstTimeDialog)
                        dialogQueue.Enqueue(sentence);
                    npcSelected.DialogToShow.metOnday2 = true;
                }
                break;

            case 3:
                if (npcSelected.DialogToShow.metOnday1 || npcSelected.DialogToShow.metOnday2)
                {
                    foreach (string sentence in npcSelected.DialogToShow.Day3MetBeforeDialog)
                        dialogQueue.Enqueue(sentence);
                    npcSelected.DialogToShow.metOnday3 = true;
                }
                else
                {
                    foreach (string sentence in npcSelected.DialogToShow.Day3FirstTimeDialog)
                        dialogQueue.Enqueue(sentence);
                    npcSelected.DialogToShow.metOnday3 = true;
                }
                break;

            case 4:
                if (npcSelected.DialogToShow.metOnday1 || npcSelected.DialogToShow.metOnday2 || npcSelected.DialogToShow.metOnday3)
                {
                    foreach (string sentence in npcSelected.DialogToShow.Day4MetBeforeDialog)
                        dialogQueue.Enqueue(sentence);
                    npcSelected.DialogToShow.metOnday3 = true;
                }
                else
                {
                    foreach (string sentence in npcSelected.DialogToShow.Day4FirstTimeDialog)
                        dialogQueue.Enqueue(sentence);
                    npcSelected.DialogToShow.metOnday4 = true;
                }
                break;

            case 5:
                if (npcSelected.DialogToShow.metOnday1 || npcSelected.DialogToShow.metOnday2 || npcSelected.DialogToShow.metOnday3 || npcSelected.DialogToShow.metOnday4)
                {
                    foreach (string sentence in npcSelected.DialogToShow.Day5MetBeforeDialog)
                        dialogQueue.Enqueue(sentence);
                    npcSelected.DialogToShow.metOnday5 = true;
                }
                else
                {
                    foreach (string sentence in npcSelected.DialogToShow.Day5FirstTimeDialog)
                        dialogQueue.Enqueue(sentence);
                    npcSelected.DialogToShow.metOnday5 = true;
                }
                break;
        }
    }

    private void ShowDialog()
    {
        if (npcSelected == null) return;
        if (dialogStarted) return;
        dialogPanel.SetActive(true);
        LoadDialogFromNPC();
        npcIcon.sprite = npcSelected.DialogToShow.Icon;
        npcNameTMP.text = npcSelected.DialogToShow.Name;
        npcDialogTMP.text = npcSelected.DialogToShow.Greeting;
        dialogStarted = true;
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

    private void OnEnable()
    {
        actions.Enable();
    }

    private void OnDisable()
    {
        actions.Disable();
    }

}
