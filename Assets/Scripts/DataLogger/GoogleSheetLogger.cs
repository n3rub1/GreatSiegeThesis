//using UnityEngine;
//using UnityEngine.Networking;
//using System.Collections;

//public class GoogleSheetLogger : Singleton<GoogleSheetLogger>
//{
//    public static GoogleSheetLogger Instance { get; private set; }
//    private string googleSheetURL = "https://script.google.com/macros/s/AKfycby7Y8cbdM4PoKl9ol84w6xZ1htcoyBDEqyhq4tMvP1puK7oVAvdaRheF3cvIMZFdQfW/exec";


//    private void Awake()
//    {
//        base.Awake();
//        Instance = this;
//    }

//    // Send whatever data you want (missing fields = blank in sheet)
//    public void LogData(string playerName, string time, int dayNumber, string additionalDetailsTitle, string additonalDetails)
//    {
//        StartCoroutine(SendData(playerName, time, dayNumber, additionalDetailsTitle, additonalDetails));
//    }

//    IEnumerator SendData(string playerName, string time, int dayNumber, string additionalDetailsTitle, string additonalDetails)
//    {
//        WWWForm form = new WWWForm();
//        form.AddField("playerName", playerName);
//        form.AddField("time", time.ToString());
//        form.AddField("dayNumber", dayNumber.ToString());
//        form.AddField("additionalDetailsTitle", additionalDetailsTitle);
//        form.AddField("additionalDetails", additonalDetails);

//        using (UnityWebRequest www = UnityWebRequest.Post(googleSheetURL, form))
//        {
//            yield return www.SendWebRequest();

//            if (www.result != UnityWebRequest.Result.Success)
//                Debug.LogError("Error: " + www.error);
//            else
//                Debug.Log("Data sent successfully!");
//        }
//    }
//}


//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.IO;
//using UnityEngine;
//using UnityEngine.Networking;

//[Serializable]
//public class LogEvent
//{
//    public int actionNumber;              // per day counter (or per logger counter)
//    public string timestampUtc;           // ISO 8601
//    public int dayNumber;
//    public string additionalDetailsTitle;
//    public string additionalDetails;
//}

//[Serializable]
//public class DayLogPayload
//{
//    public string playerId;               // deviceUniqueIdentifier
//    public string sessionId;              // per app launch
//    public string playthroughId;          // per New Game
//    public int dayNumber;
//    public List<LogEvent> events = new List<LogEvent>();
//}

//public class GoogleSheetLogger : MonoBehaviour
//{
//    public static GoogleSheetLogger I { get; private set; }

//    [SerializeField]
//    private string googleSheetURL =
//        "https://script.google.com/macros/s/AKfycby7Y8cbdM4PoKl9ol84w6xZ1htcoyBDEqyhq4tMvP1puK7oVAvdaRheF3cvIMZFdQfW/exec";


//    private DayLogPayload _current;
//    private int _counter;

//    private string _playerId;
//    private string _sessionId;
//    private string _playthroughId;

//    private string PendingDir => Path.Combine(Application.persistentDataPath, "PendingLogs");

//    void Awake()
//    {
//        if (I != null) { Destroy(gameObject); return; }
//        I = this;
//        DontDestroyOnLoad(gameObject);

//        Directory.CreateDirectory(PendingDir);

//        // Per device/player
//        _playerId = SystemInfo.deviceUniqueIdentifier;

//        // Per app launch
//        _sessionId = Guid.NewGuid().ToString("N");
//    }

//    /// Call this when the player starts a NEW RUN (New Game).
//    public void StartNewPlaythrough()
//    {
//        _playthroughId = Guid.NewGuid().ToString("N");
//    }

//    /// Call this at the start of each in-game day.
//    public void StartDay(int dayNumber)
//    {
//        _counter = 0;

//        _current = new DayLogPayload();
//        _current.playerId = _playerId;
//        _current.sessionId = _sessionId;
//        _current.playthroughId = _playthroughId;
//        _current.dayNumber = dayNumber;
//        _current.events = new List<LogEvent>();
//    }

//    /// Log events throughout the day (NO network).
//    public void Log(int dayNumber, string title, string details)
//    {
//        if (I == null) { Debug.LogError("Logger I is null"); return; }
//        Debug.Log($"[LOGGER] Log called from {title} day={dayNumber} playthrough={_playthroughId}");

//        if (_current == null || _current.dayNumber != dayNumber)
//            StartDay(dayNumber);

//        _counter++;

