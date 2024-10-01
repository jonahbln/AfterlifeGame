using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    [SerializeField] GameObject spawnObject;
    [SerializeField] List<float> spawnTimes;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach(float f in spawnTimes)
        {
            if (Time.time >= f)
            {
                Spawn();
                spawnTimes.Remove(f);
            }
        }
    }

    void Spawn()
    {
        GameObject obj = (GameObject)Instantiate(spawnObject, transform.position, transform.rotation);
    }
}
