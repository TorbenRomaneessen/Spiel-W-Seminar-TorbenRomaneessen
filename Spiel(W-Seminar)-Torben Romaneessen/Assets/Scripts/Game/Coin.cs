using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinValue = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Character"))
        {
            ScoreManager.instance2.ChangeScore(coinValue);
            FindObjectOfType<AudioManager>().Play("Coin");
            //Destroy(collision.gameObject);
            Debug.Log("Coin!");
        }

        //if (collision.gameObject.CompareTag("Character"))
        //{
        //    LevelManager.instance.LevelPassed();
        //}
    }
}
