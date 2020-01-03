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

    private int score = 0;

    private void Start()
    {
        ResetHighScore();

        //get score values
        scoreText.text = "Score: " + score;
        highScoreText.text = "HighScore: " + PlayerPrefs.GetInt("HighScore", 0);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }

    private void ResetHighScore()
    {
        if (resetHighScoreControl)
        {
            PlayerPrefs.DeleteKey("HighScore");
        }
    }

    public void IncScore(int value) //increase score
    {
        score += value;
        scoreText.text = "Score: " + score;
    }

    private void AssignHighScore(int score)
    {
        if (score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", score);
            highScoreText.text = "HighScore: " + PlayerPrefs.GetInt("HighScore", 0);
        }
    }

    public void GameOver()
    {
            gameOverPanel.SetActive(true); //show game over panel
            AssignHighScore(score);
    }
}
