using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureUI : Singleton<StructureUI>
{

    [Header("Config")]
    [SerializeField] private PlayerXP playerXP;
    [SerializeField] private int structurePercentageToIncrease = 0;
    [SerializeField] private GoogleSheetLogger logger;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private int structurePercentage = 10;
    [SerializeField] private AudioSource clearingDebrisAudioSource;

    private PlayerActions actions;
    //private int structureArrayValue = 2;
    public StructureInteraction structureInteraction { get; set; }

    protected override void Awake()
    {
        base.Awake();
        actions = new PlayerActions();
    }

    void Start()
    {
        actions.Structure.Repair.started += ctx => RepairStructure();
        actions.Structure.Repair.canceled += ctx => StopRepairStructure();
    }


    public void ResetStructurePercentageToIncrease()
    {
        structurePercentageToIncrease = 0;
    }

    public int GetStructurePercentageToIncrease()
    {
        return structurePercentageToIncrease;
    }

    public void RepairStructure()
    {
        if (structureInteraction == null) return;
        clearingDebrisAudioSource.Play();
        structureInteraction.StartRepair();
    }

    public void StopRepairStructure()
    {
        if (structureInteraction == null) return;
        clearingDebrisAudioSource.Stop();
        //logger.LogData(gameManager.GetPlayerIDForLogging(), gameManager.GetCurrentTime(), gameManager.GetDayNumber(), "Repair (Debris Manager)", $"Cancelled repair");
        //GoogleSheetLogger.I.Log(gameManager.GetDayNumber(), "Repair (Debris Manager)", $"Cancelled repair");
        GoogleSheetLogger.I.Log("Repair (Debris Manager)", $"Cancelled repair");

        structureInteraction.CancelRepair();
    }

    public void StructureRepairedAndIncreasePercentage()
    {
        clearingDebrisAudioSource.Stop();

        //logger.LogData(gameManager.GetPlayerIDForLogging(), gameManager.GetCurrentTime(), gameManager.GetDayNumber(), "Repair (Debris Manager)", $"Completed repair");
        //GoogleSheetLogger.I.Log(gameManager.GetDayNumber(), "Repair (Debris Manager)", $"Completed repair");
        GoogleSheetLogger.I.Log("Repair (Debris Manager)", $"Completed repair");

        structurePercentageToIncrease = structurePercentageToIncrease + structurePercentage;
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