//        var ev = new LogEvent();
//        ev.actionNumber = _counter;
//        ev.timestampUtc = DateTime.UtcNow.ToString("o");
//        ev.dayNumber = dayNumber;
//        ev.additionalDetailsTitle = title;
//        ev.additionalDetails = details;

//        _current.events.Add(ev);
//    }

//    /// Call once at end of day.
//    public void FlushDay()
//    {
//        if (_current == null || _current.events.Count == 0) return;
//        StartCoroutine(SendDayPayload(_current));
//    }

//    /// Optional: call on startup to resend failed batches.
//    public void RetryPending()
//    {
//        StartCoroutine(RetryPendingRoutine());
//    }

//    private IEnumerator SendDayPayload(DayLogPayload payload)
//    {
//        string json = JsonUtility.ToJson(payload);

//        WWWForm form = new WWWForm();
//        form.AddField("type", "dayBatch");
//        form.AddField("payload", json);

//        using (UnityWebRequest www = UnityWebRequest.Post(googleSheetURL, form))
//        {
//            yield return www.SendWebRequest();

//            if (www.result != UnityWebRequest.Result.Success)
//            {
//                Debug.LogError("Batch send failed: " + www.error);
//                SavePending(json, payload.dayNumber, payload.playthroughId);
//            }
//            else
//            {
//                Debug.Log("Batch sent: " + payload.events.Count + " events");
//                payload.events.Clear();
//            }
//        }
//    }

//    private void SavePending(string json, int dayNumber, string playthroughId)
//    {
//        Directory.CreateDirectory(PendingDir);
//        string file = $"pending_{dayNumber}_{playthroughId}_{DateTime.UtcNow.Ticks}.json";
//        File.WriteAllText(Path.Combine(PendingDir, file), json);
//    }

//    private IEnumerator RetryPendingRoutine()
//    {
//        Directory.CreateDirectory(PendingDir);
//        string[] files = Directory.GetFiles(PendingDir, "*.json");

//        for (int i = 0; i < files.Length; i++)
//        {
//            string path = files[i];
//            string json = File.ReadAllText(path);

//            WWWForm form = new WWWForm();
//            form.AddField("type", "dayBatch");
//            form.AddField("payload", json);

//            using (UnityWebRequest www = UnityWebRequest.Post(googleSheetURL, form))
//            {
//                yield return www.SendWebRequest();

//                if (www.result == UnityWebRequest.Result.Success)
//                    File.Delete(path);
//                else
//                    yield break; // stop retrying to avoid spamming
//            }
//        }
//    }
//}


using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

[Serializable]
public class LogEvent
{
    public int actionNumber;
    public string timestampUtc;     // ISO 8601
    public int dayNumber;
    public string additionalDetailsTitle;
    public string additionalDetails;
}

[Serializable]
public class DayLogPayload
{
    public string playerId;
    public string sessionId;
    public string playthroughId;
    public int dayNumber;
    public List<LogEvent> events = new List<LogEvent>();
}

public class GoogleSheetLogger : MonoBehaviour
{
    public static GoogleSheetLogger I { get; private set; }

    [SerializeField]
    private string googleSheetURL =
        "https://script.google.com/macros/s/AKfycby7Y8cbdM4PoKl9ol84w6xZ1htcoyBDEqyhq4tMvP1puK7oVAvdaRheF3cvIMZFdQfW/exec";

    [Header("Debug")]
    [SerializeField] private bool verboseDebug = true;

    private DayLogPayload _current;
    private int _counter;

    private string _playerId;
    private string _sessionId;
    private string _playthroughId;

    private bool _isTransitioningDay; // blocks “late logs” during end-of-day

    private string PendingDir => Path.Combine(Application.persistentDataPath, "PendingLogs");

    void Awake()
    {
        if (I != null) { Destroy(gameObject); return; }
        I = this;
        DontDestroyOnLoad(gameObject);

        Directory.CreateDirectory(PendingDir);

        _playerId = SystemInfo.deviceUniqueIdentifier;
        _sessionId = Guid.NewGuid().ToString("N");
        _playthroughId = Guid.NewGuid().ToString("N");

        //if (verboseDebug)
        //    Debug.Log($"[LOGGER Awake] instance={GetInstanceID()} player={_playerId} session={_sessionId} playthrough={_playthroughId}");
    }

    /// Call on New Game.
    public void StartNewPlaythrough()
    {
        _playthroughId = Guid.NewGuid().ToString("N");

        //if (verboseDebug)
        //    Debug.Log($"[LOGGER StartNewPlaythrough] playthrough={_playthroughId}");
    }

