using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndPoint : MonoBehaviour
{
    public void LevelPassed()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;

        if (currentLevel >= PlayerPrefs.GetInt("levelsUnlocked"))
        {
            PlayerPrefs.SetInt("levelsUnlocked", currentLevel + 1);
        }
        Debug.Log("LEVEL " + PlayerPrefs.GetInt("levelsUnlocked") + " UNLOCKED");
    }


    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Character"))
        {
            LevelPassed();
            Debug.Log("You have completed this Level!");
            Destroy(this.gameObject);
        }
    }
}
