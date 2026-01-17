using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{

    [Header("Config")]
    [SerializeField] private GameObject[] fires;
    [SerializeField] private float loopInterval = 2.0f;
    [SerializeField] private int storyScreen = 1; //was 1
    [SerializeField] private TMP_InputField passwordInputField;
    [SerializeField] private GameObject padLock;

    [Header("Movement")]
    [SerializeField] private GameObject props;
    [SerializeField] private GameObject fireProps;
    [SerializeField] public float amplitude = 0.5f;
    [SerializeField] public float frequency = 1f;

    [Header("ButtonConfig")]
    [SerializeField] private TextMeshProUGUI creditText;
    [SerializeField] private TextMeshProUGUI helpText;
    [SerializeField] private TextMeshProUGUI startText;

    private Vector3 startPosBackground;
    private Vector3 startPosFireBackground;
    private bool isCreditsOnScreen = false;
    private bool isHelpOnScreen = false;
    private bool isBurning = false;
    private string password;
    private bool isLocked = true;

    private void Start()
    {
        startPosBackground = props.transform.position;
        startPosFireBackground = fireProps.transform.position;
        StartCoroutine(BackgroundTimedLoop());
        padLock.SetActive(true);
        startText.color = Color.gray;
    }

    private void Update()
    {
        float offset = Mathf.Sin(Time.time * frequency) * amplitude;
        props.transform.position = startPosBackground + new Vector3(offset, 0f, 0f);
        fireProps.transform.position = startPosFireBackground + new Vector3(offset, 0f, 0f);
    }

    public void LoadNextScene()
    {
        if(!isLocked) SceneManager.LoadScene(storyScreen);
    }

    public void QuitGame()
    {
        Debug.Log("Exit Game");
        Application.Quit();
    }

    public void ShowCredits()
    {
        if (!isCreditsOnScreen)
        {
            creditText.gameObject.SetActive(true);
            helpText.gameObject.SetActive(false);
            isCreditsOnScreen = true;
            isHelpOnScreen = false;
        }
        else
        {
            creditText.gameObject.SetActive(false);
            helpText.gameObject.SetActive(false);
            isCreditsOnScreen = false;
            isHelpOnScreen = false;
        }
    }

    public void ShowHelp()
    {
        if (!isHelpOnScreen)
        {
            creditText.gameObject.SetActive(false);
            helpText.gameObject.SetActive(true);
            isCreditsOnScreen = false;
            isHelpOnScreen = true;
        }
        else
        {
            creditText.gameObject.SetActive(false);
            helpText.gameObject.SetActive(false);
            isCreditsOnScreen = false;
            isHelpOnScreen = false;
        }
    }

    public void InputFieldPassword()
    {

        ReadInputField();

        if(password == "1234")
        {
            padLock.SetActive(false);
            isLocked = false;
            startText.color = Color.black;
            StartOptionManager.Instance.setIsStartIDN(true);
        }
        else if(password== "4321")
        {
            padLock.SetActive(false);
            isLocked = false;
            startText.color = Color.black;
            StartOptionManager.Instance.setIsStartIDN(false);
        }
    }

    private void ReadInputField()
    {
        password = passwordInputField.text;
    }



    IEnumerator BackgroundTimedLoop()
    {
        while (true)
        {
            foreach (GameObject fire in fires)
            {
                if (isBurning)
                {
                    fire.SetActive(false);
                    fireProps.SetActive(false);
                }
                else
                {
                    fire.SetActive(true);
                    fireProps.SetActive(true);
                }
            }

            isBurning = !isBurning;

            yield return new WaitForSeconds(loopInterval);
        }
    }
}
