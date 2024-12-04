using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSoulSprite : MonoBehaviour
{
    [SerializeField]
    private float speed = 1.0f;
    [SerializeField]
    private float upperBound = 1.5f;
    [SerializeField]
    private float lowerBound = 0f;

    private bool movingUp = true;
    private Transform soulTransform;

    void Start()
    {
        soulTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (movingUp)
        {
            soulTransform.position += Vector3.up * speed * Time.deltaTime;
            if (soulTransform.position.y >= upperBound)
            {
                movingUp = false;
            }
        }
        else
        {
            soulTransform.position += Vector3.down * speed * Time.deltaTime;
            if (soulTransform.position.y <= lowerBound)
            {
                movingUp = true;
            }
        }
    }
}
