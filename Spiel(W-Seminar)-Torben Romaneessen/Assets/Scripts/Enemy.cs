using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int maxHealth = 100;
    int currentHealth;

    public Transform enemy;
    SpriteRenderer spriteRendererEnemy;
    private float timePassed = 0;

    public void Awake()
    {
        enemy = GetComponent<Transform>();
        spriteRendererEnemy = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        StartCoroutine("EnemyMovement");
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {

        Debug.Log("Enemy died!");
    }

    private IEnumerator EnemyMovement()
    {

        if (timePassed < 1)
        {
            yield return new WaitForSeconds(1f);

            this.transform.position = new Vector3(enemy.position.x + 0.0015f, enemy.position.y, enemy.position.z);
            this.transform.localScale = new Vector3(-1, enemy.localScale.y, enemy.localScale.z);
            timePassed = timePassed + 1;
            

        }

        else
        {
            yield return new WaitForSeconds(1f);
            this.transform.position = new Vector3(enemy.position.x - 0.0015f, enemy.position.y, enemy.position.z);
            this.transform.localScale = new Vector3(1, enemy.localScale.y, enemy.localScale.z);
            timePassed = timePassed - 1;
            


        }
    }

}