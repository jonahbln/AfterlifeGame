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
    LevelManager levelManager;
    private bool hitDelay = false;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        colorUnpressed = sr.color;
        levelManager = FindObjectOfType<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(key))
        {
            if (isPressed)
            {
                levelManager.addScore(-1f, "Don't Spam");
            }
            sr.color = colorPressed;
            isPressed = true;
            Invoke("Unpress", 0.115f);
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
                if (dist > 0.2)
                {
                    desc = "Late";
                }
                else if (dist < -0.3)
                {
                    desc = "Early";
                }
                else
                {
                    desc = "Nice!";
                    transform.GetChild(0).GetComponent<Animator>().SetBool("hit", true);
                }
                Destroy(collision.gameObject);
                levelManager.addScore((Mathf.Round(100 * (1.5f - Mathf.Abs(dist))) / 100), desc);

            }
        }
    }

    void HitDelay()
    {
        hitDelay = false;
    }
}
