using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Submit : MonoBehaviour
{
    [Header("Pre- Dropdown questions")]
    public TMP_Dropdown preQuestion1;
    public TMP_Dropdown preQuestion2;
    public TMP_Dropdown preQuestion3;
    public TMP_Dropdown preQuestion4;
    public TMP_Dropdown preQuestion5;
    public TMP_Dropdown preQuestion6;
    public TMP_Dropdown preQuestion7;
    public TMP_Dropdown preQuestion8;

    //[Header("Pre- Text questions")]
    //public TMP_InputField q3;

    [Header("Post- Dropdown questions")]
    public TMP_Dropdown postQuestion1;
    public TMP_Dropdown postQuestion2;
    public TMP_Dropdown postQuestion3;
    public TMP_Dropdown postQuestion4;
    public TMP_Dropdown postQuestion5;
    public TMP_Dropdown postQuestion6;
    public TMP_Dropdown postQuestion7;
    public TMP_Dropdown postQuestion8;
    public TMP_Dropdown postQuestion9;
    public TMP_Dropdown postQuestion10;
    public TMP_Dropdown postQuestion11;
    public TMP_Dropdown postQuestion12;
    public TMP_Dropdown postQuestion13;
    public TMP_Dropdown postQuestion14;
    public TMP_Dropdown postQuestion15;
    public TMP_Dropdown postQuestion16;
    public TMP_Dropdown postQuestion17;

    [Header("Post- Text questions")]
    public TMP_InputField postQuestion18;
    public TMP_InputField postQuestion19;
    public TMP_InputField postQuestion20;
    public TMP_InputField postQuestion21;
    public TMP_InputField postQuestion22;
    public TMP_InputField postQuestion23;

    private PersistantSurveyQuestions persistantSurveyQuestions;

    [SerializeField] private int mainGame = 2; // should be 2

    public void Start()
    {
        persistantSurveyQuestions = PersistantSurveyQuestions.Instance;
    }

    public void SubmitValues()
    {

        Debug.Log(persistantSurveyQuestions.GetIsFinal());

        if (persistantSurveyQuestions.GetIsFinal() == false)
        {
            // Pre- Dropdown fields (int)
            PersistantSurveyQuestions.Instance.preQuestion1 = (preQuestion1.value + 1).ToString();
            PersistantSurveyQuestions.Instance.preQuestion2 = (preQuestion2.value + 1).ToString();
            PersistantSurveyQuestions.Instance.preQuestion3 = (preQuestion3.value + 1).ToString();
            PersistantSurveyQuestions.Instance.preQuestion4 = (preQuestion4.value + 1).ToString();
            PersistantSurveyQuestions.Instance.preQuestion5 = (preQuestion5.value + 1).ToString();
            PersistantSurveyQuestions.Instance.preQuestion6 = (preQuestion6.value + 1).ToString();
            PersistantSurveyQuestions.Instance.preQuestion7 = (preQuestion7.value + 1).ToString();
            PersistantSurveyQuestions.Instance.preQuestion8 = (preQuestion8.value + 1).ToString();

            Debug.Log("Pre- values submitted.");
            persistantSurveyQuestions.SetIsFinal(true);
            SceneManager.LoadScene(mainGame);
        }
        else
        {
            // Post- Dropdown fields (int)
            PersistantSurveyQuestions.Instance.postQuestion1 = (postQuestion1.value + 1).ToString();
            PersistantSurveyQuestions.Instance.postQuestion2 = (postQuestion2.value + 1).ToString();
            PersistantSurveyQuestions.Instance.postQuestion3 = (postQuestion3.value + 1).ToString();
            PersistantSurveyQuestions.Instance.postQuestion4 = (postQuestion4.value + 1).ToString();
            PersistantSurveyQuestions.Instance.postQuestion5 = (postQuestion5.value + 1).ToString();
            PersistantSurveyQuestions.Instance.postQuestion6 = (postQuestion6.value + 1).ToString();
            PersistantSurveyQuestions.Instance.postQuestion7 = (postQuestion7.value + 1).ToString();
            PersistantSurveyQuestions.Instance.postQuestion8 = (postQuestion8.value + 1).ToString();
            PersistantSurveyQuestions.Instance.postQuestion9 = (postQuestion9.value + 1).ToString();
            PersistantSurveyQuestions.Instance.postQuestion10 = (postQuestion10.value + 1).ToString();
            PersistantSurveyQuestions.Instance.postQuestion11 = (postQuestion11.value + 1).ToString();
            PersistantSurveyQuestions.Instance.postQuestion12 = (postQuestion12.value + 1).ToString();
            PersistantSurveyQuestions.Instance.postQuestion13 = (postQuestion13.value + 1).ToString();
            PersistantSurveyQuestions.Instance.postQuestion14 = (postQuestion14.value + 1).ToString();
            PersistantSurveyQuestions.Instance.postQuestion15 = (postQuestion15.value + 1).ToString();
            PersistantSurveyQuestions.Instance.postQuestion16 = (postQuestion16.value + 1).ToString();
            PersistantSurveyQuestions.Instance.postQuestion17 = (postQuestion17.value + 1).ToString();

            //Post- Text fields (string)
            PersistantSurveyQuestions.Instance.postQuestion18 = postQuestion18.text;
            PersistantSurveyQuestions.Instance.postQuestion19 = postQuestion19.text;
            PersistantSurveyQuestions.Instance.postQuestion20 = postQuestion20.text;
            PersistantSurveyQuestions.Instance.postQuestion21 = postQuestion21.text;
            PersistantSurveyQuestions.Instance.postQuestion22 = postQuestion22.text;
            PersistantSurveyQuestions.Instance.postQuestion23 = postQuestion23.text;

            Debug.Log("Post- values submitted.");

            SceneManager.LoadScene(mainGame);  // to be removed or go to a credits scene.  its here for testing.
        }
    }
}
