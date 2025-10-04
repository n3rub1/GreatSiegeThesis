using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatUI : Singleton<CatUI>
{

    private PlayerActions actions;
    public CatInteraction catInteraction { get; set; }
    private int catClueNumber = 0;
    private int catPercentageToIncrease = 0;

    protected override void Awake()
    {
        base.Awake();
        actions = new PlayerActions();
    }

    void Start()
    {
        actions.Cat.CollectClue.performed += ctx => CatClue();
    }

    public int getCatClueNumber()
    {
        return catClueNumber;
    }

    public void setCatClueNumber()
    {
        catClueNumber++;
    }

    private void CatClue()
    {
        if (catInteraction == null) return;
        catInteraction.OpenCatPanel();
        catPercentageToIncrease = catPercentageToIncrease + 10;
    }

    public void ResetCatPercentageToIncrease()
    {
        catPercentageToIncrease = 0;
    }

    public int GetCatPercentageToIncrease()
    {
        return catPercentageToIncrease;
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
