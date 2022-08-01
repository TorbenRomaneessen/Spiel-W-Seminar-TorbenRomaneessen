using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndPoint : MonoBehaviour
{
    public static bool LevelCompleted;
    [SerializeField]
    private Animator _flagAnimator;


    public void LevelPassed()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;

        if (currentLevel >= PlayerPrefs.GetInt("levelsUnlocked"))
        {
            PlayerPrefs.SetInt("levelsUnlocked", currentLevel + 1);
        }
        Debug.Log("LEVEL " + PlayerPrefs.GetInt("levelsUnlocked") + " UNLOCKED");

        LevelCompleted = true;
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Character"))
        {
            _flagAnimator.SetTrigger("LevelCompleted");
        }

        StartCoroutine(nameof(ShowLevelCompletedScene));
    }
    
    private IEnumerator ShowLevelCompletedScene()
    {
        yield return new WaitForSeconds(1f);
        LevelPassed();
        Destroy(this.gameObject);
    }
}
 