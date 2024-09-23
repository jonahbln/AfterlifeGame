using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    int score = 0;
    TextMeshProUGUI scoreboard;
    void Start()
    {
        scoreboard = transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        scoreboard.text = "Score: 0";

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void addScore(int s)
    {
        score += s;
        scoreboard.text = "Score: " + score;
    }
}
