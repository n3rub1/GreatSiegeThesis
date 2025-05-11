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
        if (npcSelected.DialogToShow.Dialog.Length <= 0) return;

        foreach(string sentence in npcSelected.DialogToShow.Dialog)
        {
            dialogQueue.Enqueue(sentence);
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
