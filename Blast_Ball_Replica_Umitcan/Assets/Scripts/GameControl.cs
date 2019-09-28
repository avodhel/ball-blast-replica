using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    [Header("Game Over")]
    public GameObject gameOverPanel;

    public void restartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void gameOver()
    {
        gameOverPanel.SetActive(true); //show game over panel
    }
}
