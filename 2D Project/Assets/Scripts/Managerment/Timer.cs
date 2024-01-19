using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering.Universal;


public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI timeCircleText;

    public Light2D playerLight;
    public Light2D globalLight;

    public Image lightBeam;

    public float transitionDuration = 4.0f;
    public bool isNight = false;

    public float a;
    public float b;

    public int minuteTimer;
    public int secondTimer;
    public int hourTimer;

    private int minutesPast30;
    private int hourPast1;

    private float currentTime = 0;

    public float tick;
    public float seconds;
    public int minutes;
    public int hours = 6;
    public int days = 1;

    void Start()
    {
        UpdateTimerText();
    }

    void FixedUpdate()
    {
        CalculateTime();
        DisplayTime();
    }


    void Update()
    {
        currentTime += Time.deltaTime;
        UpdateTimerText();

        a = minuteTimer - 23 + (37 * hourTimer);
        b = minutesPast30 + (60 * hourPast1);

    }

    void UpdateTimerText()
    {
        minuteTimer = Mathf.FloorToInt((currentTime % 3600) / 60f);
        secondTimer = Mathf.FloorToInt(currentTime % 60f);
        hourTimer = Mathf.FloorToInt(currentTime / 3600f);

        if (minuteTimer >= 30)
        {
            minutesPast30 = minuteTimer - 30;
        }

        if (hourTimer >= 1)
        {
            hourPast1 = hourTimer - 1;
        }

        timerText.text = string.Format("{0:00}:{1:00}", minuteTimer, secondTimer);
    }


    void CalculateTime()
    {
        seconds += Time.fixedDeltaTime * tick;
        if (seconds >= 60)
        {
            seconds = 0;
            minutes += 1;
        }
        if (minutes >= 60)
        {
            minutes = 0;
            hours += 1;
        }
        if (hours >= 24)
        {
            hours = 0;
            days += 1;
        }

        ChangeLightState();
    }

    void DisplayTime()
    {
        timeCircleText.text = string.Format("{0:00}:{1:00}", hours, minutes);
    }

    void ChangeLightState()
    {
        GameObject[] lightObjects = GameObject.FindGameObjectsWithTag("Lamp");

        if (hours >= 21 && hours <= 22)
        {

            // lightBeam.color = new Color(1f, 1f, 1f, 0f);
            StartCoroutine(LerpLightBeam(new Color(1f, 1f, 1f, 0f)));
            StartCoroutine(LerpLightIntensity(playerLight, 1.3f));
            StartCoroutine(LerpLightIntensity(globalLight, 0f));
            isNight = true;

            foreach (GameObject lightObject in lightObjects)
            {
                StartCoroutine(LerpLightIntensity(lightObject.GetComponent<Light2D>(), 2f));
            }
        }
        if (hours >= 5 && hours <= 6)
        {

            // lightBeam.color = new Color(1f, 1f, 1f, 60f / 255f);
            StartCoroutine(LerpLightBeam(new Color(1f, 1f, 1f, 60f / 255f)));
            StartCoroutine(LerpLightIntensity(playerLight, 0f));
            StartCoroutine(LerpLightIntensity(globalLight, 1f));
            isNight = false;

            foreach (GameObject lightObject in lightObjects)
            {
                StartCoroutine(LerpLightIntensity(lightObject.GetComponent<Light2D>(), 0f));
            }
        }
    }

    IEnumerator LerpLightIntensity(Light2D light, float targetIntensity)
    {
        float elapsedTime = 0f;
        float startIntensity = light.intensity;

        while (elapsedTime < transitionDuration)
        {
            light.intensity = Mathf.Lerp(startIntensity, targetIntensity, elapsedTime / transitionDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        light.intensity = targetIntensity;
    }

    IEnumerator LerpLightBeam(Color targetColor)
    {
        float elapsedTime = 0f;
        Color startColor = lightBeam.color;

        while (elapsedTime < transitionDuration)
        {
            lightBeam.color = Color.Lerp(startColor, targetColor, elapsedTime / (transitionDuration / 2f));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        lightBeam.color = targetColor;
    }

}

