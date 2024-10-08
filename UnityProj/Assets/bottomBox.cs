using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bottomBox : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Note"))
        {
            FindObjectOfType<LevelManager>().addScore(-0.5f, "Missed!");
            Destroy(collision.gameObject);
        }
    }
}
