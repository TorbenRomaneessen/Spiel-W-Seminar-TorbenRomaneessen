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
    private float invincibleTime = 0f;
    private float dashRate = 2f;
    private float dashingVelocity = 20f;
    private float dashingTime = 0.1f;
    private Vector2 dashingDir;


    //////////CharacterProperties//////////
    public Transform character;
    [SerializeField]
    private Rigidbody2D rigidBody2D;
    [SerializeField]
    private Animator animator;
    public Transform attackPoint;
    public LayerMask enemyLayers;
    private TrailRenderer trailRenderer;

    //////////Animations//////////
    private string runAnimation = "Run";
    private string jumpAnimation = "Jump";
    private string idleAnimation = "Idle";
    private string attackAnimation = "Attack";
    private string takeDamageAnimation = "TakeDamage";
    private string dashAnimation = "Dash";

    //////////Tags//////////
    private string ThornsTag = "Thorns";
    private string groundTag = "Ground";
    private string coinTag = "Coin";
    private string enemyTag = "Enemy";

    private float movementX;

    //////////Boolean//////////
    private bool CollisionWithThorns;
    private bool CollisionWithEnemy;
    private bool isGrounded = false;
    private bool canDoubleJump;
    public bool isFlipped = false;
    private bool isDashing;
    private bool canDash;

  

    public void Awake()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        character = GetComponent<Transform>();
        trailRenderer = GetComponent<TrailRenderer>();
    }


    void Start()
    {

    }


    void Update()
    {
        counter += Time.deltaTime;

        CharacterRun();
        AnimateWalk();
        //DoubleJump();
        PlayerJump();
        FlipCharacter();
        AttackCooldown();
        DamageingObjects();
        Dash();
    }


    private void FixedUpdate()
    {

    }


    void CharacterRun()
    {
        movementX = Input.GetAxisRaw("Horizontal");
        transform.position += new Vector3(movementX, 0f, 0f) * Time.deltaTime * speed;
    }


    //private void DoubleJump()
    //{
    //    if (Input.GetButtonDown("Jump"))
    //    {
    //        if (isGrounded)
    //        {
    //            PlayerJump();
    //            canDoubleJump = true;
    //        }

    //        else if (canDoubleJump)
    //        {
    //            PlayerJump();
    //            canDoubleJump = false;
    //        }
    //    }
    //}


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
            canDash = true;
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

        if(collision.gameObject.CompareTag(coinTag))
        {
            Destroy(collision.gameObject);
        }

        if(collision.gameObject.CompareTag(enemyTag))
        {
            CollisionWithEnemy = true;
        }

        else
        {
            CollisionWithEnemy = false;
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
        if ((CollisionWithThorns == true || CollisionWithEnemy == true) && counter >= 2)
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


    private void Dash()
    {
        if(Input.GetButtonDown("Dash") && canDash && Time.time >= invincibleTime)
        {
            animator.SetTrigger(dashAnimation);
            isDashing = true;
            canDash = false;
            trailRenderer.emitting = true;
            dashingDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            if (dashingDir == Vector2.zero)
            {
                dashingDir = new Vector2(transform.localScale.x, 0);
            }

            StartCoroutine("StopDashing");
            invincibleTime  = Time.time + 1f / dashRate;
        }

        if (isDashing)
        {
            rigidBody2D.velocity = dashingDir.normalized * dashingVelocity;
            return;
        }
    }


    private IEnumerator StopDashing()
    {
        yield return new WaitForSeconds(dashingTime);
        trailRenderer.emitting  = false;
        isDashing = false;
        rigidBody2D.velocity = dashingDir.normalized * 0;
    }
}

// control k + c und control k + u alles aukommentieren
