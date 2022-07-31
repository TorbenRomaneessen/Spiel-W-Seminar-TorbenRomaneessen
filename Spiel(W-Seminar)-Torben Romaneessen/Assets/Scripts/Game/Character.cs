using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    //////////CharacterCharacteristics//////////
    [SerializeField]
    public int _currentHealthCharacter = 3;
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
    public GameObject[] hearts;
    public static bool playerDied;



    public static Character instance;
    //public GameObject characters;


    //////////CharacterProperties//////////
    public Transform character;
    [SerializeField]
    private Rigidbody2D rigidBody2D;
    [SerializeField]
    private Animator animator;
    public Transform attackPoint;
    public LayerMask enemyLayers;
    private TrailRenderer trailRenderer;
    public ParticleSystem dashTrail;
    public ParticleSystem dust;



    private float movementX;

    //////////Boolean//////////
    private bool CollisionWithThorns;
    private bool CollisionWithEnemy;
    private bool isGrounded = false;
    private bool canDoubleJump;
    public bool isFlipped = false;
    private bool isDashing;
    private bool canDash;
    public bool levelPassed;

  
    public void Awake()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        character = GetComponent<Transform>();
        trailRenderer = GetComponent<TrailRenderer>();
    }


    void Update()
    {
        counter += Time.deltaTime;

        CharacterRun();
        AnimateWalk();
        PlayerJump();
        FlipCharacter();
        AttackCooldown();
        DamageingObjects();
        Dash();
        CheckHearts();
    }


    void CharacterRun()
    {
        movementX = Input.GetAxisRaw("Horizontal");
        transform.position += new Vector3(movementX, 0f, 0f) * Time.deltaTime * speed;
    }


    void PlayerJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            animator.SetTrigger("TakeOf");
            isGrounded = false;
            rigidBody2D.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }

        if (Input.GetButtonUp("Jump") && rigidBody2D.velocity.y > 0)
        {
            rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x,0);
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
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            canDash = true;
        }

        else
        {
            isGrounded = false;
        }

        if (collision.gameObject.CompareTag("Thorns"))
        {
            CollisionWithThorns = true;
            Debug.Log("Player has been hit");
        }

        else
        {
            CollisionWithThorns = false;
        }

        if(collision.gameObject.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);
        }

        if(collision.gameObject.CompareTag("Enemy"))
        {
            CollisionWithEnemy = true;
        }

        else
        {
            CollisionWithEnemy = false;
        }

        if(collision.gameObject.CompareTag("Platform"))
        {
            character.transform.parent = collision.transform;
            isGrounded = true;
            canDash = true;
        }
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            character.transform.parent = null;
        }
    }


    void AnimateWalk()
    {
        if (movementX > 0 && isGrounded)
        {
            animator.SetBool("Run", true);
            animator.SetBool("Jump", false);
            animator.SetBool("Idle", false);
            CreateDust();
        }

        if (movementX == 0 && isGrounded)
        {
            animator.SetBool("Idle", true);
            animator.SetBool("Jump", false);
            animator.SetBool("Run", false);
        }

        if (movementX < 0 && isGrounded)
        {
            animator.SetBool("Run", true);
            animator.SetBool("Jump", false);
            animator.SetBool("Idle", false);
            CreateDust();
        }
    }


    private void FlipCharacter()
    {
        if (movementX < 0)
        {
            //CreateDust();
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        else if (movementX > 0)
        {
            //CreateDust();
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }


    private void Attack()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetTrigger("Attack");
            FindObjectOfType<AudioManager>().Play("AttackSound (1)");
            FindObjectOfType<AudioManager>().Play("AttackSound (2)");
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
            if (Input.GetButtonDown("Fire1"))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }


    public void CheckHearts()
    {
        if(_currentHealthCharacter < 1)
        {
            hearts[0].gameObject.SetActive(false);
        }

        else if(_currentHealthCharacter < 2)
        {
            hearts[1].gameObject.SetActive(false);
        }

        else if (_currentHealthCharacter < 3)
        {
            hearts[2].gameObject.SetActive(false);
        }
    }


    public void DamageingObjects()
    {
        if ((CollisionWithThorns == true || CollisionWithEnemy == true) && counter >= 1)
        {
            FindObjectOfType<AudioManager>().Play("TakeDamageSound");
            animator.SetTrigger("TakeDamage");
            _currentHealthCharacter -= 1;

            Debug.Log("Player has been hit");
            counter = 0;
            transform.position = CheckPoint.ReachedPoint;
        }

        if (_currentHealthCharacter <= 0)
        {
            //animator.SetBool("Dead", true);
            Debug.Log("Playerdied = true");
            playerDied = true;
            ScoreManager.instance2.ChangeDeathCounter();
            _currentHealthCharacter = 3;
        }

    }


    private void Dash()
    {
        if(Input.GetButtonDown("Dash") && canDash && Time.time >= invincibleTime)
        {
            animator.SetTrigger("Dash");
            FindObjectOfType<AudioManager>().Play("DashSound");
            CreateDashTrail();
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

    private void CreateDashTrail()
    {
        dashTrail.Play();
    }

    private void CreateDust()
    {
        dust.Play();
    }
}