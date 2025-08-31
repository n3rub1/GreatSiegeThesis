using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerXP : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private PlayerStats stats;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private float timeOfXPPanel = 5f;

    private void Update()
    {
        //TEST XP
        if (Input.GetKeyDown(KeyCode.X))
        {
            AddXPSupplies(5f);
            AddXPArmour(5f);
            AddXPMedicine(5f);
        }
    }

    public void AddXPSupplies (float amount)
    {
        stats.CurrentXPSupplies = stats.CurrentXPSupplies + amount;
        while (stats.CurrentXPSupplies >= stats.NextLevelXPSupplies)
        {
            stats.CurrentXPSupplies = stats.CurrentXPSupplies - stats.NextLevelXPSupplies;
            NextLevelSupplies();
        }
    }

    public void AddXPArmour(float amount)
    {
        stats.CurrentXPArmour = stats.CurrentXPArmour + amount;
        int loopIssue = 10;
        while (stats.CurrentXPArmour >= stats.NextLevelXPArmour && loopIssue > 0)
        {
            stats.CurrentXPArmour = stats.CurrentXPArmour - stats.NextLevelXPArmour;
            NextLevelArmour();
            loopIssue = loopIssue - 1;

            if(loopIssue <= 0)
            {
                Debug.Log("INF. LOOP TRIGGERED!");
            }
        }
    }

    public void AddXPMedicine(float amount)
    {
        stats.CurrentXPMedicine = stats.CurrentXPMedicine + amount;
        while (stats.CurrentXPMedicine >= stats.NextLevelXPMedicine)
        {
            stats.CurrentXPMedicine = stats.CurrentXPMedicine - stats.NextLevelXPMedicine;
            NextLevelMedicine();
        }
    }

    private void NextLevelSupplies()
    {
        stats.SuppliesLevel++;

        uiManager.ShowLevelUp("Supplies");

        float currentXPRequired = stats.NextLevelXPSupplies;
        float newNextLevelXP = Mathf.Round(currentXPRequired + stats.NextLevelXPSupplies * (stats.XPMultiplierSupplies / 100));
        stats.NextLevelXPSupplies = newNextLevelXP;

        StartCoroutine(HideShowLevelUp());
    }

    private void NextLevelArmour()
    {
        stats.ArmourLevel++;

        uiManager.ShowLevelUp("Armoury");

        float currentXPRequired = stats.NextLevelXPArmour;
        float newNextLevelXP = Mathf.Round(currentXPRequired + stats.NextLevelXPArmour * (stats.XPMultiplierArmour / 100));

        stats.NextLevelXPArmour = newNextLevelXP;

        StartCoroutine(HideShowLevelUp());
    }

    private void NextLevelMedicine()
    {
        stats.MedicineLevel++;

        uiManager.ShowLevelUp("Medicine");

        float currentXPRequired = stats.NextLevelXPMedicine;
        float newNextLevelXP = Mathf.Round(currentXPRequired + stats.NextLevelXPMedicine * (stats.XPMultiplierMedicine / 100));

        stats.NextLevelXPMedicine = newNextLevelXP;

        StartCoroutine(HideShowLevelUp());
    }

    public int[] GetLevels()
    {
        return new int[] { stats.SuppliesLevel, stats.MedicineLevel, stats.ArmourLevel };
    }

    private IEnumerator HideShowLevelUp()
    {
        yield return new WaitForSeconds(timeOfXPPanel);
        uiManager.HideLevelUp();

    }


}
