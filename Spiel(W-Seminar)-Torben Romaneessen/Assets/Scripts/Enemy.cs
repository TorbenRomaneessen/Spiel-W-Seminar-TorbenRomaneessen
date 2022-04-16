using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD

    public int maxhealthenemy = 100;
    int currenthealthenemy;
=======
    public int maxHealthEnemy = 100;
    int currentHealthEnemy;
>>>>>>> parent of 7c097d5 (Versuch Fehler CS0246 zu beheben)

=======
>>>>>>> parent of 1c1bfb5 (Spieler erleidet Schaden)
=======
>>>>>>> parent of 1c1bfb5 (Spieler erleidet Schaden)
    public int maxHealth = 100;
    int currentHealth;

    public Transform enemy;
<<<<<<< HEAD
<<<<<<< HEAD
    SpriteRenderer spriterendererenemy;
    public Animator animator;
    private float timepassed = 0;
=======
    SpriteRenderer spriteRendererEnemy;
    private float timePassed = 0;
>>>>>>> parent of 9bcfaff (Turtle-Die/Hurt Animation)
=======
    SpriteRenderer spriteRendererEnemy;
    public Animator animator;
    private float timePassed = 0;
>>>>>>> parent of 7c097d5 (Versuch Fehler CS0246 zu beheben)
=======
    public int maxHealthEnemy = 100;
    int currentHealthEnemy;
=======
    public int maxHealth = 100;
    int currentHealth;
>>>>>>> parent of 1c1bfb5 (Spieler erleidet Schaden)

    public Transform enemy;
    SpriteRenderer spriteRendererEnemy;
    public Animator animator;
    private float timePassed = 0;
>>>>>>> parent of 7c097d5 (Versuch Fehler CS0246 zu beheben)
=======
    public int maxHealthEnemy = 100;
    int currentHealthEnemy;

    public Transform enemy;
    SpriteRenderer spriteRendererEnemy;
    private float timePassed = 0;
>>>>>>> parent of 7c097d5 (Versuch Fehler CS0246 zu beheben)
=======
    public int maxHealthEnemy = 100;
    int currentHealthEnemy;

    public Transform enemy;
    SpriteRenderer spriteRendererEnemy;
    public Animator animator;
    private float timePassed = 0;
