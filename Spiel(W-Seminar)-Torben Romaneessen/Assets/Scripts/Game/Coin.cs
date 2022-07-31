using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinValue = 1;
    public GameObject coin;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Character"))
        {
            ScoreManager.instance2.ChangeScore(coinValue);
            FindObjectOfType<AudioManager>().Play("CoinSound");
            //Destroy(collision.gameObject);
            Destroy(coin);
            Debug.Log("Coin!");
        }
    }
}
