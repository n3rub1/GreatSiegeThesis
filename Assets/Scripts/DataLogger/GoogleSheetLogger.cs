using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class GoogleSheetLogger : MonoBehaviour
{
    private string googleSheetURL = "https://script.google.com/macros/s/AKfycby7Y8cbdM4PoKl9ol84w6xZ1htcoyBDEqyhq4tMvP1puK7oVAvdaRheF3cvIMZFdQfW/exec";

    // Send whatever data you want (missing fields = blank in sheet)
    public void LogData(string playerName, string time, int dayNumber, string additionalDetailsTitle, string additonalDetails)
    {
        StartCoroutine(SendData(playerName, time, dayNumber, additionalDetailsTitle, additonalDetails));
    }

    IEnumerator SendData(string playerName, string time, int dayNumber, string additionalDetailsTitle, string additonalDetails)
    {
        WWWForm form = new WWWForm();
        form.AddField("playerName", playerName);
        form.AddField("time", time.ToString());
        form.AddField("dayNumber", dayNumber.ToString());
        form.AddField("additionalDetailsTitle", additionalDetailsTitle);
        form.AddField("additionalDetails", additonalDetails);

        using (UnityWebRequest www = UnityWebRequest.Post(googleSheetURL, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
                Debug.LogError("Error: " + www.error);
            else
                Debug.Log("Data sent successfully!");
        }
    }
}