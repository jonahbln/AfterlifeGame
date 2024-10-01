using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareBehavior : MonoBehaviour
{
    public int squareIndex;
    private GameController gameController;

    void Start()
    {
        gameController = FindObjectOfType<GameController>();
    }

    void OnMouseDown()
    {
        gameController.CheckPlayerInput(squareIndex);
    }
}
