using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceOnWall : MonoBehaviour
{
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Vector2 normal = collision.contacts[0].normal;
            rb.velocity = Vector2.Reflect(rb.velocity, normal);
        }
    }
}
