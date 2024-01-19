using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Leveling : MonoBehaviour
{
    public float maxExperience = 79;
    public int level = 1;
    public float currentExperience = 0;

    public TextMeshProUGUI levelText;
    public Image expBar;

    private GameObject player;

    void Start()
    {
        UpdateExpBar();
        levelText.text = "Level:" + level.ToString();

        player = GameObject.FindGameObjectWithTag("Player");
    }

    // void Update()
    // {
    //     levelText.text = "Level: " + level.ToString();
    // }

    public void GainExperience(int experience)
    {
        currentExperience += experience;

        if (currentExperience >= maxExperience)
        {
            LevelUp();
            UpdateExpBar();
        }
        UpdateExpBar();
    }

    void LevelUp()
    {
        GameObject.FindGameObjectWithTag("Level").GetComponent<AudioSource>().Play();
        level++;
        float tempExp = currentExperience - maxExperience;
        currentExperience = tempExp;
        maxExperience = CalculateNextLevelExperience();
        levelText.text = "Level:" + level.ToString();

        player.SendMessage("RemoveAndApplyUpgrades");
    }

    int CalculateNextLevelExperience()
    {
        double nextLevelExp = Math.Round(Math.Pow(4 * (level + 1), 2.1)) - Math.Round(Math.Pow(4 * level, 2.1));
        return Convert.ToInt32(nextLevelExp);
    }

    void UpdateExpBar()
    {
        expBar.fillAmount = currentExperience / maxExperience;
    }
}