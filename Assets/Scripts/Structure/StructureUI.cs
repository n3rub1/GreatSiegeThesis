using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureUI : Singleton<StructureUI>
{

    [Header("Config")]
    [SerializeField] private PlayerXP playerXP;
    [SerializeField] private int structurePercentageToIncrease = 0;

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
        structureInteraction.StartRepair();
    }

    public void StopRepairStructure()
    {
        if (structureInteraction == null) return;
        structureInteraction.CancelRepair();
    }

    public void StructureRepairedAndIncreasePercentage()
    {
        structurePercentageToIncrease = structurePercentageToIncrease + 10;
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
