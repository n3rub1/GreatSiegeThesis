using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OpeningManager : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private TextMeshProUGUI textComponent;
    [TextArea] public string fullHistoricalText;
    [SerializeField] private float delay = 0.05f;
    [SerializeField] private Button continueButton;
    [SerializeField] private int fortStElmoScene = 2;

    private void Start()
    {
        StartCoroutine(WriteText());
        continueButton.gameObject.SetActive(false);
    }

    IEnumerator WriteText()
    {
        textComponent.text = "";
        foreach (char c in fullHistoricalText)
        {
            textComponent.text += c;
            yield return new WaitForSeconds(delay);
        }

        continueButton.gameObject.SetActive(true);
    }


    public void LoadNextScene()
    {
        SceneManager.LoadScene(fortStElmoScene);
    }
}




