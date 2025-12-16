using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using static UnityEngine.EventSystems.EventTrigger;

public class UserFeedback : MonoBehaviour
{

    [Header("Pre- Questions")]
    [SerializeField] private string uniquePlayerID;
    [SerializeField] private string[] preQuestions = new string[8];

    [Header("Post- Questions")]
    [SerializeField] private string[] postQuestions = new string[22];
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

        for (int i = 0; i < allAnswers.Length; i++)
        {
            if(i < 8)
            {
                preQuestions[i] = allAnswers[i];
            }
            else
            {
                postQuestions[i - 8] = allAnswers[i];
            }
        }

        StartCoroutine(Post(uniquePlayerID, preQuestions, postQuestions));
    }

    private IEnumerator Post(string uniquePlayerID, string[] preQuestions, string[] postQuestions)
    {
        WWWForm form = new WWWForm();
        form.AddField("entry.1054966409", uniquePlayerID);

        form.AddField("entry.1861348342", preQuestions[0]);
        form.AddField("entry.356919410",  preQuestions[1]);
        form.AddField("entry.1650738171", preQuestions[2]);
        form.AddField("entry.1310830898", preQuestions[3]);
        form.AddField("entry.1446351570", preQuestions[4]);
        form.AddField("entry.1423793081", preQuestions[5]);
        form.AddField("entry.1938439870", preQuestions[6]);
        form.AddField("entry.1177154035", preQuestions[7]);

        form.AddField("entry.1249654724", postQuestions[0]);
        form.AddField("entry.280961845", postQuestions[1]);
        form.AddField("entry.1758121974", postQuestions[2]);
        form.AddField("entry.1955207573", postQuestions[3]);
        form.AddField("entry.851037222", postQuestions[4]);
        form.AddField("entry.38703101", postQuestions[5]);
        form.AddField("entry.1763242330", postQuestions[6]);
        form.AddField("entry.1180264205", postQuestions[7]);
        form.AddField("entry.254762140", postQuestions[8]);
        form.AddField("entry.1415800770", postQuestions[9]);
        form.AddField("entry.1113204731", postQuestions[10]);
        form.AddField("entry.1682195559", postQuestions[11]);
        form.AddField("entry.204410795", postQuestions[12]);
        form.AddField("entry.1731375377", postQuestions[13]);
        form.AddField("entry.1255216295", postQuestions[14]);
        form.AddField("entry.79092888", postQuestions[15]);
        form.AddField("entry.1548891211", postQuestions[16]);
        form.AddField("entry.1460842819", postQuestions[17]);
        form.AddField("entry.142979142", postQuestions[18]);
        form.AddField("entry.500007731", postQuestions[19]);
        form.AddField("entry.212075583", postQuestions[20]);
        form.AddField("entry.372842914", postQuestions[21]);
        form.AddField("entry.255498212", postQuestions[22]);

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
