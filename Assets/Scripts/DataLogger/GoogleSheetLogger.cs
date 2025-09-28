using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class GoogleSheetLogger : MonoBehaviour
{
    private string googleSheetURL = "https://script.google.com/macros/s/AKfycby7Y8cbdM4PoKl9ol84w6xZ1htcoyBDEqyhq4tMvP1puK7oVAvdaRheF3cvIMZFdQfW/exec";

    // Send whatever data you want (missing fields = blank in sheet)
    public void LogData(string playerName, string time, string questSelected, int dayNumber)
    {
        StartCoroutine(SendData(playerName, time, questSelected, dayNumber));
    }

    IEnumerator SendData(string playerName, string time, string questSelected, int dayNumber)
    {
        WWWForm form = new WWWForm();
        form.AddField("playerName", playerName);
        form.AddField("time", time.ToString());
        form.AddField("questSelected", questSelected.ToString());
        form.AddField("dayNumber", dayNumber.ToString());
        form.AddField("testingSequence", 0.ToString());

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