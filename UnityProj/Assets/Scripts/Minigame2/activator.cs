using Ink.Parsed;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activator : MonoBehaviour
{
    public KeyCode key;
    private Color colorUnpressed;
    private SpriteRenderer sr;
    public Color colorPressed = Color.magenta;
    private bool isPressed = false;
    private bool delay = false;
    LevelManager levelManager;
    private AudioSource audioSource;
    private bool hitDelay = false;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        colorUnpressed = sr.color;
        levelManager = FindObjectOfType<LevelManager>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(key) && !delay)
        {
            sr.color = colorPressed;
            isPressed = true;
            Invoke("Unpress", 0.1f);
        }
        if (levelManager.winLossTrigger)
        {
            Destroy(gameObject);
        }
    }

    void Unpress()
    {
        sr.color = colorUnpressed;
        isPressed = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Note") && !hitDelay)
        {
            if (isPressed)
            {
                hitDelay = true;
                Invoke("HitDelay", 0.125f);
                float dist = transform.position.y - collision.transform.position.y;
                string desc;
                if (dist > 0.15)
                {
                    desc = "Late";
                }
                else if (dist < -0.15)
                {
                    desc = "Early";
                }
                else
                {
                    desc = "Nice!";
                }
                levelManager.addScore((Mathf.Round(100 * (1.5f - Mathf.Abs(dist))) / 100), desc);

                Destroy(collision.gameObject);
            }
        }
    }

    void HitDelay()
    {
        hitDelay = false;
    }

}
