using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMovement : MonoBehaviour
{
    public float speed = 2.0f;
    private Rigidbody2D rb;
    private Collision2D c;
    private Vector2 vel;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        MoveInRandomDirection();
    }

    void MoveInRandomDirection()
    {
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        rb.velocity = randomDirection * (speed + Random.Range(-0.75f, 1.5f));
        vel = rb.velocity;
        Debug.Log(rb.velocity);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            c = collision;
            bounceOff();
        }
    }

    void bounceOff()
    {
        Vector2 collisionNormal = c.contacts[0].normal;
        Vector2 reflectedVelocity = Vector2.Reflect(vel, collisionNormal);
        reflectedVelocity.Normalize();
        rb.velocity = reflectedVelocity * vel.magnitude;
        vel = rb.velocity;
    }
}
