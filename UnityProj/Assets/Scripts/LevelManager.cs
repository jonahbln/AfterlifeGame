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
    [SerializeField] public TextAsset spawnTimes;
    TextMeshProUGUI scoreboard;
    [field: NonSerialized] public bool winLossTrigger = false;
    public new AudioSource audio;
    private float timePassed;
    private bool songStarted;
    Slider progressSlider;

    public List<float> FlowstartTimes;
    public List<float> Flowscales;
    public List<float> Flowdurations;

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
        if (songStarted)
        {
            timePassed += Time.deltaTime;
            if (FlowstartTimes.Count > 0 && FlowstartTimes[0] <= timePassed)
            {
                TimeFlow(Flowscales[0], Flowdurations[0]);
                Flowdurations.RemoveAt(0);
                Flowscales.RemoveAt(0);
                FlowstartTimes.RemoveAt(0);
            }
        }
    }

    public void StartSong()
    {
        songStarted = true;
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

        if (score >= 0)
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