using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public static bool GameIsOver = false;
    public bool finishText;
    public GameObject pauseMenuUI;
    public GameObject gameOverUI;
    public GameObject levelCompletedUI;

    public static PauseMenu instance;



    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }


    void Update()
    {
        if (Input.GetButtonDown("Cancel") && GameIsOver == false)
        {
            if(GameIsPaused)
            {
                Resume();
                finishText = true;

            }

            else
            {
                    Pause();
                    Dialog.instance.DialogBox.SetActive(false);
                    Dialog.instance.continueButton.SetActive(false);
                    

            }
        }

        if (Character.playerDied == true)
        {
            GameOver();
            GameIsOver = true;
            Character.playerDied = false;
        }

        if (EndPoint.LevelCompleted == true)
        {
            LevelCompleted();
            EndPoint.LevelCompleted = false;
        }
    }


    public void Resume()
   {
        pauseMenuUI.SetActive(false);
        FindObjectOfType<AudioManager>().Play("ClickSound");
        Time.timeScale = 1f;
        GameIsPaused = false;
        finishText = true;

        if (Dialog.instance.isTalking == true)
        {
            Dialog.instance.DialogBox.SetActive(true);
            Dialog.instance.continueButton.SetActive(true);
        }
    }


   private void Pause()
   {
        pauseMenuUI.SetActive(true);
        FindObjectOfType<AudioManager>().Play("ClickSound");
        Time.timeScale = 0f;
        GameIsPaused = true;
   }


    private void GameOver()
    {
        FindObjectOfType<AudioManager>().Play("DyingSound");
        gameOverUI.SetActive(true);
        Time.timeScale = 0f;
    }


    private void LevelCompleted()
    {
        FindObjectOfType<AudioManager>().Play("LevelCompletedSound");
        levelCompletedUI.SetActive(true);
        Time.timeScale = 0f;
    }


    public void LoadNextLevel()
    {
        FindObjectOfType<AudioManager>().Play("ClickSound");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1f;
    }


    public void Restart()
    {
        FindObjectOfType<AudioManager>().Play("ClickSound");
        Debug.Log("Game has been restarted!");
        gameOverUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsOver = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    public void LoadMenu()
    {
        FindObjectOfType<AudioManager>().Play("ClickSound");
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }


    public void QuitGame()
    {
        FindObjectOfType<AudioManager>().Play("ClickSound");
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
