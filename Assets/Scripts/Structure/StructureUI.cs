using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureUI : Singleton<StructureUI>
{

    [Header("Config")]
    [SerializeField] private PlayerXP playerXP;

    private PlayerActions actions;
    private bool isPlayerInRangeOfRepair = false;
    private int structureArrayValue = 2;
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

    private void OnEnable()
    {
        actions.Enable();
    }

    private void OnDisable()
    {
        actions.Disable();
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

}
