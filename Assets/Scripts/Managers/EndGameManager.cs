using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndGameManager : MonoBehaviour
{

    [Header("Data")]
    [SerializeField] private GameObject EndOfGameScreen;
    [SerializeField] [TextArea] private List<string> titles;
    [SerializeField] [TextArea] private List<string> descriptions;

    [SerializeField] private TextMeshProUGUI titleTMP;
    [SerializeField] private TextMeshProUGUI descriptionTMP;
    [SerializeField] private int finalPost;
    [SerializeField] private int midPost;


    public void ShowEndGame(int endingNumber)
    {
        EndOfGameScreen.SetActive(true);
        switch (endingNumber)
        {
            case 1:
                titleTMP.text = titles[0];
                descriptionTMP.text = descriptions[0];
                break;
            case 2:
                titleTMP.text = titles[1];
                descriptionTMP.text = descriptions[1];
                break;
            case 3:
                titleTMP.text = titles[2];
                descriptionTMP.text = descriptions[2];
                break;
            case 4:
                titleTMP.text = titles[3];
                descriptionTMP.text = descriptions[3];
                break;
        }
    }

    public void MoveToFinalQuestions()
    {
        bool isPreTextPostIDNPost = StartOptionManager.Instance.GetIsPreTextPostIDNPost();
        bool isPreIDNPostTextPost = StartOptionManager.Instance.GetIsPreIDNPostTextPost();
        bool isTextPostIDNPost = StartOptionManager.Instance.GetIsTextPostIDNPost();
        bool isIDNPostTextPost = StartOptionManager.Instance.GetIsIDNPostTextPost();

        bool isPreTextPostIDNPost_MidPostReady = StartOptionManager.Instance.GetisPreTextPostIDNPost_MidPostReady();
        bool isPreIDNPostTextPost_MidPostReady = StartOptionManager.Instance.GetisPreIDNPostTextPost_MidPostReady();
        bool isTextPostIDNPost_MidPostReady = StartOptionManager.Instance.GetisTextPostIDNPost_MidPostReady();
        bool isIDNPostTextPost_MidPostReady = StartOptionManager.Instance.GetisIDNPostTextPost_MidPostReady();

        if (isPreTextPostIDNPost) SceneManager.LoadScene(finalPost);
        if (isPreIDNPostTextPost) SceneManager.LoadScene(midPost);
        if (isTextPostIDNPost) SceneManager.LoadScene(finalPost);
        if (isIDNPostTextPost) SceneManager.LoadScene(midPost);
    }
}
