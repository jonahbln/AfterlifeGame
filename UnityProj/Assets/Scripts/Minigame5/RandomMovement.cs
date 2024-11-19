using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMovement : MonoBehaviour
{
    public float speed = 2.0f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        MoveInRandomDirection();
    }

    void MoveInRandomDirection()
    {
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        rb.velocity = randomDirection * speed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        MoveInRandomDirection();
    }
}
