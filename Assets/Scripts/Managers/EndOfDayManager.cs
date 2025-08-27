using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndOfDayManager : MonoBehaviour
{

    [Header("Config")]
    [SerializeField] private List<string> endOfDayDescriptions;
    [SerializeField] private TextMeshProUGUI endOfDayDescriptionTMP;
    [SerializeField] private TextMeshProUGUI endOfDayNumber;
    [SerializeField] private GameObject endOfDayPanel;

    public void ShowPanelAndText(int dayNumber)
    {
        Debug.Log($"Ending day {dayNumber}");
        endOfDayDescriptionTMP.text = endOfDayDescriptions[dayNumber-1];
        endOfDayNumber.text = $"Day {dayNumber}";
        StartCoroutine(ShowAndHidePanel());
    }

    IEnumerator ShowAndHidePanel()
    {
        endOfDayPanel.SetActive(true);
        yield return new WaitForSeconds(10f);
        endOfDayPanel.SetActive(false);
    }

}
