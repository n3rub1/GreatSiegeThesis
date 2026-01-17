using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ReadingManager : MonoBehaviour
{

    public TextMeshProUGUI titleTMP;
    public TextMeshProUGUI mainTextTMP;
    public GameObject nextButton;
    public GameObject backButton;
    public GameObject endStoryButton;

    [TextArea] public List<string> title;
    [TextArea] public List<string> mainText;

    private int index;
    private int mainMenuScene = 0;
    private int postSurveyScene = 4;

    private void Start()
    {
        index = 0;
        titleTMP.text = title[index];
        mainTextTMP.text = mainText[index];
        backButton.SetActive(false);
        endStoryButton.SetActive(false);
    }

    public void MoveToNextText()
    {

        if(index < mainText.Count-1)
        {
            index++;
            titleTMP.text = title[index];
            mainTextTMP.text = mainText[index];
            CheckButtons();
        }
    }

    public void MoveToPreviousText()
    {
        if(index > 0)
        {
            index--;
            titleTMP.text = title[index];
            mainTextTMP.text = mainText[index];
            CheckButtons();
        }
    }

    public void EndStory()
    {
        SceneManager.LoadScene(postSurveyScene);
    }

    public void CheckButtons()
    {
        if(index == mainText.Count-1)
        {
            nextButton.SetActive(false);
            endStoryButton.SetActive(true);
        }
        else
        {
            nextButton.SetActive(true);
            endStoryButton.SetActive(false);
        }

        if(index == 0)
        {
            backButton.SetActive(false);
        }
        else
        {
            backButton.SetActive(true);
        }
    }

}
