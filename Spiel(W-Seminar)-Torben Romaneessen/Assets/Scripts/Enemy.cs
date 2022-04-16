using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int maxhealthenemy = 100;
    int currenthealthenemy;

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
        currenthealthenemy = maxhealthenemy;
    }

    private void Update()
    {
        StartCoroutine("enemymovement");
    }

    public void TakeDamage(int damage)
    {
        this.transform.position = new Vector3(enemy.position.x + 0.5f, enemy.position.y, enemy.position.z);
        currenthealthenemy -= damage;

        animator.SetTrigger("hurt");

        if (currenthealthenemy <= 0)
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