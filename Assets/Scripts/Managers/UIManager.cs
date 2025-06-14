using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header ("Stats")]
    [SerializeField] private PlayerStats stats;
    [SerializeField] private GameManager gameManager;

    [Header("Bars")]
    //[SerializeField] private Image healthBar;
    //[SerializeField] private Image staminaBar;
    [SerializeField] private Image xpBarSupplies;
    [SerializeField] private Image xpBarArmour;
    [SerializeField] private Image xpBarMedicine;


    [Header("Text")]
    [SerializeField] private TextMeshProUGUI levelTMP;
    //[SerializeField] private TextMeshProUGUI healthTMP;
    //[SerializeField] private TextMeshProUGUI staminaTMP;
    [SerializeField] private TextMeshProUGUI xpSuppliesTMP;
    [SerializeField] private TextMeshProUGUI xpArmourTMP;
    [SerializeField] private TextMeshProUGUI xpMedicineTMP;
    [SerializeField] private TextMeshProUGUI dayNumber;

    private void Start()
    {
        stats.ResetPlayer();
    }

    private void Update()
    {
        UpdatePlayerUI();
    }

    private void UpdatePlayerUI()
    {
        //healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, stats.Health / stats.MaxHealth, 10f*Time.deltaTime);
        //staminaBar.fillAmount = Mathf.Lerp(staminaBar.fillAmount, stats.Stamina / stats.MaxStamina, 10f * Time.deltaTime);
        //xpBar.fillAmount = Mathf.Lerp(xpBar.fillAmount, stats.CurrentXPArmour / stats.NextLevelXPArmour, 10f * Time.deltaTime);

        //levelTMP.text = $"Level {stats.Level}";
        //healthTMP.text = $"HP {stats.Health} / {stats.MaxHealth}";
        //staminaTMP.text = $"STM {stats.Stamina} / {stats.MaxStamina}";
        xpSuppliesTMP.text = $"XP Supplies {stats.CurrentXPSupplies} / {stats.NextLevelXPSupplies} -- Level {stats.SuppliesLevel}";
        xpArmourTMP.text = $"XP Armoury {stats.CurrentXPArmour} / {stats.NextLevelXPArmour} -- Level {stats.ArmourLevel}";
        xpMedicineTMP.text = $"XP Medicine {stats.CurrentXPMedicine} / {stats.NextLevelXPMedicine} -- Level {stats.MedicineLevel}";
    }
}
