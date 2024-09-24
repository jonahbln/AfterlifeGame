using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    float score = 0;
    TextMeshProUGUI scoreboard;
    TextMeshProUGUI subscoreboard;
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
        scoreboard.text = "Score: " + score;
        subscoreboard.text = desc + " + " + s;
    }
}
