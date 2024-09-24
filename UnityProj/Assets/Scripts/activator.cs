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

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        colorUnpressed = sr.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(key) && !delay)
        {
            sr.color = colorPressed;
            isPressed = true;
            Invoke("Unpress", 0.5f);
        }
    }

    void Unpress()
    {
        sr.color = colorUnpressed;
        isPressed = false;
        delay = true;
        Invoke("Delay", 0.15f);
    }

    void Delay()
    {
        delay = false;
    }

    private void FixedUpdate()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Note"))
        {
            if (isPressed)
            {
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
                FindObjectOfType<LevelManager>().addScore((Mathf.Round(100 * (1 - Mathf.Abs(dist))) / 100), desc);

                Destroy(collision.gameObject);
            }
        }
    }

}
