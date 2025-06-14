using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Player Stats")]
public class PlayerStats : ScriptableObject
{
    //[Header ("Config")]
    //public int Level;

    //    [Header ("Health")]
    //    public float Health;
    //    public float MaxHealth;

    //    [Header ("Stamina")]
    //    public float Stamina;
    //    public float MaxStamina;

    [Header("XP Supplies")]
    public int SuppliesLevel;
    public float CurrentXPSupplies;
    public float NextLevelXPSupplies;
    public float IntialNextLevelXPSupplies;
    [Range(1f, 100f)] public float XPMultiplierSupplies;

    [Header("XP Medicine")]
    public int MedicineLevel;
    public float CurrentXPMedicine;
    public float NextLevelXPMedicine;
    public float IntialNextLevelXPMedicine;
    [Range(1f, 100f)] public float XPMultiplierMedicine;

    [Header("XP Armour")]
    public int ArmourLevel;
    public float CurrentXPArmour;
    public float NextLevelXPArmour;
    public float IntialNextLevelXPArmour;
    [Range(1f, 100f)] public float XPMultiplierArmour;

    public void ResetPlayer()
    {
        //        Health = MaxHealth;
        //        Stamina = MaxStamina;
        SuppliesLevel = 1;
        MedicineLevel = 1;
        ArmourLevel = 1;
        CurrentXPSupplies = 0f;
        CurrentXPMedicine = 0f;
        CurrentXPArmour = 0f;
        NextLevelXPSupplies = IntialNextLevelXPSupplies;
        NextLevelXPMedicine = IntialNextLevelXPMedicine;
        NextLevelXPArmour = IntialNextLevelXPArmour;
    }
}
