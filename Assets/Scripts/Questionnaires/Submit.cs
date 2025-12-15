using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Submit : MonoBehaviour
{
    [Header("Dropdown questions")]
    public TMP_Dropdown q1;
    public TMP_Dropdown q2;

    [Header("Text questions")]
    public TMP_InputField q3;


    public void SubmitValues()
    {
        // Dropdowns (int)
        PersistantSurveyQuestions.Instance.likertData1Before = (q1.value + 1).ToString();
        PersistantSurveyQuestions.Instance.likertData2Before = (q2.value + 1).ToString();

        // Text fields (string)
        PersistantSurveyQuestions.Instance.openEndData3Before = q3.text;

        Debug.Log("All questionnaire values submitted.");
    }
}
