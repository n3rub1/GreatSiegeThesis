using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestUI : Singleton<QuestUI>
{

    [Header("Config")]
    [SerializeField] private GameObject questPanel;
    [SerializeField] private TextMeshProUGUI questMarkerTMP;
    [SerializeField] private GameObject additionalInformationQuestPanel;
    [SerializeField] private TextMeshProUGUI additionalInformationQuestPanelTMP;
    [SerializeField] [TextArea(5, 15)] private List<string> questDetails = new List<string>();

    [Header("QuestMarker")]
    [SerializeField] float bounceHeight;
    [SerializeField] float bounceSpeed;
    [SerializeField] GameObject questMarker;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private DayNightCycleManager dayNightCycleManager;


    private Vector3 startPosition;
    private Vector3 originalPosition;
    private PlayerActions actions;
    private bool anyQuestAccepted;
    private bool isAdditionalDetailsOpen = false;
    private string currentDetailsOpened;

    public QuestInteraction questInteraction { get; set; }


    protected override void Awake()
    {
        base.Awake();
        actions = new PlayerActions();
    }

    private void Start()
    {
        anyQuestAccepted = false;
        startPosition = questMarker.transform.localPosition;
        originalPosition = questMarker.transform.localPosition;
        ResetQuests();
        actions.Quest.TakeQuest.performed += ctx => OpenQuestPanel();
    }

    private void Update()
    {
        if (questInteraction == null)
        {
            float newY = startPosition.y + Mathf.Sin(Time.time * bounceSpeed) * bounceHeight;
            questMarker.transform.localPosition = new Vector3(startPosition.x, newY, startPosition.z);
        }
        else
        {
            startPosition = originalPosition;
        }

        if (dayNightCycleManager.GetCurrentTime() >= 22)
        {
            RemoveQuestMarker();
        }
        else if (dayNightCycleManager.GetCurrentTime() < 22 && anyQuestAccepted == false)
        {
            ActivateQuestMarker();
        }
    }

    public void ChangeQuestMarker()
    {
        questMarkerTMP.text = "f";
    }

    public void CloseQuestPanel()
    {
        questPanel.SetActive(false);
        questMarkerTMP.text = "!";
    }

    private void RemoveQuestMarker()
    {
        questMarker.SetActive(false);
    }

    private void ActivateQuestMarker()
    {
        questMarker.SetActive(true);
    }

    private void OpenQuestPanel()
    {
        if (questInteraction == null || dayNightCycleManager.GetCurrentTime() >= 22 || anyQuestAccepted) return;  //|| gameManager.GetQuestAccepted() != "Reset"

        additionalInformationQuestPanel.SetActive(false);
        SelectProperQuestText("reset");
        questPanel.SetActive(true);
    }

    public void CloseAdditionalDetails()
    {
        additionalInformationQuestPanel.SetActive(false);
        isAdditionalDetailsOpen = false;
        SelectProperQuestText("reset");
    }

    public void OpenAdditionalDetails(string quest)
    {
 
        if (isAdditionalDetailsOpen && currentDetailsOpened == quest)
        {
            additionalInformationQuestPanel.SetActive(false);
            SelectProperQuestText(quest);
            isAdditionalDetailsOpen = false;
            SelectProperQuestText("reset");
            currentDetailsOpened = "reset";
        }
        else
        {
            additionalInformationQuestPanel.SetActive(true);
            SelectProperQuestText(quest);
            isAdditionalDetailsOpen = true;
            currentDetailsOpened = quest;
        }

    }

    private void SelectProperQuestText(string quest)
    {

        if(quest == "armoury")
        {
        additionalInformationQuestPanelTMP.text = questDetails[0];

        }else if(quest == "infirmary")
        {
            additionalInformationQuestPanelTMP.text = questDetails[1];

        }
        else if(quest == "cat")
        {
            additionalInformationQuestPanelTMP.text = questDetails[2];

        }
        else if(quest == "help")
        {
            additionalInformationQuestPanelTMP.text = questDetails[3];
        }else if(quest == "reset")
        {
            additionalInformationQuestPanelTMP.text = "";

        }

    }

    public void ArmouryQuestAccepted()
    {
        gameManager.SetQuestAccepted("Armoury");
        CloseQuestPanel();
        RemoveQuestMarker();
        anyQuestAccepted = true;
    }

    public void InfirmaryQuestAccepted()
    {
        gameManager.SetQuestAccepted("Infirmary");
        CloseQuestPanel();
        RemoveQuestMarker();
        anyQuestAccepted = true;
    }

    public void CatQuestAccepted()
    {
        gameManager.SetQuestAccepted("Cat");
        CloseQuestPanel();
        RemoveQuestMarker();
        anyQuestAccepted = true;
    }

    public void StructureQuestAccepted()
    {
        gameManager.SetQuestAccepted("Structure");
        CloseQuestPanel();
        RemoveQuestMarker();
        anyQuestAccepted = true;
    }

    public void ResetQuests()
    {
        ActivateQuestMarker();
        gameManager.SetQuestAccepted("Reset");
        anyQuestAccepted = false;
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
