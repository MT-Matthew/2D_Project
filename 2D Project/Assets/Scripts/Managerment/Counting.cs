using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Counting : MonoBehaviour
{
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI eliminatedText;

    public TextMeshProUGUI score;
    public TextMeshProUGUI coin;

    public int goldCount = 0;
    public int eliminatedCount = 0;

    public int miniBoss = 0;
    public int Boss = 0;
    void Update()
    {
        goldText.text = goldCount.ToString();
        eliminatedText.text = eliminatedCount.ToString();
    }

    public void CalculateScore()
    {
        int level = GameObject.FindGameObjectWithTag("Manager").GetComponent<Leveling>().level;
        int minutes = GameObject.FindGameObjectWithTag("Manager").GetComponent<Timer>().minuteTimer;
        int seconds = GameObject.FindGameObjectWithTag("Manager").GetComponent<Timer>().secondTimer;

        int playerScore = (eliminatedCount * 75) + (miniBoss * 428) + (Boss * 2251) + (Math.Max(0, minutes - 30) * 20126) + (Math.Min(59, minutes) * 126) + (Math.Min(59, seconds) * 10) + (level * 1219);

        score.text = "Score: " + playerScore.ToString();
        coin.text = "Coin Gained: " + goldCount.ToString();
    }

}
