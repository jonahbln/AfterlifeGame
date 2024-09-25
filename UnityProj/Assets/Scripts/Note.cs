using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Note : MonoBehaviour
{
    [SerializeField]public float gravity = 0.05f;
    Rigidbody2D rb;
    SpriteRenderer sr;
    Color defaultColor;
    [SerializeField] Color contactColor = Color.magenta;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = gravity;
        sr = GetComponent<SpriteRenderer>();
        defaultColor = sr.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Activator"))
        {
            sr.color = contactColor;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Activator"))
        {
            sr.color = defaultColor;
        }
    }

}