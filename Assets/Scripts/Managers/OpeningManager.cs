using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OpeningManager : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private TextMeshProUGUI textComponent;
    [TextArea(5, 10)] public string[] storyScreens;
    [SerializeField] public string[] storyContinueButtons;
    [SerializeField] private float delay = 0.05f;
    [SerializeField] private Button continueButton;
    [SerializeField] private TextMeshProUGUI continueButtonTMP;
    [SerializeField] private AudioSource buttonClickAudioSource;

    [Header("scenes")]
    [SerializeField] private int preSurveyQuestions = 3;
    [SerializeField] private int textScene;
    [SerializeField] private int IDNScene;

    [Header("Audio (TTS per screen)")]
    [SerializeField] private AudioSource ttsSource;
    [SerializeField] private AudioClip[] ttsClips; // Must match storyScreens length
    //[SerializeField] private bool playAudioOnScreenStart = true;

    private int currentScreen = 0;
    private bool isTyping = false;

    private void Start()
    {
        continueButton.onClick.AddListener(NextScreen);
        continueButton.gameObject.SetActive(false);
        StartCoroutine(WriteText(storyScreens[currentScreen]));
    }

    IEnumerator WriteText(string textToWrite)
    {

            ttsSource.clip = ttsClips[currentScreen];
            ttsSource.Play();


        isTyping = true;
        textComponent.text = "";

        foreach (char c in textToWrite)
        {
            textComponent.text += c;
            yield return new WaitForSeconds(delay);
        }

        isTyping = false;
        continueButtonTMP.text = storyContinueButtons[currentScreen];
        continueButton.gameObject.SetActive(true);
    }

    public void NextScreen()
    {
        buttonClickAudioSource.Play();

        if (isTyping) return;

        continueButton.gameObject.SetActive(false);

        if (currentScreen < storyScreens.Length - 1)
        {
            currentScreen++;
            StartCoroutine(WriteText(storyScreens[currentScreen]));
        }
        else
        {
            // Last screen -> Load the next scene
            Debug.Log("Loading Fort St. Elmo Scene...");
            LoadNextScene();
        }
    }

    public void SkipCurrent()
    {
        buttonClickAudioSource.Play();
        StopAllCoroutines();
        isTyping = false;
        NextScreen();
    }

    public void LoadNextScene()
    {
        bool isPreTextPostIDNPost = StartOptionManager.Instance.GetIsPreTextPostIDNPost();
        bool isPreIDNPostTextPost = StartOptionManager.Instance.GetIsPreIDNPostTextPost();
        bool isTextPostIDNPost = StartOptionManager.Instance.GetIsTextPostIDNPost();
        bool isIDNPostTextPost = StartOptionManager.Instance.GetIsIDNPostTextPost();

        if (isPreIDNPostTextPost) SceneManager.LoadScene(preSurveyQuestions);
        if (isPreTextPostIDNPost) SceneManager.LoadScene(IDNScene);
        if (isIDNPostTextPost) SceneManager.LoadScene(IDNScene);
        if (isTextPostIDNPost) SceneManager.LoadScene(IDNScene);

    }
}
