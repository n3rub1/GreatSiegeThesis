using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatUI : Singleton<CatUI>
{

    private PlayerActions actions;
    public CatInteraction catInteraction { get; set; }

    protected override void Awake()
    {
        base.Awake();
        actions = new PlayerActions();
    }

    void Start()
    {
        actions.Cat.CollectClue.performed += ctx => CatClue();
    }

    private void OnEnable()
    {
        actions.Enable();
    }

    private void OnDisable()
    {
        actions.Disable();
    }

    private void CatClue()
    {
        if (catInteraction == null) return;
        catInteraction.OpenCatPanel();
    }

}
