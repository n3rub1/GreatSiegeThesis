using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PercentageManager : MonoBehaviour
{

    [Header("Config")]
    [SerializeField] private UIManager uiManager;
    [SerializeField] private int moralPercentageUpdate;
    [SerializeField] private int weaponsPercentageUpdate;
    [SerializeField] private int structurePercentageUpdate;
    [SerializeField] private int catPercentageUpdate;

    [SerializeField] private int moralPercentage;
    [SerializeField] private int weaponsPercentage;
    [SerializeField] private int structurePercentage;
    [SerializeField] private int catPercentage;

    private int increaseDecreaseAmountToMoral;
    private int increaseDecreaseAmountToWeapons;
    private int increaseDecreaseAmountToStructure;
    private int increaseDecreaseAmountToCat;


    public void UpdatePercentages(int addAmountToMoral, int addAmountToWeapons, int addAmountToStructure, int addAmountToCat)
    {

        Debug.Log("CAT : " + addAmountToCat);

        moralPercentageUpdate = (addAmountToMoral == 0) ? -5 : addAmountToMoral;
        weaponsPercentageUpdate = (addAmountToWeapons == 0) ? -5 : addAmountToWeapons;
        structurePercentageUpdate = (addAmountToStructure == 0) ? -5 : addAmountToStructure;
        catPercentageUpdate = (addAmountToCat == 0) ? -0: addAmountToCat;

        increaseDecreaseAmountToMoral = moralPercentageUpdate;
        increaseDecreaseAmountToWeapons = weaponsPercentageUpdate;
        increaseDecreaseAmountToStructure = structurePercentageUpdate;
        increaseDecreaseAmountToCat = catPercentageUpdate;

        UpdateNumbers();
    }

    public List<int> GetIncreaseDecreasePercenatageNumbers()
    {
        List<int> allIncreaseDecreasePercentageNumbers = new List<int>();
        allIncreaseDecreasePercentageNumbers.Add(increaseDecreaseAmountToMoral);
        allIncreaseDecreasePercentageNumbers.Add(increaseDecreaseAmountToWeapons);
        allIncreaseDecreasePercentageNumbers.Add(increaseDecreaseAmountToStructure);
        allIncreaseDecreasePercentageNumbers.Add(increaseDecreaseAmountToCat);

        return allIncreaseDecreasePercentageNumbers;
    }

    private void UpdateNumbers()
    {
        moralPercentage = ((moralPercentage + moralPercentageUpdate) <= 0) ? 0: (moralPercentage + moralPercentageUpdate);
        weaponsPercentage = ((weaponsPercentage + weaponsPercentageUpdate) <= 0) ? 0 : (weaponsPercentage + weaponsPercentageUpdate);
        structurePercentage = ((structurePercentage + structurePercentageUpdate) <= 0) ? 0 : (structurePercentage + structurePercentageUpdate);
        catPercentage = ((catPercentage + catPercentageUpdate) <= 0) ? 0 : (catPercentage + catPercentageUpdate);

        moralPercentage = moralPercentage > 100 ? 100 : moralPercentage;
        weaponsPercentage = weaponsPercentage > 100 ? 100 : weaponsPercentage;
        structurePercentage = structurePercentage > 100 ? 100 : structurePercentage;
        catPercentage = catPercentage > 100 ? 100 : catPercentage;

        UpdateUI();
    }

    private void UpdateUI()
    {
        uiManager.UpdatePercentages(moralPercentage, weaponsPercentage, structurePercentage, catPercentage);
    }

    public List<int> GetPercentages()
    {
        List<int> allPercentages = new List<int>();
        allPercentages.Add(moralPercentage);
        allPercentages.Add(weaponsPercentage);
        allPercentages.Add(structurePercentage);
        allPercentages.Add(catPercentage);

        return allPercentages;
    }

}
