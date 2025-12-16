using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistantSurveyQuestions : MonoBehaviour
{
    public static PersistantSurveyQuestions Instance;

    [Header("Pre- Questions")]
    [SerializeField] private string uniquePlayerID;
    public string preQuestion1;
    public string preQuestion2;
    public string preQuestion3;
    public string preQuestion4;
    public string preQuestion5;
    public string preQuestion6;
    public string preQuestion7;
    public string preQuestion8;

    [Header("Post- Questions")]
    public string postQuestion1;
    public string postQuestion2;
    public string postQuestion3;
    public string postQuestion4;
    public string postQuestion5;
    public string postQuestion6;
    public string postQuestion7;
    public string postQuestion8;
    public string postQuestion9;
    public string postQuestion10;
    public string postQuestion11;
    public string postQuestion12;
    public string postQuestion13;
    public string postQuestion14;
    public string postQuestion15;
    public string postQuestion16;
    public string postQuestion17;
    public string postQuestion18;  //input text
    public string postQuestion19;
    public string postQuestion20;
    public string postQuestion21;
    public string postQuestion22;
    public string postQuestion23;

    private bool isFinal = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)  //removes if duplicated
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public bool GetIsFinal()
    {
        return isFinal;
    }

    public void SetIsFinal(bool setIsFinal)
    {
        isFinal = setIsFinal;
    }

    public string[] GetAllAnswerValues()
    {
        return new string[]
        {
            preQuestion1,
            preQuestion2,
            preQuestion3,
            preQuestion4,
            preQuestion5,
            preQuestion6,
            preQuestion7,
            preQuestion8,
            postQuestion1,
            postQuestion2,
            postQuestion3,
            postQuestion4,
            postQuestion5,
            postQuestion6,
            postQuestion7,
            postQuestion8,
            postQuestion9,
            postQuestion10,
            postQuestion11,
            postQuestion12,
            postQuestion13,
            postQuestion14,
            postQuestion15,
            postQuestion16,
            postQuestion17,
            postQuestion18,
            postQuestion19,
            postQuestion20,
            postQuestion21,
            postQuestion22,
            postQuestion23
        };
    }
}
