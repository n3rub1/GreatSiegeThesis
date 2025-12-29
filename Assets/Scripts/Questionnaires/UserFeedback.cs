using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using static UnityEngine.EventSystems.EventTrigger;
using UnityEngine.SceneManagement;

public class UserFeedback : MonoBehaviour
{

    [Header("Pre- Questions")]
    [SerializeField] private string uniquePlayerID;
    [SerializeField] private string preQuestion1;
    [SerializeField] private string preQuestion2;
    [SerializeField] private string preQuestion3;
    [SerializeField] private string preQuestion4;
    [SerializeField] private string preQuestion5;
    [SerializeField] private string preQuestion6;
    [SerializeField] private string preQuestion7;
    [SerializeField] private string preQuestion8;


    [Header("Post- Questions")]
    [SerializeField] private string postQuestion1;
    [SerializeField] private string postQuestion2;
    [SerializeField] private string postQuestion3;
    [SerializeField] private string postQuestion4;
    [SerializeField] private string postQuestion5;
    [SerializeField] private string postQuestion6;
    [SerializeField] private string postQuestion7;
    [SerializeField] private string postQuestion8;
    [SerializeField] private string postQuestion9;
    [SerializeField] private string postQuestion10;
    [SerializeField] private string postQuestion11;
    [SerializeField] private string postQuestion12;
    [SerializeField] private string postQuestion13;
    [SerializeField] private string postQuestion14;
    [SerializeField] private string postQuestion15;
    [SerializeField] private string postQuestion16;
    [SerializeField] private string postQuestion17;
    [SerializeField] private string postQuestion18;
    [SerializeField] private string postQuestion19;
    [SerializeField] private string postQuestion20;
    [SerializeField] private string postQuestion21;
    [SerializeField] private string postQuestion22;
    [SerializeField] private string postQuestion23;

    [SerializeField] private Submit submit;

    private string formURL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLScceC0tlcOh-2VuN8f0qOkyZAJ2er0x3AQzjQEO7czEBlHu1g/formResponse";
    private string[] allAnswers = new string[] { };
    private PersistantSurveyQuestions persistantSurveyQuestions;

    private void Start()
    {
        uniquePlayerID = SystemInfo.deviceUniqueIdentifier;
        persistantSurveyQuestions = PersistantSurveyQuestions.Instance;
    }

    public void SubmitForm()
    {
        submit.SubmitValues();
        allAnswers = persistantSurveyQuestions.GetAllAnswerValues();
        preQuestion1 = allAnswers[0];
        preQuestion2 = allAnswers[1];
        preQuestion3 = allAnswers[2];
        preQuestion4 = allAnswers[3];
        preQuestion5 = allAnswers[4];
        preQuestion6 = allAnswers[5];
        preQuestion7 = allAnswers[6];
        preQuestion8 = allAnswers[7];

        postQuestion1 = allAnswers[8];
        postQuestion2 = allAnswers[9];
        postQuestion3 = allAnswers[10];
        postQuestion4 = allAnswers[11];
        postQuestion5 = allAnswers[12];
        postQuestion6 = allAnswers[13];
        postQuestion7 = allAnswers[14];
        postQuestion8 = allAnswers[15];
        postQuestion9 = allAnswers[16];
        postQuestion10 = allAnswers[17];
        postQuestion11 = allAnswers[18];
        postQuestion12 = allAnswers[19];
        postQuestion13 = allAnswers[20];
        postQuestion14 = allAnswers[21];
        postQuestion15 = allAnswers[22];
        postQuestion16 = allAnswers[23];
        postQuestion17 = allAnswers[24];
        postQuestion18 = allAnswers[25];
        postQuestion19 = allAnswers[26];
        postQuestion20 = allAnswers[27];
        postQuestion21 = allAnswers[28];
        postQuestion22 = allAnswers[29];
        postQuestion23 = allAnswers[30];


        StartCoroutine(Post(uniquePlayerID, preQuestion1, preQuestion2, preQuestion3, preQuestion4, preQuestion5, preQuestion6, preQuestion7, preQuestion8, postQuestion1, 
            postQuestion2, postQuestion3, postQuestion4, postQuestion5, postQuestion6, postQuestion7, postQuestion8, postQuestion9, postQuestion10, postQuestion11, postQuestion12,
            postQuestion13, postQuestion14, postQuestion15, postQuestion16, postQuestion17, postQuestion18, postQuestion19, postQuestion20, postQuestion21, postQuestion22, postQuestion23));


    }

    private IEnumerator Post(string uniquePlayerID, string preQuestion1, string preQuestion2, string preQuestion3, string preQuestion4, string preQuestion5, string preQuestion6, string preQuestion7,
        string preQuestion8, string postQuestion1, string postQuestion2, string postQuestion3, string postQuestion4, string postQuestion5, string postQuestion6, string postQuestion7, string postQuestion8,
        string postQuestion9, string postQuestion10, string postQuestion11, string postQuestion12, string postQuestion13, string postQuestion14, string postQuestion15, string postQuestion16, string postQuestion17,
        string postQuestion18, string postQuestion19, string postQuestion20, string postQuestion21, string postQuestion22, string postQuestion23)
    {
        WWWForm form = new WWWForm();
        form.AddField("entry.1054966409", uniquePlayerID);

        form.AddField("entry.1861348342", preQuestion1);
        form.AddField("entry.356919410",  preQuestion2);
        form.AddField("entry.1650738171", preQuestion3);
        form.AddField("entry.1310830898", preQuestion4);
        form.AddField("entry.1446351570", preQuestion5);
        form.AddField("entry.1423793081", preQuestion6);
        form.AddField("entry.1938439870", preQuestion7);
        form.AddField("entry.1177154035", preQuestion8);

        form.AddField("entry.1249654724", postQuestion1);
        form.AddField("entry.280961845", postQuestion2);
        form.AddField("entry.1758121974", postQuestion3);
        form.AddField("entry.1955207573", postQuestion4);
        form.AddField("entry.851037222", postQuestion5);
        form.AddField("entry.38703101", postQuestion6);
        form.AddField("entry.1763242330", postQuestion7);
        form.AddField("entry.1180264205", postQuestion8);
        form.AddField("entry.254762140", postQuestion9);
        form.AddField("entry.1415800770", postQuestion10);
        form.AddField("entry.1113204731", postQuestion11);
        form.AddField("entry.1682195559", postQuestion12);
        form.AddField("entry.204410795", postQuestion13);
        form.AddField("entry.1731375377", postQuestion14);
        form.AddField("entry.1255216295", postQuestion15);
        form.AddField("entry.79092888", postQuestion16);
        form.AddField("entry.1548891211", postQuestion17);
        form.AddField("entry.1460842819", postQuestion18);
        form.AddField("entry.142979142", postQuestion19);
        form.AddField("entry.500007731", postQuestion20);
        form.AddField("entry.212075583", postQuestion21);
        form.AddField("entry.372842914", postQuestion22);
        form.AddField("entry.255498212", postQuestion23);

        using (UnityWebRequest www = UnityWebRequest.Post(formURL, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Feedback Sent Successfully!");
            }
            else
            {
                Debug.LogWarning($"Error in sending feedback! {www.error}" );
            }
        }
    }



}
