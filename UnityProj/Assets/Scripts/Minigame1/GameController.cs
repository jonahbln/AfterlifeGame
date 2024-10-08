using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject[] squares;
    public AudioClip[] sounds;
    private List<int> randomSequence;
    private int currentStep;
    private bool pressable;
    private AudioSource audioSource;
    private float soundPlayTime = 1f;
    private float soundTimer = 0f;
    private bool isPlayingSound = false;
    private int squareNum = 9;
    private int sequenceNum = 5;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        pressable = false;
        Invoke("startLate", 1f);
    }

    void startLate()
    {
        GenerateRandomSequence();
        StartCoroutine(SquareSequence());
    }

    void Update()
    {
        if (isPlayingSound)
        {
            soundTimer -= Time.deltaTime;

            if (soundTimer <= 0f)
            {
                audioSource.Stop();
                isPlayingSound = false;
            }
        }
    }

    void GenerateRandomSequence()
    {
        randomSequence = new List<int>();
        List<int> availableIndices = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8 };

        while (availableIndices.Count > squareNum - sequenceNum)
        {
            int randomIndex = Random.Range(0, availableIndices.Count);
            randomSequence.Add(availableIndices[randomIndex]);
            availableIndices.RemoveAt(randomIndex);
        }

        currentStep = 0;
    }

    IEnumerator SquareSequence()
    {
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
        PlaySound(index);
        yield return new WaitForSeconds(1f);
        color.a = 1f;
        renderer.color = color;
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
                PlaySound(squareIndex);
                Debug.Log("Correct");

                if (currentStep == randomSequence.Count)
                {
                    Debug.Log("You Win");
                }
            }
            else
            {
                pressable = false;
                Debug.Log("Incorrect");
                currentStep = 0;
                Invoke("LightUpAllSquares", 1f);
                Invoke("Restart", 1f);
            }
        }
    }

    private void PlaySound(int index)
    {
        audioSource.clip = sounds[index];
        audioSource.Play();
        isPlayingSound = true;
        soundTimer = soundPlayTime;
    }

    public void Restart()
    {
        GenerateRandomSequence();
        StartCoroutine(SquareSequence());
    }
}
