using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance2;
    public TextMeshProUGUI text;
    public TextMeshProUGUI endtext;
    public TextMeshProUGUI totalDeaths;
    public int score;
    public int totalDeathCounter;

    void Start()
    {
        if(instance2 == null)
        {
            instance2 = this;
        }

        totalDeaths.text = PlayerPrefs.GetInt("totalDeathCounter").ToString();
    }


    public void ChangeScore(int coinValue)
    {
        score += coinValue;
        text.text = "X" + score.ToString();
        endtext.text = "X" + score.ToString();
    }

    
    public void ChangeDeathCounter()
    {
        PlayerPrefs.SetInt("totalDeathCounter", PlayerPrefs.GetInt("totalDeathCounter") + 1);
        totalDeaths.text = PlayerPrefs.GetInt("totalDeathCounter").ToString();
    }
}
