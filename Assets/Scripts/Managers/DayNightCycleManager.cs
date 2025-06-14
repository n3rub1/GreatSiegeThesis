using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering.Universal;

public class DayNightCycleManager : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private int timeOfDay = 8;
    [SerializeField] private int speedOfTimeOfDayInSeconds = 1;
    [SerializeField] private TextMeshProUGUI timeOfDayTMP;
    [SerializeField] private TextMeshProUGUI dayNumber;
    [SerializeField] private int dayNumberStart = 1;
    [SerializeField] private Light2D globalLight;
    [SerializeField] private int hourStartIndexLight = 8;

    private Color[] hourlyColors = new Color[]
    {
    new Color(11f/255f, 12f/255f, 30f/255f, 1f),  // 00:00 - #0B0C1E
    new Color(11f/255f, 12f/255f, 26f/255f, 1f),  // 01:00 - #0B0C1A
    new Color(10f/255f, 10f/255f, 24f/255f, 1f),  // 02:00 - #0A0A18
    new Color(10f/255f, 11f/255f, 25f/255f, 1f),  // 03:00 - #0A0B19
    new Color(18f/255f, 15f/255f, 32f/255f, 1f),  // 04:00 - #120F20
    new Color(31f/255f, 28f/255f, 44f/255f, 1f),  // 05:00 - #1F1C2C
    new Color(58f/255f, 48f/255f, 69f/255f, 1f),  // 06:00 - #3A3045
    new Color(110f/255f, 90f/255f, 111f/255f, 1f),// 07:00 - #6E5A6F
    new Color(178f/255f, 140f/255f, 114f/255f, 1f), // 08:00 - #B28C72
    new Color(215f/255f, 181f/255f, 94f/255f, 1f), // 09:00 - #D7B55E
    new Color(242f/255f, 210f/255f, 75f/255f, 1f), // 10:00 - #F2D24B
    new Color(249f/255f, 232f/255f, 107f/255f, 1f),// 11:00 - #F9E86B
    new Color(255f/255f, 255f/255f, 153f/255f, 1f),// 12:00 - #FFFF99
    new Color(255f/255f, 242f/255f, 160f/255f, 1f),// 13:00 - #FFF2A0
    new Color(255f/255f, 232f/255f, 140f/255f, 1f),// 14:00 - #FFE88C
    new Color(246f/255f, 214f/255f, 114f/255f, 1f),// 15:00 - #F6D672
    new Color(232f/255f, 181f/255f, 92f/255f, 1f), // 16:00 - #E8B55C
    new Color(211f/255f, 147f/255f, 88f/255f, 1f), // 17:00 - #D39358
    new Color(172f/255f, 106f/255f, 92f/255f, 1f), // 18:00 - #AC6A5C
    new Color(116f/255f, 71f/255f, 88f/255f, 1f),  // 19:00 - #744758
    new Color(63f/255f, 47f/255f, 74f/255f, 1f),   // 20:00 - #3F2F4A
    new Color(36f/255f, 31f/255f, 53f/255f, 1f),   // 21:00 - #241F35
    new Color(24f/255f, 24f/255f, 41f/255f, 1f),   // 22:00 - #181829
    new Color(15f/255f, 14f/255f, 34f/255f, 1f)    // 23:00 - #0F0E22
    };

    private Color currentColor;
    private Color targetColor;
    private float transitionProgress = 0f;
    private float transitionDuration;

    private void Start()
    {
        StartCoroutine(StartTimer());
        currentColor = hourlyColors[8];
        targetColor = hourlyColors[9];
        transitionDuration = speedOfTimeOfDayInSeconds;
    }

    private void Update()
    {
        if (transitionProgress < 1f)
        {
            transitionProgress += Time.deltaTime / transitionDuration;
            globalLight.color = Color.Lerp(currentColor, targetColor, transitionProgress);
        }
    }

    IEnumerator StartTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(speedOfTimeOfDayInSeconds);
            TriggerTimer();
        }
    }

    private void TriggerTimer()
    {
        timeOfDay++;
        timeOfDay = timeOfDay >= 24 ? 0 : timeOfDay;
        timeOfDayTMP.text = $"Time: {timeOfDay}:00";

        TriggerDayNightGlobalLight();
        UpdateDay(timeOfDay);

    }

    private void UpdateDay(int hour)
    {
        if(hour == 0)
        {
            dayNumberStart++;
        }

        dayNumber.text = $"Day: {dayNumberStart}";

    }

    private void TriggerDayNightGlobalLight()
    {
        hourStartIndexLight = hourStartIndexLight >= hourlyColors.Length ? 0 : hourStartIndexLight;

        currentColor = globalLight.color;
        targetColor = hourlyColors[hourStartIndexLight];
        transitionProgress = 0f;

        hourStartIndexLight++;
    }

}
