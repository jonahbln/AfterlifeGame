using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI scoreText;
    private float timeLeft = 30f;
    private int score = 10;
    public bool isGameOver = false;

    void Update()
    {
        if (!isGameOver)
        {
            timeLeft -= Time.deltaTime;
            timerText.text = "Time: " + Mathf.Round(timeLeft).ToString();

            if (timeLeft <= 0)
            {
                EndGame();
            }
        }
    }

    public void CollectPoint()
    {
        score--;
        scoreText.text = "Souls to Collect: " + score;

        if (score == 0)
        {
            WinGame();
        }
    }

    void EndGame()
    {
        timerText.text = "Time's up!";
    }

    void WinGame()
    {
        timerText.text = "You Win!";
        isGameOver = true;
        Invoke("goToNextScene", 2);
    }

    void goToNextScene()
    {
        FindObjectOfType<SceneTransition>().LoadNextScene();

    }
}
