
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public GameObject player;
    public GameObject gameOverText;

    bool gameOver = false;

    void Start()
    {
        gameOverText.SetActive(false);
    }

    void Update()
    {
        if (gameOver && Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }

    public void EndGame()
    {
        gameOver = true;
        player.GetComponent<RubyController>().enabled = false;
        gameOverText.SetActive(true);
    }

    void RestartGame()
    {
        
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
}

