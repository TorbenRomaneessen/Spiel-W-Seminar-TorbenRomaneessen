using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance2;
    public TextMeshProUGUI text;
    public int score;
  
    void Start()
    {
        if(instance2 == null)
        {
            instance2 = this;
        }
    }

    
    public void ChangeScore(int coinValue)
    {
        score += coinValue;
        text.text = "X" + score.ToString();
    }
}
