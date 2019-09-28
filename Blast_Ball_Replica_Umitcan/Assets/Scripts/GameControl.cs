using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    [Header("Score")]
    public Text scoreText;

    [Header("High Score")]
    public bool resetHighScoreControl = false;
    public Text highScoreText;

    [Header("Game Over")]
    public GameObject gameOverPanel;

    int score = 0;

    private void Start()
    {
        resetHighScore();
        //get score values
        scoreText.text = "Score: " + score;
        highScoreText.text = "HighScore: " + PlayerPrefs.GetInt("HighScore", 0);
    }

    public void restartGame()
    {
        SceneManager.LoadScene(1);
    }

    void resetHighScore()
    {
        if (resetHighScoreControl)
        {
            PlayerPrefs.DeleteKey("HighScore");
        }
    }

    public void incScore(int value) //increase score
    {
        score += value;
        scoreText.text = "Score: " + score;
    }

    public void assignHighScore(int score)
    {
        if (score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", score);
            highScoreText.text = "HighScore: " + PlayerPrefs.GetInt("HighScore", 0);
        }
    }

    public void gameOver()
    {
        gameOverPanel.SetActive(true); //show game over panel
        assignHighScore(score);
    }
}
