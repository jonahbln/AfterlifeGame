using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject[] squares;
    private List<int> randomSequence;
    private int currentStep;
    private bool pressable;

    void Start()
    {
        GenerateRandomSequence();
        StartCoroutine(SquareSequence());
    }

    void GenerateRandomSequence()
    {
        randomSequence = new List<int>();
        List<int> availableIndices = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8 };

        while (availableIndices.Count > 0)
        {
            int randomIndex = Random.Range(0, availableIndices.Count);
            randomSequence.Add(availableIndices[randomIndex]);
            availableIndices.RemoveAt(randomIndex);
        }

        currentStep = 0;
    }

    IEnumerator SquareSequence()
    {
        pressable = false;

        foreach (int index in randomSequence)
        {
            yield return StartCoroutine(PressSquareEffect(index));
        }

        LightUpAllSquares();

        pressable = true;
    }

    IEnumerator PressSquareEffect(int index)
    {
        SpriteRenderer renderer = squares[index].GetComponent<SpriteRenderer>();
        Color color = renderer.color;

        color.a = 0.5f;
        renderer.color = color;

        yield return new WaitForSeconds(1f);
    }

    public void LightUpAllSquares()
    {
        foreach (GameObject square in squares)
        {
            SpriteRenderer renderer = square.GetComponent<SpriteRenderer>();
            Color color = renderer.color;
            color.a = 1f;
            renderer.color = color;
        }
    }

    public void CheckPlayerInput(int squareIndex)
    {
        if (pressable)
        {
            if (squareIndex == randomSequence[currentStep])
            {
                currentStep++;
                SpriteRenderer renderer = squares[squareIndex].GetComponent<SpriteRenderer>();
                Color color = renderer.color;
                color.a = 0.5f;
                renderer.color = color;

                Debug.Log("Correct");

                if (currentStep == randomSequence.Count)
                {
                    Debug.Log("You Win");
                }
            }
            else
            {
                Debug.Log("Incorrect");
                currentStep = 0;
                Invoke("LightUpAllSquares", 1f);
                Invoke("Restart", 1f);
            }
        }
    }

    public void Restart()
    {
        GenerateRandomSequence();
        StartCoroutine(SquareSequence());
    }
}
