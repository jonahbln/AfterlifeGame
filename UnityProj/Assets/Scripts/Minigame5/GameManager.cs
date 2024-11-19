using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI scoreText;
    private float timeLeft = 10f;
    private int score = 0;
    private int totalPoints = 15;

    void Update()
    {
        timeLeft -= Time.deltaTime;
        timerText.text = "Time: " + Mathf.Round(timeLeft).ToString();

        if (timeLeft <= 0)
        {
            EndGame();
        }
    }

    public void CollectPoint()
    {
        score++;
        scoreText.text = "Score: " + score;

        if (score >= totalPoints)
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
    }
}
