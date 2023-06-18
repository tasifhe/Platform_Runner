using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool isGameOver = false;
    public float restartDelay = 0.2f;
    public string gameSceneName; // Name of the main game scene
    public string gameOverSceneName; // Name of the game over scene

    public void GameOver()
    {
        if (isGameOver == false)
        {
            isGameOver = true;
            Debug.Log("Game Over!");
            Invoke("LoadGameOverScene", restartDelay);
        }
    }

    public void LoadGameOverScene()
    {
        SceneManager.LoadScene(gameOverSceneName); // Load the game over scene
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(gameSceneName); // Load the main game scene
    }
}
