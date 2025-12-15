using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistantSurveyQuestions : MonoBehaviour
{
    public static PersistantSurveyQuestions Instance;

    [Header("Questions")]
    [SerializeField] private string uniquePlayerID;
    public string likertData1Before;
    public string likertData2Before;
    public string openEndData3Before;
    public string likertData1After;
    public string likertData2After;
    public string openEndData3After;

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

    public void CheckValues()
    {
        Debug.Log($"1: {likertData1Before}");
        Debug.Log($"2: {likertData2Before}");
        Debug.Log($"3: {openEndData3Before}");

    }
}
