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


}