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
                    if (guardScale.x > 3)
                    {
                        hit.collider.gameObject.transform.localScale = guardScale * 2f;
                        hit.collider.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
                        hit.collider.gameObject.transform.position = new Vector3(0, 0, 0);
                    }
                    else
                    {
                        hit.collider.gameObject.transform.localScale = guardScale * 1.2f;
                    }
                }
            }
        }
    }
}
