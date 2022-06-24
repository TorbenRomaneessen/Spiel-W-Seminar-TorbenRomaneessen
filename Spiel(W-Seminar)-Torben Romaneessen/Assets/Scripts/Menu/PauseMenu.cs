using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public static bool GameIsOver = false;
    public GameObject pauseMenuUI;
    public GameObject gameOverUI;



    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && GameIsOver == false)
        {
            if(GameIsPaused )
            {
                Resume();
            }

            else
            {
                Pause();
            }
        }

        if(Character.playerDied == true)
        {
            GameOver();
            GameIsOver = true;
            Character.playerDied = false;
        }
    }


   public void Resume()
   {
        pauseMenuUI.SetActive(false);
        //gameOverUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
   }


   private void Pause()
   {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
   }


    private void GameOver()
    {
        gameOverUI.SetActive(true);
        Time.timeScale = 0f;
    }


    public void Restart()
    {
        gameOverUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsOver = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }


    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
