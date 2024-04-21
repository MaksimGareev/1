using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public GameObject player;
    public GameObject gameOverText;
    public AudioSource gameOverSound;

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
        if (player.GetComponent<RubyController>().CurrentHealth <= 0) // Accessing CurrentHealth
        {
            player.GetComponent<RubyController>().enabled = false;
        }
        gameOverText.SetActive(true);
        if (gameOverSound != null)
        {
            gameOverSound.Play();
        }
    }

    void RestartGame()
    {   
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
}
