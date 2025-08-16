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

    [Header("QuestMarker")]
    [SerializeField] float bounceHeight;
    [SerializeField] float bounceSpeed;
    [SerializeField] GameObject questMarker;
    [SerializeField] private GameManager gameManager;


    private Vector3 startPosition;
    private Vector3 originalPosition;
    private PlayerActions actions;
    public QuestInteraction questInteraction { get; set; }


    protected override void Awake()
    {
        base.Awake();
        actions = new PlayerActions();
    }

    private void Start()
    {
        startPosition = questMarker.transform.localPosition;
        originalPosition = questMarker.transform.localPosition;
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

    private void OpenQuestPanel()
    {
        if (questInteraction == null || gameManager.GetQuestAccepted() != "") return;
        questPanel.SetActive(true);
    }

    public void ArmouryQuestAccepted()
    {
        Debug.Log("Armoury Quest Accepted");
        gameManager.SetQuestAccepted("Armoury");
        CloseQuestPanel();
        RemoveQuestMarker();
    }

    public void InfirmaryQuestAccepted()
    {
        Debug.Log("Infirmary Quest Accepted");
        gameManager.SetQuestAccepted("Infirmary");
        CloseQuestPanel();
        RemoveQuestMarker();
    }

    public void CatQuestAccepted()
    {
        Debug.Log("Cat Quest Accepted");
        gameManager.SetQuestAccepted("Cat");
        CloseQuestPanel();
        RemoveQuestMarker();
    }

    public void StructureQuestAccepted()
    {
        Debug.Log("Structure Quest Accepted");
        gameManager.SetQuestAccepted("Structure");
        CloseQuestPanel();
        RemoveQuestMarker();
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
