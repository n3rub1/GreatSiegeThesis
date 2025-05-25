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
    [SerializeField] private Image healthBar;
    [SerializeField] private Image staminaBar;
    [SerializeField] private Image xpBar;

    [Header("Text")]
    [SerializeField] private TextMeshProUGUI levelTMP;
    [SerializeField] private TextMeshProUGUI healthTMP;
    [SerializeField] private TextMeshProUGUI staminaTMP;
    [SerializeField] private TextMeshProUGUI xpTMP;
    [SerializeField] private TextMeshProUGUI dayNumber;

    private void Update()
    {
        UpdatePlayerUI();
    }

    private void UpdatePlayerUI()
    {
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, stats.Health / stats.MaxHealth, 10f*Time.deltaTime);
        staminaBar.fillAmount = Mathf.Lerp(staminaBar.fillAmount, stats.Stamina / stats.MaxStamina, 10f * Time.deltaTime);
        xpBar.fillAmount = Mathf.Lerp(xpBar.fillAmount, stats.CurrentXP / stats.NextLevelXP, 10f * Time.deltaTime);

        levelTMP.text = $"Level {stats.Level}";
        healthTMP.text = $"HP {stats.Health} / {stats.MaxHealth}";
        staminaTMP.text = $"STM {stats.Stamina} / {stats.MaxStamina}";
        xpTMP.text = $"XP {stats.CurrentXP} / {stats.NextLevelXP}";
        dayNumber.text = $"Day {gameManager.dayNumber}";
    }
}
