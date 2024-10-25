using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject[] squares;
    public AudioClip[] sounds;
    public GameObject[] circles;
    private List<int> randomSequence;
    private int currentStep;
    private bool pressable;
    private AudioSource audioSource;
    private float soundPlayTime = 1f;
    private float soundTimer = 0f;
    private bool isPlayingSound = false;
    private int squareNum = 9;
    private int sequenceNum = 5;
    private int roundWon = 0;
    private int roundWonChecker = 0;
    

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
        
        SpriteRenderer renderer = squares[index].transform.GetChild(0).GetComponent<SpriteRenderer>();
        Color color = renderer.color;

        color.r = 0.5f;
        color.g = 0.5f;
        color.b = 0.5f;
        renderer.color = color;
        PlaySound(index);
        yield return new WaitForSeconds(1f);
        color.r = 1f;
        color.g = 1f;
        color.b = 1f;
        renderer.color = color;
    }

    void LightUpAllSquares()
    {
        foreach (GameObject square in squares)
        {
            SpriteRenderer renderer = square.transform.GetChild(0).GetComponent<SpriteRenderer>();
            Color color = renderer.color;
            color.r = 1f;
            color.g = 1f;
            color.b = 1f;
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
                SpriteRenderer renderer = squares[squareIndex].transform.GetChild(0).GetComponent<SpriteRenderer>();
                Color color = renderer.color;
                color.r = 0.5f;
                color.g = 0.5f;
                color.b = 0.5f;
                renderer.color = color;
                PlaySound(squareIndex);
                Debug.Log("Correct");

                if (currentStep == randomSequence.Count)
                {
                    Debug.Log("You Win");
                    Invoke("roundWin", 1f);
                    roundWonChecker++;
                    if (isGameOver())
                    {
                        //Go to dialogue
                        Invoke("goNextScene", 1f);
                    }
                    else
                    {
                        pressable = false;
                        currentStep = 0;
                        Invoke("LightUpAllSquares", 1.5f);
                        Invoke("Restart", 2.5f);
                    }
                }
            }
            else
            {
                lostFeedback();
                roundWonChecker = 0;
                roundWon = 0;
                pressable = false;
                Debug.Log("Incorrect");
                currentStep = 0;
                Invoke("LightUpAllSquares", 1.5f);
                Invoke("Restart", 2.5f);
            }
        }
    }

    void PlaySound(int index)
    {
        audioSource.clip = sounds[index];
        audioSource.Play();
        isPlayingSound = true;
        soundTimer = soundPlayTime;
    }

    void Restart()
    {
        GenerateRandomSequence();
        StartCoroutine(SquareSequence());
    }

    bool isGameOver()
    {
        return roundWonChecker == 3;
    }

    void roundWin()
    {
        SpriteRenderer renderer = circles[roundWon].GetComponent<SpriteRenderer>();
        Color color = renderer.color;
        color.a = 1f;
        renderer.color = color;
        roundWon++;
    }

    void lostFeedback()
    {
        changeRed();
        Invoke("changeGreen", 2f);
    }

    void changeRed()
    {
        foreach(GameObject circle in circles)
        {
            SpriteRenderer renderer = circle.GetComponent<SpriteRenderer>();
            Color color = renderer.color;
            color.a = 1f;
            color.r = 1f;
            color.g = 0f;
            renderer.color = color;
        }
    }

    void changeGreen()
    {
        foreach (GameObject circle in circles)
        {
            SpriteRenderer renderer = circle.GetComponent<SpriteRenderer>();
            Color color = renderer.color;
            color.a = 0.4f;
            color.r = 0f;
            color.g = 1f;
            renderer.color = color;
        }
    }

    void goNextScene()
    {
        FindObjectOfType<SceneTransition>().LoadNextScene();
    }
}
