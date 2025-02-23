using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Player Stats")]
public class PlayerStats : ScriptableObject
{
    [Header ("Config")]
    public int Level;

    [Header ("Health")]
    public float Health;
    public float MaxHealth;

    [Header ("Stamina")]
    public float Stamina;
    public float MaxStamina;

    [Header("XP")]
    public float CurrentXP;
    public float NextLevelXP;
    public float IntialNextLevelXP;
    [Range(1f, 100f)] public float XPMultiplier;

    public void ResetPlayer()
    {
        Health = MaxHealth;
        Stamina = MaxStamina;
        Level = 1;
        CurrentXP = 0f;
        NextLevelXP = IntialNextLevelXP;
    }
}
