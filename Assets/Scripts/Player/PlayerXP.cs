using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerXP : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private PlayerStats stats;

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

        float currentXPRequired = stats.NextLevelXPSupplies;
        float newNextLevelXP = Mathf.Round(currentXPRequired + stats.NextLevelXPSupplies * (stats.XPMultiplierSupplies / 100));
        stats.NextLevelXPSupplies = newNextLevelXP;
    }

    private void NextLevelArmour()
    {
        stats.ArmourLevel++;

        float currentXPRequired = stats.NextLevelXPArmour;
        float newNextLevelXP = Mathf.Round(currentXPRequired + stats.NextLevelXPArmour * (stats.XPMultiplierArmour / 100));

        stats.NextLevelXPArmour = newNextLevelXP;
    }

    private void NextLevelMedicine()
    {
        stats.MedicineLevel++;

        float currentXPRequired = stats.NextLevelXPMedicine;
        float newNextLevelXP = Mathf.Round(currentXPRequired + stats.NextLevelXPMedicine * (stats.XPMultiplierMedicine / 100));

        stats.NextLevelXPMedicine = newNextLevelXP;
    }

    public int[] GetLevels()
    {
        return new int[] { stats.SuppliesLevel, stats.MedicineLevel, stats.ArmourLevel };
    }
}
