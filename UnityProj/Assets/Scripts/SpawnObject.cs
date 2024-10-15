using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.TerrainTools;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    [SerializeField] GameObject spawnObject;
    [SerializeField] float[] spawnTimes;
    Queue<bool> spawnArray = new Queue<bool>();
    private float timePassed = 0f;
    public bool songStarted = false;

    void Start()
    {

    }

    void Note()
    {
        GameObject obj = (GameObject)Instantiate(spawnObject, transform.position, transform.rotation);

    }

    // Update is called once per frame
    void Update()
    {
        if (songStarted)
        {
            foreach (float f in spawnTimes)
            {
                Invoke("Note", f);
            }
            songStarted = false;
        }
    }

    public void StartSong()
    {
        songStarted = true;
    }

    void Notes()
    {
        bool thisNote = spawnArray.Dequeue();

        if (thisNote)
        {
            GameObject obj = (GameObject)Instantiate(spawnObject, transform.position, transform.rotation);
        }
    }
}
