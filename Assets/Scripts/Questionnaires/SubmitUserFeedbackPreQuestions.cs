using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class SubmitUserFeedbackPreQuestions : MonoBehaviour
{
    //https://docs.google.com/forms/u/0/d/e/1FAIpQLSfoYaSP6GEepKspbZwR4Y2ZmYC9lkQOdxOQDD1BWcZT84u3Kg/formResponse PRE TEXT
    //https://docs.google.com/forms/u/0/d/e/1FAIpQLScPaVkllX0G8URkKw4p3lNFybWaHSEvMOUdIshQizf-EPkVgg/formResponse PRE IDN

    [Header("Pre-Questions")]
    [SerializeField] private List<TMP_Dropdown> preQuestionsAnswers;
    [SerializeField] private List<string> entryIDPreText;
    [SerializeField] private List<string> entryIDPreIDN;
    [SerializeField] private GameObject ErrorMessageTMP;
    [SerializeField] private List<string> formURLs;

    [Header("Config")]
    [SerializeField] private int textScene;
    [SerializeField] private int IDNScene;


    private string uniquePlayerID;
    private string uniquePlayerIDEntry;
    private List<string> answers;
    private int URLIndex;


    private void Start()
    {
        uniquePlayerID = SystemInfo.deviceUniqueIdentifier;
    }

    private List<string> GetValues()
    {

        answers = new List<string>();

         foreach (TMP_Dropdown dropDownValue in preQuestionsAnswers)
        {
            answers.Add(dropDownValue.value.ToString());
        }

        return answers;
    }

    private bool CheckValues()
    {
        foreach(string answer in answers)
        {
            if (answer == "0") return false;
        }
        return true;
    }


    public void SubmitForm()
    {
        GetValues();
        bool isOk = CheckValues();

        if (isOk)
        {
            StartCoroutine(Post(answers));
            ErrorMessageTMP.SetActive(false);
        }
        else
        {
            ErrorMessageTMP.SetActive(true);
        }

    }


    private IEnumerator Post(List<string> answers)
    {

        WWWForm form = new WWWForm();

        bool isPreTextPostIDNPost = StartOptionManager.Instance.GetIsPreTextPostIDNPost();
        bool isPreIDNPostTextPost = StartOptionManager.Instance.GetIsPreIDNPostTextPost();

        if (isPreTextPostIDNPost)
        {
            uniquePlayerIDEntry = "entry.1054966409";
            form.AddField("entry.1799841438", "Doing the Pre_Text_Post_IDN_Post");
        }
        if (isPreIDNPostTextPost)
        {
            uniquePlayerIDEntry = "entry.1054966409";
            form.AddField("entry.1215758962", "Doing the Pre_IDN_Post_Text_Post");
        }

        form.AddField(uniquePlayerIDEntry, uniquePlayerID);

        int index = 0;
        foreach (string answer in answers)
        {

            if (isPreTextPostIDNPost)
            {
                form.AddField(entryIDPreText[index], answer);
            }
            else if (isPreIDNPostTextPost)
            {
                form.AddField(entryIDPreIDN[index], answer);
            }

            index++;
        }


        if (isPreTextPostIDNPost) URLIndex = 0;
        if (isPreIDNPostTextPost) URLIndex = 1;

        using (UnityWebRequest www = UnityWebRequest.Post(formURLs[URLIndex], form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Feedback Sent Successfully!");
            }
            else
            {
                Debug.LogWarning($"Error in sending feedback! {www.error}");
            }
        }

        if (isPreTextPostIDNPost) SceneManager.LoadScene(textScene);
        if (isPreIDNPostTextPost) SceneManager.LoadScene(IDNScene);

    }
}
