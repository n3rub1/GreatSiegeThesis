using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class SubmitUserFeedbackPostFinalQuestions : MonoBehaviour
{

    //https://docs.google.com/forms/u/0/d/e/1FAIpQLSdIw7YY3bxKevBrRC9hhlxt6BIy5bPO_GujGRudYosNVsnn2w/formResponse FINAL POST CONTROL NO PRE
    //https://docs.google.com/forms/u/0/d/e/1FAIpQLSeHD_hwnIpCvjLDJLwGwIDJz2xE2D3RzLsAz4f6TkoaY6HsPQ/formResponse FINAL POST IDN NO PRE

    //https://docs.google.com/forms/u/0/d/e/1FAIpQLSerJYECV2-nw207omV74J4a4YKqD-cegsP_G8LJkYbjbT6OqA/formResponse FINAL POST CONTROL WITH PRE
    //https://docs.google.com/forms/u/0/d/e/1FAIpQLSdB_3AQ1EwbpAXVfXBH2t_rL5B_UCpPi35EBvtfYahXGBZnQA/formResponse FINAL POST IDN WITH PRE

    [Header("Post-Questions")]
    [SerializeField] private List<TMP_Dropdown> postQuestionsAnswers;
    [SerializeField] private List<TMP_InputField> postQuestionsLongAnswers;
    [SerializeField] private List<string> entryIDPostTextNoPre;
    [SerializeField] private List<string> entryIDPostIDNNoPre;
    [SerializeField] private List<string> entryIDPostTextWithPre;
    [SerializeField] private List<string> entryIDPostIDNWithPre;
    [SerializeField] private GameObject ErrorMessageTMP;
    [SerializeField] private List<string> formURLs;

    [Header("Config")]
    [SerializeField] private int thankyouScene;


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

         foreach (TMP_InputField longAnswers in postQuestionsLongAnswers)
        {
            answers.Add(longAnswers.text.ToString());
        }

        return answers;
    }

    private bool CheckValues()
    {
        foreach(string answer in answers)
        {
            if (answer == "0" || answer == "") return false;
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

        bool isTextPostIDNPost = StartOptionManager.Instance.GetIsTextPostIDNPost();
        bool isIDNPostTextPost = StartOptionManager.Instance.GetIsIDNPostTextPost();
        bool isPreTextPostIDNPost = StartOptionManager.Instance.GetIsPreTextPostIDNPost();
        bool isPreIDNPostTextPost = StartOptionManager.Instance.GetIsPreIDNPostTextPost();

        //final post control with pre
        //entry.1054966409  unique id
        //entry.1774621622 doing...

        //mid post idn with pre
        //entry.1054966409 unique id
        //entry.1774621622 doing....

        //mid post text without pre
        //entry.1054966409 unique id
        //entry.1774621622 doing...

        //mid post idn without PRE
        //entry.1054966409 unique id
        //entry.1774621622 doing....

        if (isTextPostIDNPost)
        {
            uniquePlayerIDEntry = "entry.1054966409";
            form.AddField("entry.1774621622", "Doing the Text_Post_IDN_Post_NO PRE");
        }

        if (isIDNPostTextPost)
        {
            uniquePlayerIDEntry = "entry.1054966409";
            form.AddField("entry.1774621622", "Doing the IDN_Post_Text_Post_NO PRE");
        }

        if (isPreTextPostIDNPost)
        {
            uniquePlayerIDEntry = "entry.1054966409";
            form.AddField("entry.1774621622", "Doing the Pre_Text_Post_IDN_Post");
        }

        if (isPreIDNPostTextPost)
        {
            uniquePlayerIDEntry = "entry.1054966409";
            form.AddField("entry.1774621622", "Doing the Pre_IDN_Post_Text_Post");
        }

        form.AddField(uniquePlayerIDEntry, uniquePlayerID);

        int index = 0;
        foreach (string answer in answers)
        {

            if (isTextPostIDNPost)
            {
                form.AddField(entryIDPostTextNoPre[index], answer);
            }
            else if (isIDNPostTextPost)
            {
                form.AddField(entryIDPostIDNNoPre[index], answer);
            }
            else if (isPreTextPostIDNPost)
            {
                form.AddField(entryIDPostTextWithPre[index], answer);
            }
            else if (isPreIDNPostTextPost)
            {
                form.AddField(entryIDPostIDNWithPre[index], answer);
            }

            index++;
        }

        //https://docs.google.com/forms/u/0/d/e/1FAIpQLSdIw7YY3bxKevBrRC9hhlxt6BIy5bPO_GujGRudYosNVsnn2w/formResponse FINAL POST CONTROL NO PRE
        //https://docs.google.com/forms/u/0/d/e/1FAIpQLSeHD_hwnIpCvjLDJLwGwIDJz2xE2D3RzLsAz4f6TkoaY6HsPQ/formResponse FINAL POST IDN NO PRE

        //https://docs.google.com/forms/u/0/d/e/1FAIpQLSerJYECV2-nw207omV74J4a4YKqD-cegsP_G8LJkYbjbT6OqA/formResponse FINAL POST CONTROL WITH PRE
        //https://docs.google.com/forms/u/0/d/e/1FAIpQLSdB_3AQ1EwbpAXVfXBH2t_rL5B_UCpPi35EBvtfYahXGBZnQA/formResponse FINAL POST IDN WITH PRE


        if (isTextPostIDNPost) URLIndex = 1;
        if (isIDNPostTextPost) URLIndex = 0;
        if (isPreTextPostIDNPost) URLIndex = 3;
        if (isPreIDNPostTextPost) URLIndex = 2;

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

        if (isTextPostIDNPost)
        {
            StartOptionManager.Instance.SetIsTextPostIDNPost(true);
            SceneManager.LoadScene(thankyouScene);
        }
        if (isIDNPostTextPost)
        {
            StartOptionManager.Instance.SetisIDNPostTextPost_MidPostReady(true);
            SceneManager.LoadScene(thankyouScene);
        }
        if (isPreTextPostIDNPost)
        {
            StartOptionManager.Instance.SetisPreTextPostIDNPost_MidPostReady(true);
            SceneManager.LoadScene(thankyouScene);
        }
        if (isPreIDNPostTextPost)
        {
            StartOptionManager.Instance.SetisPreIDNPostTextPost_MidPostReady(true);
            SceneManager.LoadScene(thankyouScene);
        }

    }
}
