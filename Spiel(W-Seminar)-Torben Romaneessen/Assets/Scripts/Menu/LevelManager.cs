using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{
    int levelsUnlocked;
    public static LevelManager instance;
    public GameObject levelPage1;
    public GameObject levelPage2;

    public Button[] buttons;

    public bool levelpassed;



    private void Update()
    {
        
    }

    void Start()
    {
        //levelsUnlocked = PlayerPrefs.GetInt("levelsUnlocked", 1);

        //for (int i = 0; i < buttons.Length; i++)
        //{
        //    buttons[i].interactable = false;
        //}

        //for (int i = 0; i < levelsUnlocked; i++)
        //{
        //    buttons[i].interactable = true;
        //}
    }

    
    public void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }


    public void LoadNextPage()
    {
        levelPage1.SetActive(false);
        levelPage2.SetActive(true);
    }

    public void LoadPreviousPage()
    {
        levelPage1.SetActive(true);
        levelPage2.SetActive(false);
    }
}
