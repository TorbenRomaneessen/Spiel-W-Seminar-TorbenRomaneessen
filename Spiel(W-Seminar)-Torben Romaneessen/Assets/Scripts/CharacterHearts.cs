using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHearts : MonoBehaviour
{
    private string ThornsTag = "Thorns";
    private bool CollisionWithThorns;
    public float playerHearts = 5f;
    public CharacterHearts(int PlayerHearts)
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        DamageingObjects();
    }

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(ThornsTag))
        {
            CollisionWithThorns = true;
        }
        else
        {
            CollisionWithThorns = false;
        }
    }*/
    public void DamageingObjects()
    {
        if(CollisionWithThorns == true)
        {
            TakeDamage(1);
        }
    }

    public void TakeDamage(int damage)
    {
        playerHearts -= damage;

        if (playerHearts <= 0)
        {
            StartCoroutine(Die());
        }
    }

    private IEnumerator Die()
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }
}
