using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        FindObjectOfType<AudioManager>().Play("ClickSound");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void PlaySound()
    {
        FindObjectOfType<AudioManager>().Play("ClickSound");
    }

    public void QuitGame()
    {
        FindObjectOfType<AudioManager>().Play("ClickSound");
        Application.Quit();
    }
}
