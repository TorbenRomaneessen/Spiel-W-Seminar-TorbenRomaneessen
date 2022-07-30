using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //////////EnemyCharacteristics//////////
    public int maxhealthenemy = 5;
    private int currenthealthenemy;
    private float timepassed = 0;

    //////////EnemyProperties//////////
    public Transform enemy;
    public GameObject turtle;
    private SpriteRenderer spriteRendererEnemy;
    public Animator animator;
  


    public void Awake()
    {
        enemy = GetComponent<Transform>();
        spriteRendererEnemy = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }


    void Start()
    {
        currenthealthenemy = maxhealthenemy;
    }


    private void Update()
    {
        StartCoroutine("EnemyMovement");
    }


    public void TakeDamage(int damage)
    {
        //this.transform.position = new Vector3(enemy.position.x + 0.5f, enemy.position.y, enemy.position.z);
        currenthealthenemy -= damage;
        animator.SetTrigger("Hurt");

        if (currenthealthenemy <= 0)
        {
            Die();
        }
    }


    void Die()
    {
        Debug.Log("Enemy died!");

        animator.SetBool("Dead", true);

        //Destroy(turtle);
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