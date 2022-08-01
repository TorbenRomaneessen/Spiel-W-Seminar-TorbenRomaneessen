using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{
    private int _levelsUnlocked;
    //public static LevelManager Instance;
    [SerializeField]
    private GameObject _levelPage1;
    [SerializeField]
    private GameObject _levelPage2;

    [SerializeField]
    private Button[] _buttons;


    void Start()
    {
        _levelsUnlocked = PlayerPrefs.GetInt("levelsUnlocked", 1);

        for (int i = 0; i < _buttons.Length; i++)
        {
            _buttons[i].interactable = false;
        }

        for (int i = 0; i < _levelsUnlocked; i++)
        {
            _buttons[i].interactable = true;
        }
    }

    private void LoadLevel(int levelIndex)
    {
        FindObjectOfType<AudioManager>().Play("StartGameSound");
        SceneManager.LoadScene(levelIndex);
    }

    private void LoadNextPage()
    {
        FindObjectOfType<AudioManager>().Play("ClickSound");
        _levelPage1.SetActive(false);
        _levelPage2.SetActive(true);
    }

    private void LoadPreviousPage()
    {
        FindObjectOfType<AudioManager>().Play("ClickSound");
        _levelPage1.SetActive(true);
        _levelPage2.SetActive(false);
    }
}
