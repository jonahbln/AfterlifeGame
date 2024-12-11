using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulClicked : MonoBehaviour
{
    private GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    void Update()
    {

        if (Input.GetMouseButtonDown(0) && !gameManager.isGameOver)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("Soul"))
                {
                    gameManager.CollectPoint();
                    Destroy(hit.collider.gameObject);
                }
                if (hit.collider.CompareTag("Guard"))
                {
                    Vector3 guardScale = hit.collider.gameObject.transform.localScale;
                    hit.collider.gameObject.transform.localScale = guardScale * 1.5f;
                    Rigidbody2D rb = hit.collider.gameObject.GetComponent<Rigidbody2D>();
                    rb.velocity = rb.velocity * Random.Range(1.5f, 2f);
                }
            }
        }
    }
}
