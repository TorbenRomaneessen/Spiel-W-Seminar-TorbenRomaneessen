using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

<<<<<<< HEAD

    public int maxhealthenemy = 100;
    int currenthealthenemy;

=======
>>>>>>> parent of 1c1bfb5 (Spieler erleidet Schaden)
    public int maxHealth = 100;
    int currentHealth;

    public Transform enemy;
    SpriteRenderer spriterendererenemy;
    public Animator animator;
    private float timepassed = 0;

    public void Awake()
    {
        enemy = GetComponent<Transform>();
        spriterendererenemy = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
<<<<<<< HEAD

        currenthealthenemy = maxhealthenemy;

        currentHealth = maxHealth;

=======
        currentHealth = maxHealth;
>>>>>>> parent of 1c1bfb5 (Spieler erleidet Schaden)
    }

    private void Update()
    {
        StartCoroutine("enemymovement");
    }

    public void TakeDamage(int damage)
    {
        this.transform.position = new Vector3(enemy.position.x + 0.5f, enemy.position.y, enemy.position.z);
<<<<<<< HEAD
=======
        currentHealth -= damage;
>>>>>>> parent of 1c1bfb5 (Spieler erleidet Schaden)

        currenthealthenemy -= damage;

        currentHealth -= damage;


        animator.SetTrigger("hurt");


        if (currenthealthenemy <= 0)

        if (currentHealth <= 0)

<<<<<<< HEAD
=======
        if (currentHealth <= 0)
>>>>>>> parent of 1c1bfb5 (Spieler erleidet Schaden)
        {
            Die();
        }
    }

    void Die()
    {

        Debug.Log("enemy died!");

        animator.SetBool("dead", true);

        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }




    private IEnumerator EnemyMovement()
    {

        if (timepassed < 1)
        {
            yield return new WaitForSeconds(1f);

            this.transform.position = new Vector3(enemy.position.x + 0.0015f, enemy.position.y, enemy.position.z);
            this.transform.localScale = new Vector3(-1, enemy.localScale.y, enemy.localScale.z);
            timepassed = timepassed + 1;


        }

        else
        {
            yield return new WaitForSeconds(1f);
            this.transform.position = new Vector3(enemy.position.x - 0.0015f, enemy.position.y, enemy.position.z);
            this.transform.localScale = new Vector3(1, enemy.localScale.y, enemy.localScale.z);
            timepassed = timepassed - 1;



        }
    }

}