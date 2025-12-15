using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class UserFeedback : MonoBehaviour
{

    [Header("Questions")]
    [SerializeField] private string uniquePlayerID;
    [SerializeField] private string likertData1Before;
    [SerializeField] private string likertData2Before;
    [SerializeField] private string openEndData3Before;
    [SerializeField] private string likertData1After;
    [SerializeField] private string likertData2After;
    [SerializeField] private string openEndData3After;

    private string formURL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLScceC0tlcOh-2VuN8f0qOkyZAJ2er0x3AQzjQEO7czEBlHu1g/formResponse";

    private void Start()
    {
        uniquePlayerID = SystemInfo.deviceUniqueIdentifier;
    }

    public void SubmitForm()
    {
        StartCoroutine(Post(uniquePlayerID, likertData1Before, likertData2Before, openEndData3Before, likertData1After, likertData2After, openEndData3After));
    }

    private IEnumerator Post(string uniquePlayerID, string likertData1Before, string likertData2Before, string openEndData3Before, string likertData1After, string likertData2After, string openEndData3After)
    {
        WWWForm form = new WWWForm();
        form.AddField("entry.1054966409", uniquePlayerID);
        form.AddField("entry.1861348342", likertData1Before);
        form.AddField("entry.356919410", likertData2Before);
        form.AddField("entry.1650738171", openEndData3Before);
        form.AddField("entry.1310830898", likertData1After);
        form.AddField("entry.1446351570", likertData2After);
        form.AddField("entry.1423793081", openEndData3After);

        using (UnityWebRequest www = UnityWebRequest.Post(formURL, form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Feedback Sent Successfully!");
            }
            else
            {
                Debug.LogWarning($"Error in sending feedback! {www.error}" );
            }
        }
    }

}
