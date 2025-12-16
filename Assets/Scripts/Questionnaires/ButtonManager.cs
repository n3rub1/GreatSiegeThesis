using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{

    public GameObject nextButton;
    public GameObject backButton;
    public GameObject submitButton;
    public GameObject questionsPage1;
    public GameObject questionsPage2;
    public GameObject questionsPage3;
    public GameObject questionsPage4;

    private void Start()
    {
        nextButton.SetActive(true);
        backButton.SetActive(false);
        submitButton.SetActive(false);
        questionsPage1.SetActive(true);
        questionsPage2.SetActive(false);
        questionsPage3.SetActive(false);
        questionsPage4.SetActive(false);
    }

    public void PageChangeForward()
    {

        if (questionsPage1.activeSelf)
        {
            nextButton.SetActive(true);
            backButton.SetActive(true);
            submitButton.SetActive(false);
            questionsPage1.SetActive(false);
            questionsPage2.SetActive(true);
            questionsPage3.SetActive(false);
            questionsPage4.SetActive(false);
        }

        else if (questionsPage2.activeSelf)
        {
            nextButton.SetActive(true);
            backButton.SetActive(true);
            submitButton.SetActive(false);
            questionsPage1.SetActive(false);
            questionsPage2.SetActive(false);
            questionsPage3.SetActive(true);
            questionsPage4.SetActive(false);
        }

        else if (questionsPage3.activeSelf)
        {
            nextButton.SetActive(false);
            backButton.SetActive(true);
            submitButton.SetActive(true);
            questionsPage1.SetActive(false);
            questionsPage2.SetActive(false);
            questionsPage3.SetActive(false);
            questionsPage4.SetActive(true);
        }

    }

    public void PageChangeBackward()
    {
        if (questionsPage2.activeSelf)
        {
            nextButton.SetActive(true);
            backButton.SetActive(false);
            submitButton.SetActive(false);
            questionsPage1.SetActive(true);
            questionsPage2.SetActive(false);
            questionsPage3.SetActive(false);
            questionsPage4.SetActive(false);
        }

        else if (questionsPage3.activeSelf)
        {
            nextButton.SetActive(true);
            backButton.SetActive(true);
            submitButton.SetActive(false);
            questionsPage1.SetActive(false);
            questionsPage2.SetActive(true);
            questionsPage3.SetActive(false);
            questionsPage4.SetActive(false);
        }

        else if (questionsPage4.activeSelf)
        {
            nextButton.SetActive(true);
            backButton.SetActive(true);
            submitButton.SetActive(false);
            questionsPage1.SetActive(false);
            questionsPage2.SetActive(false);
            questionsPage3.SetActive(true);
            questionsPage4.SetActive(false);
        }
    }


}
