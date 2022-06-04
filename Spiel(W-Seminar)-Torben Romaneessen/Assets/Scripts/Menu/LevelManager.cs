using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{
    int levelsUnlocked;
    public static LevelManager instance;

    public Button[] buttons;

    public bool levelpassed;



    void Start()
    {
        levelsUnlocked = PlayerPrefs.GetInt("levelsUnlocked", 1);

        for(int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }

        for (int i = 0; i < levelsUnlocked; i++)
        {
            buttons[i].interactable = true;
        }
    }

    
    public void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }


   public void LevelPassed()
   {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;

        //if(currentLevel >= PlayerPrefs.GetInt("levelsUnlocked"))
        //{
            PlayerPrefs.SetInt("levelsUnlocked", currentLevel + 1);
       // }
        Debug.Log("LEVEL " + PlayerPrefs.GetInt("levelsUnlocked") + " UNLOCKED");  
   }

 
}
