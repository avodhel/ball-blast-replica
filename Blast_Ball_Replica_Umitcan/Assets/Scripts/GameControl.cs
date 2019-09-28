using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    [Header("Score")]
    public Text scoreText;
    [Header("Game Over")]
    public GameObject gameOverPanel;

    int score = 0;

    public void restartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void incScore(int value) //increase score
    {
        score += value;
        scoreText.text = "Score: " + score;
    }

    public void gameOver()
    {
        gameOverPanel.SetActive(true); //show game over panel
    }
}
