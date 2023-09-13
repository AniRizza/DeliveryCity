using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class TimeController : MonoBehaviour
{
    [SerializeField]
    private float startHour;
    [SerializeField]
    private float endHour;
    [SerializeField]
    private Light sunLightSource;
    [SerializeField]
    private GameObject timelinIndicator;
    [SerializeField]
    private float sunriseHour = 7;
    [SerializeField]
    private float sunsetHour = 19;
    private TimeSpan sunriseTime;
    private TimeSpan sunsetTime;
    private TimeSpan startTime;
    private TimeSpan endTime;
    private TimeSpan shiftLength;
    private DateTime currentTime;
    private Vector3 timelineIndicatorStartPosition;

    public static bool isDay;
    public UnityEvent endOfDayEvent;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = DateTime.Now.Date + TimeSpan.FromHours(startHour);
        sunriseTime = TimeSpan.FromHours(sunriseHour);
        sunsetTime = TimeSpan.FromHours(sunsetHour);
        startTime = TimeSpan.FromHours(startHour);
        endTime = TimeSpan.FromHours(endHour);
        shiftLength = CalculateTimeDifference(startTime, endTime);
        timelineIndicatorStartPosition = timelinIndicator.transform.position;

        if (startHour < sunriseHour) {
            isDay = false;
        }
        else isDay = true;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimeOfDay();
        RotateSun();
        PlaceTimelineMarker();
        TurnTheLightswitchWhenSunset();
        if (currentTime.TimeOfDay >= endTime) endOfDayEvent.Invoke();
    }

    private void RotateSun() {
        float sunLightRotation;
        if (currentTime.TimeOfDay > sunriseTime && currentTime.TimeOfDay < sunsetTime) {
            TimeSpan sunriseToSunsetDuration = CalculateTimeDifference(sunriseTime, sunsetTime);
            TimeSpan timeSinceSunrise = CalculateTimeDifference(sunriseTime, currentTime.TimeOfDay);
            double percentage = timeSinceSunrise.TotalMinutes / sunriseToSunsetDuration.TotalMinutes;
            sunLightRotation = Mathf.Lerp(0, 180, (float) percentage);
        }
        else {
            TimeSpan sunsetToSunriseDuration = CalculateTimeDifference(sunsetTime, sunriseTime);
            TimeSpan timeSinceSunset = CalculateTimeDifference(sunsetTime, currentTime.TimeOfDay);
            double percentage = timeSinceSunset.TotalMinutes / sunsetToSunriseDuration.TotalMinutes;
            sunLightRotation = Mathf.Lerp(180, 360, (float) percentage);
        }
        sunLightSource.transform.rotation = Quaternion.Euler(sunLightRotation, 90f, 0f);
    }

    private void PlaceTimelineMarker() {
        float currentTimelinIndicatorPosition;
        if (currentTime.TimeOfDay < endTime) {
            TimeSpan timeSinceShiftStart = CalculateTimeDifference(startTime, currentTime.TimeOfDay);
            double percentage = timeSinceShiftStart.TotalMinutes / shiftLength.TotalMinutes;
            currentTimelinIndicatorPosition = Mathf.Lerp(timelineIndicatorStartPosition.x, (float) Screen.width - timelineIndicatorStartPosition.x, (float) percentage);
        }
        else {
            currentTimelinIndicatorPosition = (float) Screen.width - timelineIndicatorStartPosition.x;
        }
        timelinIndicator.transform.position = new Vector3(currentTimelinIndicatorPosition, timelineIndicatorStartPosition.y, timelineIndicatorStartPosition.z);
    }

    private void TurnTheLightswitchWhenSunset() {
        if ((currentTime.TimeOfDay > sunriseTime && currentTime.TimeOfDay < sunsetTime && !isDay) || 
            ((currentTime.TimeOfDay < sunriseTime || currentTime.TimeOfDay > sunsetTime) && isDay)) {
            isDay = !isDay;
        }
    }

    private TimeSpan CalculateTimeDifference (TimeSpan fromTime, TimeSpan toTime) {
        TimeSpan difference = toTime - fromTime;
        if (difference. TotalSeconds < 0) {
            difference += TimeSpan. FromHours(24);
        }
        return difference;
    }

    private void UpdateTimeOfDay() {
        currentTime = currentTime.AddSeconds(Time.deltaTime * 500);
    }
}
