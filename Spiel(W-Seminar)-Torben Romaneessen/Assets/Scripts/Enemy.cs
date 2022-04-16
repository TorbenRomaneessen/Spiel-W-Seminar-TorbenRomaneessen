using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxhealthenemy = 100;
    int currentHealthenemy;


    public Transform enemy;

    SpriteRenderer spriteRendererEnemy;
    public Animator animator;
    private float timepassed = 0;

   

    public int maxHealthEnemy = 100;
    int currentHealthEnemy; 

    public void Awake()
    {
        enemy = GetComponent<Transform>();

        spriteRendererEnemy = GetComponent<SpriteRenderer>();

        animator = GetComponent<Animator>();

    }

    void Start()
    {


        currentHealthenemy = maxhealthenemy;


    }

    private void Update()
    {

        EnemyMovement();

        StartCoroutine("EnemyMovement");

    }

    public void TakeDamage(int damage)
    {

        this.transform.position = new Vector3(enemy.position.x + 0.5f, enemy.position.y, enemy.position.z);

        currentHealthenemy -= damage;


        animator.SetTrigger("hurt");

        if (currentHealthenemy <= 0)
        {
            Die();
        }
    }

    void Die()
    {

        Debug.Log("enemy died!");

        animator.SetBool("Dead", true);

        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }


    private IEnumerator EnemyMovement()
    {

        if (timepassed < 1)
        {
            yield return new WaitForSeconds(1f);

            this.transform.position = new Vector3(enemy.position.x + 0.0025f, enemy.position.y, enemy.position.z);

            this.transform.position = new Vector3(enemy.position.x + 0.0015f, enemy.position.y, enemy.position.z);
            this.transform.localScale = new Vector3(-1, enemy.localScale.y, enemy.localScale.z);

            timepassed = timepassed + 1;
            

        }

        else
        {
            yield return new WaitForSeconds(1f);

            this.transform.position = new Vector3(enemy.position.x - 0.0025f, enemy.position.y, enemy.position.z);
            timepassed = timepassed - 1;
            this.transform.localScale = new Vector3(- enemy.localScale.x, enemy.localScale.y, enemy.localScale.z);


        }
    }

}