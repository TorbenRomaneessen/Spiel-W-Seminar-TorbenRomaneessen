using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused;
    public static bool GameIsOver;
    public bool FinishText;

    [SerializeField]
    private GameObject _pauseMenuUI;
    [SerializeField]
    private GameObject _gameOverUI;
    [SerializeField]
    private GameObject _levelCompletedUI;

    public static PauseMenu Instance;

    [SerializeField]
    private EventSystem _eventSystem;
    [SerializeField]
    private GameObject _resumeButton;
    [SerializeField]
    private GameObject _restartButton;
    [SerializeField]
    private GameObject _nextLevelButton;


    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        _eventSystem = EventSystem.current;
    }


    private void Update()
    {
        if (Input.GetButtonDown("Cancel") && GameIsOver == false)
        {
            if(GameIsPaused)
            {
                Resume();
            }

            else
            {
                Pause();
            }
        }

        if (Character.IsDead == true)
        {
            GameOver();
        }

        if (EndPoint.LevelCompleted == true)
        {
            LevelCompleted();
        }
    }


    private void Resume()
    {
        FindObjectOfType<AudioManager>().Play("ClickSound");
        _pauseMenuUI.SetActive(false);

        GameIsPaused = false;
        FinishText = true;

        Time.timeScale = 1f;

        if (Dialog.Instance.IsTalking == true)
        {
            Dialog.Instance.DialogBox.SetActive(true);
            Dialog.Instance.ContinueButton.SetActive(true);
        }
    }

   private void Pause()
   {
        FindObjectOfType<AudioManager>().Play("ClickSound");
        _pauseMenuUI.SetActive(true);
        _eventSystem.SetSelectedGameObject(_resumeButton);

        Dialog.Instance.DialogBox.SetActive(false);
        Dialog.Instance.ContinueButton.SetActive(false);
        GameIsPaused = true;

        Time.timeScale = 0f;
   }

    private void GameOver()
    {
        FindObjectOfType<AudioManager>().Play("GameOverSound");
        _gameOverUI.SetActive(true);
        _eventSystem.SetSelectedGameObject(_restartButton);

        GameIsOver = true;
        Character.IsDead = false;

        Time.timeScale = 0f;
    }

    private void LevelCompleted()
    {
        FindObjectOfType<AudioManager>().Play("LevelCompletedSound");
        _levelCompletedUI.SetActive(true);
        _eventSystem.SetSelectedGameObject(_nextLevelButton);

        EndPoint.LevelCompleted = false;

        Time.timeScale = 0f;
    }

    private void LoadNextLevel()
    {
        FindObjectOfType<AudioManager>().Play("ClickSound");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        Time.timeScale = 1f;
    }

    private void Restart()
    {
        FindObjectOfType<AudioManager>().Play("ClickSound");
        _gameOverUI.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        Time.timeScale = 1f;
    }

    private void LoadMenu()
    {
        FindObjectOfType<AudioManager>().Play("ClickSound");
        SceneManager.LoadScene("Menu");

        Time.timeScale = 1f;
    }

    private void QuitGame()
    {
        FindObjectOfType<AudioManager>().Play("ClickSound");
        Application.Quit();
    }
}