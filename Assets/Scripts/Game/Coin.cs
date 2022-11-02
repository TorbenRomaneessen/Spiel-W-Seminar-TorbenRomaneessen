using UnityEngine;

public class Coin : MonoBehaviour
{
    private const int CoinValue = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Character"))
        {
            FindObjectOfType<AudioManager>().Play("CoinSound");
            ScoreManager.Instance.ChangeScore(CoinValue);
            Destroy(this.gameObject);
        }
    }
}