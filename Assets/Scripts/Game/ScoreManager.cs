using UnityEngine.UI;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int _score;
    public static ScoreManager Instance;

    [SerializeField]
    private Text _currentCoinCounter;
    [SerializeField]
    private Text _coinsCollectedFinal;
    [SerializeField]
    private Text _totalDeaths;


    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        _totalDeaths.text = PlayerPrefs.GetInt("totalDeathCounter").ToString();
    }

    public void ChangeScore(int coinValue)
    {
        _score += coinValue;
        _currentCoinCounter.text = "X" + _score.ToString();
        _coinsCollectedFinal.text = "X" + _score.ToString();
    }

    public void ChangeDeathCounter()
    {
        PlayerPrefs.SetInt("totalDeathCounter", PlayerPrefs.GetInt("totalDeathCounter") + 1);
        _totalDeaths.text = PlayerPrefs.GetInt("totalDeathCounter").ToString();
    }
}
