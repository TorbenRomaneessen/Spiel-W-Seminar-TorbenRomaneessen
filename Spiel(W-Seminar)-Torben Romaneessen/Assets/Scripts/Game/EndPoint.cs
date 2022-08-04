using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndPoint : MonoBehaviour
{
    public static bool LevelIsCompleted;
    [SerializeField]
    private Animator _flagAnimator;


    public void LevelPassed()
    {
        int _currentLevel = SceneManager.GetActiveScene().buildIndex - 1;

        if (_currentLevel >= PlayerPrefs.GetInt("levelsUnlocked") && _currentLevel < 4)
        {
            PlayerPrefs.SetInt("levelsUnlocked", _currentLevel + 1);
        }

        LevelIsCompleted = true;
    }

    private void OnTriggerEnter2D(Collider2D col)
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
 