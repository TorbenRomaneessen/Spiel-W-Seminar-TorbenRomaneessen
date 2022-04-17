using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    //////////CharacterCharacteristics//////////
    [SerializeField]
    int currentHealthPlayer = 5;
    [SerializeField]
    private float speed = 10f;
    [SerializeField]
    private float jumpForce = 10f;
    public float attackrange = 0.5f;
    public int attackDamage = 1;
    public float attackRate = 2f;
    private float nextAttackTime = 0f;
    private float counter;

    //////////CharacterProperties//////////
    public Transform character;
    [SerializeField]
    private Rigidbody2D rigidBody2D;
    [SerializeField]
    private Animator animator;
    public Transform attackPoint;
    public LayerMask enemyLayers;

    //////////Animations//////////
    private string runAnimation = "Run";
    private string jumpAnimation = "Jump";
    private string idleAnimation = "Idle";
    private string attackAnimation = "Attack";
    private string takeDamageAnimation = "TakeDamage";

    //////////Tags//////////
    private string ThornsTag = "Thorns";
    private string groundTag = "Ground";

    private float movementX;

    //////////Boolean//////////
    private bool CollisionWithThorns;
    private bool isGrounded = false;
    private bool canDoubleJump;
    public bool isFlipped = false;

  

    public void Awake()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        character = GetComponent<Transform>();
    }


    void Start()
    {

    }


    void Update()
    {
        counter += Time.deltaTime;

        CharacterRun();
        AnimateWalk();
        DoubleJump();
        PlayerJump();
        FlipCharacter();
        AttackCooldown();
        DamageingObjects();
    }


    private void FixedUpdate()
    {

    }


    void CharacterRun()
    {
        movementX = Input.GetAxisRaw("Horizontal");
        transform.position += new Vector3(movementX, 0f, 0f) * Time.deltaTime * speed;
    }


    private void DoubleJump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                PlayerJump();
                canDoubleJump = true;
            }

            else if (canDoubleJump)
            {
                PlayerJump();
                canDoubleJump = false;
            }
        }
    }


    void PlayerJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            animator.SetTrigger("TakeOf");
            isGrounded = false;
            rigidBody2D.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }

        if (isGrounded == true)
        {
            animator.SetBool("Jump", false);
        }

        else
        {
            animator.SetBool("Jump", true);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(groundTag))
        {
            isGrounded = true;
        }

        else
        {
            isGrounded = false;
        }

        if (collision.gameObject.CompareTag(ThornsTag))
        {
            CollisionWithThorns = true;
            Debug.Log("Player has been hit");
        }

        else
        {
            CollisionWithThorns = false;
        }
    }


    void AnimateWalk()
    {
        if (movementX > 0 && isGrounded)
        {
            animator.SetBool(runAnimation, true);
            animator.SetBool(jumpAnimation, false);
            animator.SetBool(idleAnimation, false);
        }

        if (movementX == 0 && isGrounded)
        {
            animator.SetBool(idleAnimation, true);
            animator.SetBool(jumpAnimation, false);
            animator.SetBool(runAnimation, false);
        }

        if (movementX < 0 && isGrounded)
        {
            animator.SetBool(runAnimation, true);
            animator.SetBool(jumpAnimation, false);
            animator.SetBool(idleAnimation, false);
        }
    }


    private void FlipCharacter()
    {
        if (movementX < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        else if (movementX > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }


    private void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger(attackAnimation);
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackrange, enemyLayers);

            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
            }
        }
    }


    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackrange);
    }


    private void AttackCooldown()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }


    public void DamageingObjects()
    {
        if (CollisionWithThorns == true && counter >= 2)
        {
            animator.SetTrigger(takeDamageAnimation);
            currentHealthPlayer -= 1;

            Debug.Log("Player has been hit");
            counter = 0;
            this.transform.position = new Vector3(character.position.x - 1f, character.position.y + 7f, character.position.z);
        }

        if (currentHealthPlayer <= 0)
        {
            Die();
        }
    }


    private void Die()
    {
        Debug.Log("Character died!");

        //animator.SetBool("Dead", true);

        this.transform.position = new Vector3(-35, 5, 0);
        currentHealthPlayer = 5;
    }
}

// control k + c und control k + u alles aukommentieren
