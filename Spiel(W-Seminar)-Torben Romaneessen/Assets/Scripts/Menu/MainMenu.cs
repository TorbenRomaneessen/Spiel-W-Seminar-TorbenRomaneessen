using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        FindObjectOfType<AudioManager>().Play("ClickSound");
    }

    public void PlaySound()
    {
        FindObjectOfType<AudioManager>().Play("ClickSound");
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        FindObjectOfType<AudioManager>().Play("ClickSound");
        Application.Quit();
    }
}