    /// Call once when a new in-game day begins (BEFORE gameplay logs happen).
    public void BeginDay(int dayNumber)
    {
        _isTransitioningDay = false;
        _counter = 0;

        _current = new DayLogPayload
        {
            playerId = _playerId,
            sessionId = _sessionId,
            playthroughId = _playthroughId,
            dayNumber = dayNumber,
            events = new List<LogEvent>()
        };

        //if (verboseDebug)
        //    Debug.Log($"[LOGGER BeginDay] day={dayNumber} instance={GetInstanceID()}");
    }

    /// Call once when the day is ending, BEFORE you increment the day number.
    /// This blocks late logs from slipping into the wrong day.
    public void BeginEndOfDayTransition()
    {
        _isTransitioningDay = true;

        //if (verboseDebug)
        //    Debug.Log($"[LOGGER BeginEndOfDayTransition] currentDay={_current?.dayNumber.ToString() ?? "null"} events={_current?.events.Count.ToString() ?? "null"}");
    }

    /// Call from anywhere. No dayNumber argument.
    public void Log(string title, string details)
    {
        if (_isTransitioningDay)
        {
            // This is intentional: if logs are still firing during sleep/scene transitions,
            // they are the ones that were messing up your day batches.
            //if (verboseDebug)
            //    Debug.LogWarning($"[LOGGER Log BLOCKED] End-of-day transition active. title={title}");
            return;
        }

        if (_current == null)
        {
            //Debug.LogError($"[LOGGER Log ERROR] No active day. You forgot to call BeginDay(dayNumber). title={title}");
            return;
        }

        _counter++;

        var ev = new LogEvent
        {
            actionNumber = _counter,
            timestampUtc = DateTime.UtcNow.ToString("o"),
            dayNumber = _current.dayNumber,
            additionalDetailsTitle = title,
            additionalDetails = details
        };

        _current.events.Add(ev);

        //if (verboseDebug)
        //    Debug.Log($"[LOGGER Log] day={_current.dayNumber} count={_current.events.Count} action={_counter} title={title}");
    }

    /// Call once at end of day (still before incrementing day).
    public void FlushDay()
    {
        if (_current == null || _current.events.Count == 0)
        {
            //if (verboseDebug)
            //    Debug.Log($"[LOGGER FlushDay] nothing to send. day={_current?.dayNumber.ToString() ?? "null"}");
            return;
        }

        //if (verboseDebug)
        //    Debug.Log($"[LOGGER FlushDay] sending day={_current.dayNumber} events={_current.events.Count}");

        StartCoroutine(SendDayPayload(_current));

        // Close this buffer immediately so you can't accidentally keep writing to it.
        _current = null;
        _counter = 0;
    }

    public void RetryPending()
    {
        StartCoroutine(RetryPendingRoutine());
    }

    private IEnumerator SendDayPayload(DayLogPayload payload)
    {
        string json = JsonUtility.ToJson(payload);

        WWWForm form = new WWWForm();
        form.AddField("type", "dayBatch");
        form.AddField("payload", json);

        using (UnityWebRequest www = UnityWebRequest.Post(googleSheetURL, form))
        {
            yield return www.SendWebRequest();

            string responseText = (www.downloadHandler != null) ? www.downloadHandler.text : "(no body)";

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"[LOGGER SendDayPayload] FAILED error={www.error} response={responseText}");
                SavePending(json, payload.dayNumber, payload.playthroughId);
            }
            else
            {
                Debug.Log($"[LOGGER SendDayPayload] SUCCESS day={payload.dayNumber} events={payload.events.Count} response={responseText}");
            }
        }
    }

    private void SavePending(string json, int dayNumber, string playthroughId)
    {
        Directory.CreateDirectory(PendingDir);
        string file = $"pending_{dayNumber}_{playthroughId}_{DateTime.UtcNow.Ticks}.json";
        File.WriteAllText(Path.Combine(PendingDir, file), json);
    }

    private IEnumerator RetryPendingRoutine()
    {
        Directory.CreateDirectory(PendingDir);
        string[] files = Directory.GetFiles(PendingDir, "*.json");

        for (int i = 0; i < files.Length; i++)
        {
            string path = files[i];
            string json = File.ReadAllText(path);

            WWWForm form = new WWWForm();
            form.AddField("type", "dayBatch");
            form.AddField("payload", json);

            using (UnityWebRequest www = UnityWebRequest.Post(googleSheetURL, form))
            {
                yield return www.SendWebRequest();

                if (www.result == UnityWebRequest.Result.Success)
                    File.Delete(path);
                else
                    yield break;
            }
        }
    }
}
