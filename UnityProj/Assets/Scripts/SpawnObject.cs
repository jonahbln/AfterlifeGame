using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    [SerializeField] GameObject spawnObject;

    void Start()
    {
        InvokeRepeating("Spawn", Random.value * 3 + 2, Random.value * 3 +  2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Spawn()
    {
        GameObject obj = (GameObject)Instantiate(spawnObject, transform.position, transform.rotation);
    }
}
