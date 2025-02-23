using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerXP : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private PlayerStats stats;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            AddXP(300f);
        }
    }

        public void AddXP(float amount)
    {
        stats.CurrentXP = stats.CurrentXP + amount;
        while (stats.CurrentXP >= stats.NextLevelXP)
        {
            stats.CurrentXP = stats.CurrentXP - stats.NextLevelXP;
            NextLevel();
        }
    }

    private void NextLevel()
    {
        stats.Level++;

        float currentXPRequired = stats.NextLevelXP;
        float newNextLevelXP = Mathf.Round(currentXPRequired + stats.NextLevelXP * (stats.XPMultiplier / 100));

        stats.NextLevelXP = newNextLevelXP;
    }
}
