using System;
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
    TextMeshProUGUI scoreboard;
    [field: NonSerialized] public bool winLossTrigger = false;
    private new AudioSource audio;
    Slider progressSlider;

    private void Awake()
    {
        Time.timeScale = 0f;
    }

    void Start()
    {
        scoreboard = transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        audio = GetComponent<AudioSource>();
        progressSlider = transform.GetChild(0).GetChild(1).GetComponent<Slider>();
        progressSlider.maxValue = winScore;
        progressSlider.minValue = loseScore;

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void StartSong()
    {
        audio.Play();
        Time.timeScale = 1f;
    }

    public void addScore(float s, string desc)
    {
        score += s;
        if (score >= winScore)
        {
            Win();
        }
        else if (score <= loseScore)
        {
            Lose();
        }
        else
        {
            scoreboard.text = desc + " + " + s;
            progressSlider.value += s;
        }

        if(score >= 0)
        {
            progressSlider.transform.GetChild(1).transform.GetChild(0).GetComponent<Image>().color = Color.green; 
        }
        else
        {
            progressSlider.transform.GetChild(1).transform.GetChild(0).GetComponent<Image>().color = Color.red;
        }
    }

    public void Win()
    {
        scoreboard.text = "You Win!!!";
        Time.timeScale = 0.0f;
        audio.Pause();
        winLossTrigger = true;
    }

    public void Lose()
    {
        scoreboard.text = "You Lose!!!";
        Time.timeScale = 0.0f;
        audio.Pause();
        winLossTrigger = true;
    }

    public void TimeFlow()
    {
        audio.pitch = 1.0f;
        Time.timeScale = 1.0f;
    }

    public void TimeFlow(float scale, float duration)
    {
        Time.timeScale = scale;
        audio.pitch = scale;
        Invoke("TimeFlow", duration);
    }
}