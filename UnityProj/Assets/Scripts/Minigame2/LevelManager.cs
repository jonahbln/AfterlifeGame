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
    [SerializeField] TextMeshProUGUI scoreboard;
    [field: NonSerialized] public bool winLossTrigger = false;
    public new AudioSource audio;
    private float timePassed;
    private bool songStarted;
    [SerializeField] Slider progressSlider;
    public float timeFlow = 0.9f;

    public List<float> FlowstartTimes;
    public List<float> Flowscales;
    public List<float> Flowdurations;

    private void Awake()
    {
        Time.timeScale = 0f;
    }

    void Start()
    {
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
            progressSlider.value -= Time.deltaTime * (timePassed / 25);
            score -= Time.deltaTime * (timePassed / 25);
            if (score >= -5)
            {
                progressSlider.transform.GetChild(1).transform.GetChild(0).GetComponent<Image>().color = Color.green;
            }
            else
            {
                progressSlider.transform.GetChild(1).transform.GetChild(0).GetComponent<Image>().color = Color.red;
            }
        }
    }

    public void StartSong()
    {
        songStarted = true;
        audio.Play();
        Time.timeScale = timeFlow;
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
    }

    public void Win()
    {
        scoreboard.text = "You Win!!!";
        Time.timeScale = 0.0f;
        audio.Pause();
        winLossTrigger = true;
        FindObjectOfType<SceneTransition>().LoadNextScene();
    }

    public void Lose()
    {
        scoreboard.text = "You Lose!!!";
        Time.timeScale = 0.0f;
        audio.Pause();
        winLossTrigger = true;
        FindObjectOfType<SceneTransition>().ReloadScene();
    }

    public void TimeFlow()
    {
        audio.pitch = timeFlow;
        Time.timeScale = timeFlow;
    }

    public void TimeFlow(float scale, float duration)
    {
        Time.timeScale = scale;
        audio.pitch = scale;
        Invoke("TimeFlow", duration);
    }
}