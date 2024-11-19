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
        if (Input.GetMouseButtonDown(0))
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
                    hit.collider.gameObject.transform.localScale = hit.collider.gameObject.transform.localScale * 1.5f;
                }
            }
        }
    }
}
