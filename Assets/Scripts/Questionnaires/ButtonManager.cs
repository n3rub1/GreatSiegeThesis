using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ButtonManager : MonoBehaviour
{

    public GameObject nextButton;
    public GameObject backButton;
    public GameObject submitButton;
    public GameObject questionsPage1;
    public GameObject questionsPage2;
    public GameObject questionsPage3;
    public GameObject questionsPage4;
    public TMP_Dropdown postQuestion1;
    public TMP_Dropdown postQuestion2;
    public TMP_Dropdown postQuestion3;
    public TMP_Dropdown postQuestion4;
    public TMP_Dropdown postQuestion5;
    public TMP_Dropdown postQuestion6;
    public TMP_Dropdown postQuestion7;
    public TMP_Dropdown postQuestion8;
    public TMP_Dropdown postQuestion9;
    public TMP_Dropdown postQuestion10;
    public TMP_Dropdown postQuestion11;
    public TMP_Dropdown postQuestion12;
    public TMP_Dropdown postQuestion13;
    public TMP_Dropdown postQuestion14;
    public TMP_Dropdown postQuestion15;
    public TMP_Dropdown postQuestion16;
    public TMP_Dropdown postQuestion17;
    public GameObject postError;

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

            if (postQuestion1.value == 0 || postQuestion2.value == 0 || postQuestion3.value == 0 || postQuestion4.value == 0 || postQuestion5.value == 0 || postQuestion6.value == 0 ||
                postQuestion7.value == 0 || postQuestion8.value == 0)
            {
                postError.SetActive(true);
            }
            else
            {
                nextButton.SetActive(true);
                backButton.SetActive(true);
                submitButton.SetActive(false);
                questionsPage1.SetActive(false);
                questionsPage2.SetActive(true);
                questionsPage3.SetActive(false);
                questionsPage4.SetActive(false);
                postError.SetActive(false);

            }

        }

        else if (questionsPage2.activeSelf)
        {

            if (postQuestion9.value == 0 || postQuestion10.value == 0 || postQuestion11.value == 0 || postQuestion12.value == 0 || postQuestion13.value == 0 || postQuestion14.value == 0 ||
                postQuestion15.value == 0 || postQuestion16.value == 0)
            {
                postError.SetActive(true);
            }
            else
            {
                nextButton.SetActive(true);
                backButton.SetActive(true);
                submitButton.SetActive(false);
                questionsPage1.SetActive(false);
                questionsPage2.SetActive(false);
                questionsPage3.SetActive(true);
                questionsPage4.SetActive(false);
                postError.SetActive(false);
            }
        }

        else if (questionsPage3.activeSelf)
        {
            if (postQuestion17.value == 0)
            {
                postError.SetActive(true);
            }
            else
            {
                nextButton.SetActive(false);
                backButton.SetActive(true);
                submitButton.SetActive(true);
                questionsPage1.SetActive(false);
                questionsPage2.SetActive(false);
                questionsPage3.SetActive(false);
                questionsPage4.SetActive(true);
                postError.SetActive(false);
            }
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
