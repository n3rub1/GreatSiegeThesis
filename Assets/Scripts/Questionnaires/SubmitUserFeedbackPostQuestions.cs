using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class SubmitUserFeedbackPostQuestions : MonoBehaviour
{
    //https://docs.google.com/forms/u/0/d/e/1FAIpQLSeIDQxahlReuWeHRKLdTWfm45erEHIg-HDQTvCJelzzI9485A/formResponse PRE TEXT
    //https://docs.google.com/forms/u/0/d/e/1FAIpQLSeTBy5uPUnghRz4eeVMwe7qAoZW05Y5FgpZGbZwI7ZI6TPM9A/formResponse PRE IDN
    //https://docs.google.com/forms/u/0/d/e/1FAIpQLScG9zmhozg5RoJJ318fCacdMPa9o-cnCA1CqIi6VJekTQxBag/formResponse POST IDN
    //https://docs.google.com/forms/u/0/d/e/1FAIpQLSeY6Y_EEAnTT_wPEjHEoOSa-lQoVPS9GQfxG5WAXyOQ5Z1x8Q/formResponse POST TEXT



    [Header("Post-Questions")]
    [SerializeField] private List<TMP_Dropdown> postQuestionsAnswers;
    [SerializeField] private List<string> entryIDPostText;
    [SerializeField] private List<string> entryIDPostIDN;
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

        foreach (TMP_Dropdown dropDownValue in postQuestionsAnswers)
        {
            answers.Add(dropDownValue.value.ToString());
        }

        return answers;
    }

    public void SubmitForm()
    {
        GetValues();
        StartCoroutine(Post(answers));
    }


    private IEnumerator Post(List<string> answers)
    {

        WWWForm form = new WWWForm();

        bool isPreTextPostIDNPost = StartOptionManager.Instance.GetIsPreTextPostIDNPost();
        bool isPreIDNPostTextPost = StartOptionManager.Instance.GetIsPreIDNPostTextPost();

        if (isPreTextPostIDNPost)
        {
            uniquePlayerIDEntry = "entry.1054966409";
            form.AddField("entry.1814235898", "Doing the Pre_Text_Post_IDN_Post");
        }
        if (isPreIDNPostTextPost)
        {
            uniquePlayerIDEntry = "entry.1054966409";
            form.AddField("entry.968983527", "Doing the Pre_IDN_Post_Text_Post");
        }

        form.AddField(uniquePlayerIDEntry, uniquePlayerID);

        int index = 0;
        foreach (string answer in answers)
        {

            if (isPreTextPostIDNPost)
            {
                form.AddField(entryIDPostText[index], answer);
            }
            else if (isPreIDNPostTextPost)
            {
                form.AddField(entryIDPostIDN[index], answer);
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
