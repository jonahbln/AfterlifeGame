using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.TerrainTools;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    [SerializeField] GameObject spawnObject;
    [SerializeField] string spawnTimes;
    Queue<bool> spawnArray = new Queue<bool>();
    Queue<bool> spawnArray2 = new Queue<bool>();
    bool loop = false;
    public float quarterNoteDuration = .576923f;
    private float timePassed = 0f;

    void Start()
    {
        foreach (char c in spawnTimes)
        {
            if (c == '0')
            {
                spawnArray.Enqueue(false);
            }
            else if (c == '1')
            {
                spawnArray.Enqueue(true);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;
        if (timePassed >= quarterNoteDuration)
        {
            Notes();
            timePassed = 0f;
        }
    }

    void Notes()
    {
        bool thisNote;
        if (!loop)
        {
            thisNote = spawnArray.Dequeue();
            spawnArray2.Enqueue(thisNote);
        }
        else
        {
            thisNote = spawnArray2.Dequeue();
            spawnArray.Enqueue(thisNote);
        }
        if(spawnArray.Count == 0)
        {
            loop = true;
        }
        else if(spawnArray2.Count == 0)
        {
            loop = false;
        }

        if (thisNote)
        {
            GameObject obj = (GameObject)Instantiate(spawnObject, transform.position, transform.rotation);
        }
    }
}
