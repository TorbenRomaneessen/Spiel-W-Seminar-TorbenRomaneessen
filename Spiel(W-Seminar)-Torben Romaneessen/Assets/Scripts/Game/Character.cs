using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("Characteristics")]
    [SerializeField]
    private int _currentHealth = 3;
    private const float Speed = 6f;
    private const float JumpForce = 8f;

    [Header("Attack")]
    private const float AttackRange = 1.1f;
    private const int AttackDamage = 1;
    private const float AttackRate = 2f;
    private float _nextAttackTime = 0f;
    [SerializeField]
    private Transform _attackPoint;
    [SerializeField]
    private LayerMask _enemyLayers;

    private float _invincibleTimer;

    [Header("Dash")]
    private float _dashTimer = 0f;
    private const float DashingRate = 2f;
    private const float DashingVelocity = 20f;
    private const float DashingTime = 0.1f;
    private bool _isDashing;
    private bool _canDash;
    private Vector2 _dashingDirection;

    [Header("Character Components")]
    [SerializeField]
    private GameObject[] hearts;
    private Transform _transform;
    private Rigidbody2D _rigidBody2D;
    private Animator _animator;
    [SerializeField]
    private ParticleSystem _dashTrail;
    [SerializeField]
    private ParticleSystem _dust;

    public static bool IsDead;



    private float _movementX;

    private bool _collisionWithThorns;
    private bool _collisionWithEnemy;
    private bool _isGrounded = false;


  
    public void Awake()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _transform = GetComponent<Transform>();
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
        _movementX = Input.GetAxisRaw("Horizontal");
        transform.position += new Vector3(_movementX, 0f, 0f) * Time.deltaTime * Speed;
    }


    void PlayerJump()
    {
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _animator.SetTrigger("TakeOf");
            _isGrounded = false;
            _rigidBody2D.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
        }

        if (Input.GetButtonUp("Jump") && _rigidBody2D.velocity.y > 0)
        {
            _rigidBody2D.velocity = new Vector2(_rigidBody2D.velocity.x,0);
        }

        if (_isGrounded == true)
        {
            _animator.SetBool("Jump", false);
        }

        else
        {
            _animator.SetBool("Jump", true);
        }
    }

 

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = true;
            _canDash = true;
        }

        else
        {
            _isGrounded = false;
        }

        if (collision.gameObject.CompareTag("Thorns"))
        {
            _collisionWithThorns = true;
            Debug.Log("Player has been hit");
        }

        else
        {
            _collisionWithThorns = false;
        }

        if(collision.gameObject.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);
        }

        if(collision.gameObject.CompareTag("Enemy"))
        {
            _collisionWithEnemy = true;
        }

        else
        {
            _collisionWithEnemy = false;
        }

        if(collision.gameObject.CompareTag("Platform"))
        {
            _transform.parent = collision.transform;
            _isGrounded = true;
            _canDash = true;
        }
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            _transform.parent = null;
        }
    }


    void AnimateWalk()
    {
        if (_movementX > 0 && _isGrounded)
        {
            _animator.SetBool("Run", true);
            _animator.SetBool("Jump", false);
            _animator.SetBool("Idle", false);
            CreateDust();
        }

        if (_movementX == 0 && _isGrounded)
        {
            _animator.SetBool("Idle", true);
            _animator.SetBool("Jump", false);
            _animator.SetBool("Run", false);
        }

        if (_movementX < 0 && _isGrounded)
        {
            _animator.SetBool("Run", true);
            _animator.SetBool("Jump", false);
            _animator.SetBool("Idle", false);
            CreateDust();
        }
    }


    private void FlipCharacter()
    {
        if (_movementX < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        else if (_movementX > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }


    private void Attack()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            _animator.SetTrigger("Attack");
            FindObjectOfType<AudioManager>().Play("AttackSound (1)");
            FindObjectOfType<AudioManager>().Play("AttackSound (2)");
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_attackPoint.position, AttackRange, _enemyLayers);

            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<Enemy>().TakeDamage(AttackDamage);
            }
        }
    }


    private void OnDrawGizmosSelected()
    {
        if (_attackPoint == null)
            return;
        Gizmos.DrawWireSphere(_attackPoint.position, AttackRange);
    }


    private void AttackCooldown()
    {
        if (Time.time >= _nextAttackTime)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Attack();
                _nextAttackTime = Time.time + 1f / AttackRate;
            }
        }
    }


    public void CheckHearts()
    {
        if(_currentHealth < 1)
        {
            hearts[0].gameObject.SetActive(false);
        }

        else if(_currentHealth < 2)
        {
            hearts[1].gameObject.SetActive(false);
        }

        else if (_currentHealth < 3)
        {
            hearts[2].gameObject.SetActive(false);
        }
    }


    public void DamageingObjects()
    {
        if ((_collisionWithThorns == true || _collisionWithEnemy == true) && _invincibleTimer >= 1)
        {
            FindObjectOfType<AudioManager>().Play("TakeDamageSound");
            _animator.SetTrigger("TakeDamage");
            _currentHealth -= 1;

            Debug.Log("Player has been hit");
            _invincibleTimer = 0;
            transform.position = CheckPoint.ReachedPoint;
        }

        if (_currentHealth <= 0)
        {
            Debug.Log("Playerdied = true");
            IsDead = true;
            ScoreManager.instance2.ChangeDeathCounter();
            _currentHealth = 3;
        }

    }


    private void Dash()
    {
        if(Input.GetButtonDown("Dash") && _canDash && Time.time >= _dashTimer)
        {
            _animator.SetTrigger("Dash");
            FindObjectOfType<AudioManager>().Play("DashSound");
            CreateDashTrail();
            _isDashing = true;
            _canDash = false;
            _dashingDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            if (_dashingDirection == Vector2.zero)
            {
                _dashingDirection = new Vector2(transform.localScale.x, 0);
            }

            StartCoroutine(nameof(StopDashing));
            _dashTimer = Time.time + 1f / DashingRate;
        }

        if (_isDashing)
        {
            _rigidBody2D.velocity = _dashingDirection.normalized * DashingVelocity;
            return;
        }
    }


    private IEnumerator StopDashing()
    {
        yield return new WaitForSeconds(DashingTime);
        _isDashing = false;
        _rigidBody2D.velocity = _dashingDirection.normalized * 0;
    }

    private void CreateDashTrail()
    {
        _dashTrail.Play();
    }

    private void CreateDust()
    {
        _dust.Play();
    }
}