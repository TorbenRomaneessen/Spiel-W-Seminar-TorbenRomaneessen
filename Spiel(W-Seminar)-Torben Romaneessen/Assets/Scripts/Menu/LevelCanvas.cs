using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class LevelCanvas : MonoBehaviour
{
    public bool GameIsPaused;
    public bool GameIsOver;
    public bool LevelIsCompleted;
    public bool FinishText;

    [SerializeField]
    private GameObject _pauseCanvas;
    [SerializeField]
    private GameObject _gameOverCanvas;
    [SerializeField]
    private GameObject _levelCompletedCanvas;

    public static LevelCanvas Instance;

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

        if (EndPoint.LevelIsCompleted == true)
        {
            LevelCompleted();
        }
    }


    public void Resume()
    {
        FindObjectOfType<AudioManager>().Play("ClickSound");
        _pauseCanvas.SetActive(false);

        GameIsPaused = false;
        FinishText = true;

        if (Dialog.Instance.IsTalking == true)
        {
            Dialog.Instance.DialogBox.SetActive(true);
            Dialog.Instance.ContinueButton.SetActive(true);
        }
    }

   private void Pause()
   {
        FindObjectOfType<AudioManager>().Play("ClickSound");
        _pauseCanvas.SetActive(true);
        _eventSystem.SetSelectedGameObject(_resumeButton);

        Dialog.Instance.DialogBox.SetActive(false);
        Dialog.Instance.ContinueButton.SetActive(false);
        GameIsPaused = true;
   }

    private void GameOver()
    {
        FindObjectOfType<AudioManager>().Play("GameOverSound");
        _gameOverCanvas.SetActive(true);
        _eventSystem.SetSelectedGameObject(_restartButton);

        GameIsOver = true;
        Character.IsDead = false;
    }

    private void LevelCompleted()
    {
        FindObjectOfType<AudioManager>().Play("LevelCompletedSound");
        _levelCompletedCanvas.SetActive(true);
        _eventSystem.SetSelectedGameObject(_nextLevelButton);

        // This boolean is true because it disables the character movements.
        LevelIsCompleted = true;
        // This boolean is set to false because otherwise the LevelCompletedScreen wouldn't turn off.
        EndPoint.LevelIsCompleted = false;
        
    }

    public void LoadNextLevel()
    {
        FindObjectOfType<AudioManager>().Play("ClickSound");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        LevelIsCompleted = false;
    }

    public void Restart()
    {
        FindObjectOfType<AudioManager>().Play("ClickSound");
        _gameOverCanvas.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMenu()
    {
        FindObjectOfType<AudioManager>().Play("ClickSound");
        SceneManager.LoadScene("Menu");

        LevelIsCompleted = false;
    }
}