>>>>>>> parent of 7c097d5 (Versuch Fehler CS0246 zu beheben)

    public void Awake()
    {
        enemy = GetComponent<Transform>();
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
        spriterendererenemy = GetComponent<SpriteRenderer>();
=======
        spriteRendererEnemy = GetComponent<SpriteRenderer>();
>>>>>>> parent of 7c097d5 (Versuch Fehler CS0246 zu beheben)
=======
        spriteRendererEnemy = GetComponent<SpriteRenderer>();
<<<<<<< HEAD
>>>>>>> parent of 7c097d5 (Versuch Fehler CS0246 zu beheben)
=======
        spriteRendererEnemy = GetComponent<SpriteRenderer>();
>>>>>>> parent of 7c097d5 (Versuch Fehler CS0246 zu beheben)
=======
        spriteRendererEnemy = GetComponent<SpriteRenderer>();
>>>>>>> parent of 7c097d5 (Versuch Fehler CS0246 zu beheben)
        animator = GetComponent<Animator>();
=======
        spriteRendererEnemy = GetComponent<SpriteRenderer>();
>>>>>>> parent of 9bcfaff (Turtle-Die/Hurt Animation)
=======
>>>>>>> parent of 9bcfaff (Turtle-Die/Hurt Animation)
    }

    void Start()
    {
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD

        currenthealthenemy = maxhealthenemy;

        currentHealth = maxHealth;

=======
        currentHealth = maxHealth;
>>>>>>> parent of 1c1bfb5 (Spieler erleidet Schaden)
=======
        currentHealth = maxHealth;
>>>>>>> parent of 1c1bfb5 (Spieler erleidet Schaden)
=======
        currentHealthEnemy = maxHealthEnemy;
>>>>>>> parent of 7c097d5 (Versuch Fehler CS0246 zu beheben)
=======
        currentHealthEnemy = maxHealthEnemy;
>>>>>>> parent of 7c097d5 (Versuch Fehler CS0246 zu beheben)
=======
        currentHealthEnemy = maxHealthEnemy;
>>>>>>> parent of 7c097d5 (Versuch Fehler CS0246 zu beheben)
=======
        currentHealthEnemy = maxHealthEnemy;
>>>>>>> parent of 7c097d5 (Versuch Fehler CS0246 zu beheben)
=======
        currentHealth = maxHealth;
>>>>>>> parent of 1c1bfb5 (Spieler erleidet Schaden)
    }

    private void Update()
    {
<<<<<<< HEAD
        EnemyMovement();
=======
        StartCoroutine("EnemyMovement");
>>>>>>> parent of 7c097d5 (Versuch Fehler CS0246 zu beheben)
    }

    public void TakeDamage(int damage)
    {
<<<<<<< HEAD
<<<<<<< HEAD
        this.transform.position = new Vector3(enemy.position.x + 0.5f, enemy.position.y, enemy.position.z);
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
=======
=======
>>>>>>> parent of 9bcfaff (Turtle-Die/Hurt Animation)
        currentHealth -= damage;
>>>>>>> parent of 1c1bfb5 (Spieler erleidet Schaden)

<<<<<<< HEAD
        currenthealthenemy -= damage;

        currentHealth -= damage;

=======
=======
>>>>>>> parent of 9bcfaff (Turtle-Die/Hurt Animation)
        currentHealth -= damage;
>>>>>>> parent of 1c1bfb5 (Spieler erleidet Schaden)

        animator.SetTrigger("hurt");

<<<<<<< HEAD

<<<<<<< HEAD
        if (currenthealthenemy <= 0)

=======
>>>>>>> parent of 9bcfaff (Turtle-Die/Hurt Animation)
=======
>>>>>>> parent of 9bcfaff (Turtle-Die/Hurt Animation)
        if (currentHealth <= 0)

<<<<<<< HEAD
=======
        if (currentHealth <= 0)
>>>>>>> parent of 1c1bfb5 (Spieler erleidet Schaden)
=======
        if (currentHealth <= 0)
>>>>>>> parent of 1c1bfb5 (Spieler erleidet Schaden)
=======
        currentHealthEnemy -= damage;

        animator.SetTrigger("Hurt");

        if (currentHealthEnemy <= 0)
>>>>>>> parent of 7c097d5 (Versuch Fehler CS0246 zu beheben)
=======
        currentHealthEnemy -= damage;

        animator.SetTrigger("Hurt");

        if (currentHealthEnemy <= 0)
>>>>>>> parent of 7c097d5 (Versuch Fehler CS0246 zu beheben)
=======
        currentHealthEnemy -= damage;

        animator.SetTrigger("Hurt");

        if (currentHealthEnemy <= 0)
>>>>>>> parent of 7c097d5 (Versuch Fehler CS0246 zu beheben)
=======
        currentHealthEnemy -= damage;

        animator.SetTrigger("Hurt");

        if (currentHealthEnemy <= 0)
>>>>>>> parent of 7c097d5 (Versuch Fehler CS0246 zu beheben)
=======
        currentHealth -= damage;

        animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
>>>>>>> parent of 1c1bfb5 (Spieler erleidet Schaden)
        {
            Die();
        }
    }

    void Die()
    {

<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
        Debug.Log("enemy died!");
=======
        Debug.Log("Enemy died!");
>>>>>>> parent of 7c097d5 (Versuch Fehler CS0246 zu beheben)
=======
        Debug.Log("Enemy died!");
<<<<<<< HEAD
>>>>>>> parent of 7c097d5 (Versuch Fehler CS0246 zu beheben)
=======
        Debug.Log("Enemy died!");
>>>>>>> parent of 7c097d5 (Versuch Fehler CS0246 zu beheben)
=======
        Debug.Log("Enemy died!");
>>>>>>> parent of 7c097d5 (Versuch Fehler CS0246 zu beheben)

        animator.SetBool("Dead", true);

        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD



=======
        Debug.Log("Enemy died!");
    }
>>>>>>> parent of 9bcfaff (Turtle-Die/Hurt Animation)
=======
      
     
    
>>>>>>> parent of 7c097d5 (Versuch Fehler CS0246 zu beheben)
=======
      
     
    
>>>>>>> parent of 7c097d5 (Versuch Fehler CS0246 zu beheben)
=======
      
     
    
>>>>>>> parent of 7c097d5 (Versuch Fehler CS0246 zu beheben)
=======
    }
>>>>>>> parent of 9bcfaff (Turtle-Die/Hurt Animation)
=======
      
     
    
>>>>>>> parent of 7c097d5 (Versuch Fehler CS0246 zu beheben)

    private IEnumerator EnemyMovement()
    {

        if (timePassed < 1)
        {
            yield return new WaitForSeconds(1f);

<<<<<<< HEAD
            this.transform.position = new Vector3(enemy.position.x + 0.0025f, enemy.position.y, enemy.position.z);
=======
            this.transform.position = new Vector3(enemy.position.x + 0.0015f, enemy.position.y, enemy.position.z);
            this.transform.localScale = new Vector3(-1, enemy.localScale.y, enemy.localScale.z);
>>>>>>> parent of 7c097d5 (Versuch Fehler CS0246 zu beheben)
            timePassed = timePassed + 1;
            

        }

        else
        {
            yield return new WaitForSeconds(1f);
<<<<<<< HEAD
            this.transform.position = new Vector3(enemy.position.x - 0.0025f, enemy.position.y, enemy.position.z);
            timePassed = timePassed - 1;
            this.transform.localScale = new Vector3(- enemy.localScale.x, enemy.localScale.y, enemy.localScale.z);
=======
            this.transform.position = new Vector3(enemy.position.x - 0.0015f, enemy.position.y, enemy.position.z);
            this.transform.localScale = new Vector3(1, enemy.localScale.y, enemy.localScale.z);
            timePassed = timePassed - 1;
            
>>>>>>> parent of 7c097d5 (Versuch Fehler CS0246 zu beheben)


        }
    }

}