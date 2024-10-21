using Ink.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using UnityEditor.TerrainTools;
using UnityEngine;
using UnityEngine.Windows;

public class SpawnObject : MonoBehaviour
{
    [SerializeField] GameObject spawnObject;
    [SerializeField] char thisNote;
    String spawnTimes;
    List<float> spawnTimesArray = new List<float>();
    List<float> spawnTimesArray2 = new List<float>();
    private float timePassed = 0f;
    private bool songStarted = false;
    LevelManager levelManager;

    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        spawnTimes = levelManager.spawnTimes.text;
        bool inRange = false;
        string chunk = "";
        foreach (char c in spawnTimes)
        {
            if (c == thisNote)
            {
                inRange = true;
            }
            else if (inRange && c == ',')
            {
                spawnTimesArray.Add(float.Parse(chunk));
                chunk = "";
            }
            else if (inRange && c == ':')
            {
                break;
            }
            else if (inRange && (Char.IsDigit(c) || c == '.'))
            {
                chunk += c;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (songStarted)
        {
            timePassed += Time.deltaTime;
            if (spawnTimesArray.Count > 0 && spawnTimesArray[0] <= timePassed)
            {
                spawnTimesArray2.Add(spawnTimesArray[0]);
                spawnTimesArray.RemoveAt(0);
                GameObject obj = (GameObject)Instantiate(spawnObject, transform.position, transform.rotation);
            }
            else if (spawnTimesArray.Count == 0)
            {
                spawnTimesArray = spawnTimesArray2;
                timePassed = 0;
                songStarted = false;
            }
        }
    }

    public void StartSong()
    {
        songStarted = true;
    }
}