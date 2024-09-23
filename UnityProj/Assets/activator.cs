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

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        colorUnpressed = sr.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(key))
        {
            sr.color = colorPressed;
            isPressed = true;
        }
        else if (Input.GetKeyUp(key))
        {
            sr.color = colorUnpressed;
            isPressed = false;
        }
    }

    private void FixedUpdate()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Note")
        {
            if(isPressed)
            {
                
                Destroy(collision.gameObject, 1f - collision.gameObject.GetComponent<Note>().gravity);
            }
        }
    }

}
