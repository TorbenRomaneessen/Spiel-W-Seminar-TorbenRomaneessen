using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    //////////CharacterCharacteristics//////////
    [SerializeField]
    private int _currentHealthCharacter = 3;
    private const float _speed = 6f;
    private const float _jumpForce = 8f;

    private const float _attackrange = 1.1f;
    private const int _attackDamage = 1;
    private const float _attackRate = 2f;
    private float _nextAttackTime = 0f;

    private float _invincibleTimer;

    private float _dashTimer = 0f;
    private const float _dashRate = 2f;
    private const float _dashingVelocity = 20f;
    private const float _dashingTime = 0.1f;
    private Vector2 dashingDirection;

    [SerializeField]
    private GameObject[] hearts;
    public static bool PlayerDied;



    public static Character instance;


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
        _invincibleTimer += Time.deltaTime;

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
        transform.position += new Vector3(movementX, 0f, 0f) * Time.deltaTime * _speed;
    }


    void PlayerJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            animator.SetTrigger("TakeOf");
            isGrounded = false;
            rigidBody2D.AddForce(new Vector2(0f, _jumpForce), ForceMode2D.Impulse);
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
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, _attackrange, enemyLayers);

            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<Enemy>().TakeDamage(_attackDamage);
            }
        }
    }


    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, _attackrange);
    }


    private void AttackCooldown()
    {
        if (Time.time >= _nextAttackTime)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Attack();
                _nextAttackTime = Time.time + 1f / _attackRate;
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
        if ((CollisionWithThorns == true || CollisionWithEnemy == true) && _invincibleTimer >= 1)
        {
            FindObjectOfType<AudioManager>().Play("TakeDamageSound");
            animator.SetTrigger("TakeDamage");
            _currentHealthCharacter -= 1;

            Debug.Log("Player has been hit");
            _invincibleTimer = 0;
            transform.position = CheckPoint.ReachedPoint;
        }

        if (_currentHealthCharacter <= 0)
        {
            //animator.SetBool("Dead", true);
            Debug.Log("Playerdied = true");
            PlayerDied = true;
            ScoreManager.instance2.ChangeDeathCounter();
            _currentHealthCharacter = 3;
        }

    }


    private void Dash()
    {
        if(Input.GetButtonDown("Dash") && canDash && Time.time >= _dashTimer)
        {
            animator.SetTrigger("Dash");
            FindObjectOfType<AudioManager>().Play("DashSound");
            CreateDashTrail();
            isDashing = true;
            canDash = false;
            trailRenderer.emitting = true;
            dashingDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            if (dashingDirection == Vector2.zero)
            {
                dashingDirection = new Vector2(transform.localScale.x, 0);
            }

            StartCoroutine(nameof(StopDashing));
            _dashTimer = Time.time + 1f / _dashRate;
        }

        if (isDashing)
        {
            rigidBody2D.velocity = dashingDirection.normalized * _dashingVelocity;
            return;
        }
    }


    private IEnumerator StopDashing()
    {
        yield return new WaitForSeconds(_dashingTime);
        trailRenderer.emitting  = false;
        isDashing = false;
        rigidBody2D.velocity = dashingDirection.normalized * 0;
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