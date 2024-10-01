using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    float score = 0;
    [SerializeField] float winScore = 50;
    [SerializeField] float loseScore = -10;
    [SerializeField] float loseTime = 15;
    TextMeshProUGUI scoreboard;
    TextMeshProUGUI subscoreboard;
    public bool winLossTrigger = false;
    void Start()
    {
        scoreboard = transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        subscoreboard = scoreboard.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        scoreboard.text = "Score: 0";
        subscoreboard.text = "";
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void addScore(float s, string desc)
    {
        score += s;
        if (score >= winScore)
        {
            subscoreboard.text = "You Win!!!";
            scoreboard.text = "Score: " + score;
            Win();
        }
        else if (score <= loseScore || Time.time >= loseTime)
        {
            subscoreboard.text = "You Lose!!!";
            scoreboard.text = "Score: " + score;
            Lose();
        }
        else
        {
            scoreboard.text = "Score: " + score;
            subscoreboard.text = desc + " + " + s;
        }
    }

    public void Win()
    {
        Time.timeScale = 0.0f;
        winLossTrigger = true;
    }

    public void Lose()
    {
        Time.timeScale = 0.0f;
        winLossTrigger = true;
    }

    public void TimeFlow()
    {
        Time.timeScale = 1.0f;
    }

    public void TimeFlow(float scale, float duration)
    {
        Time.timeScale = scale;
        Invoke("TimeFlow", duration);
    }
}
