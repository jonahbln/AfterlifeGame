using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMovement : MonoBehaviour
{
    public float speed = 2.0f;
    private Rigidbody2D rb;
    private Collision2D c;
    private Vector2 vel;
    private float timePassed = 0f;
    private float gameTime = 0f;
    private float afterSomeTime = 10f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        MoveInRandomDirection();

        // Random size
        Transform myTransform = transform;
        Vector3 scale = myTransform.localScale;
        float xScale = scale.x;
        float yScale = scale.y;
        float randomScale = Random.Range(0.5f, 1f);
        scale.x = xScale * randomScale;
        scale.y = yScale * randomScale;
        myTransform.localScale = scale;
    }

    void Update()
    {
        // Random speed boost
        timePassed += Time.deltaTime;
        gameTime += Time.deltaTime;

        if (gameTime >= 20f && timePassed >= 1f)
        {
            timePassed = 0f;
            rb.velocity = rb.velocity * Random.Range(1.5f, 2f);
        }
    }

    void MoveInRandomDirection()
    {
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        rb.velocity = randomDirection * (speed + Random.Range(-1f, 3f));
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
