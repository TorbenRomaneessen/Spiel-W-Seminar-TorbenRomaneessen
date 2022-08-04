using System.Collections;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("Characteristics")]
    [SerializeField]
    private int _currentHealth = 3;
    private const float Speed = 6f;
    private float _movementX;
    public bool CanMove;
    private const float JumpForce = 8f;
    public static bool IsDead;
    private float _invincibleTimer;

    [Header("Character Components")]
    [SerializeField]
    private GameObject[] _hearts;
    private Transform _transform;
    private Rigidbody2D _rigidBody2D;
    private Animator _animator;
    [SerializeField]
    private ParticleSystem _dashTrail;
    [SerializeField]
    private ParticleSystem _dust;

    [Header("Attack")]
    private float _nextAttackTime;
    private const float AttackRate = 2f;
    private const float AttackRange = 1.1f;
    private const int AttackDamage = 1;
    [SerializeField]
    private Transform _attackPoint;
    [SerializeField]
    private LayerMask _enemyLayers;

    [Header("Dash")]
    private float _nextDashTime;
    private const float DashingRate = 2f;
    private const float DashingVelocity = 20f;
    private const float DashingTime = 0.1f;
    private bool _isDashing;
    private bool _canDash;
    private Vector2 _dashingDirection;

    [Header("Collision")]
    private bool _takeDamage;
    private bool _isGrounded;


    private void Awake()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _transform = GetComponent<Transform>();
    }

    private void Update()
    {
        Run();
        AnimateRun();
        TurnAround();
        Jump();
        AttackCooldown();
        Dash();
        TakeDamage();
        CheckHearts();
    }


    private void Run()
    {
        if (LevelCanvas.Instance.GameIsPaused == false && LevelCanvas.Instance.GameIsOver == false && LevelCanvas.Instance.LevelIsCompleted == false)
        {
            _movementX = Input.GetAxisRaw("Horizontal");
            _transform.position += new Vector3(_movementX, 0f, 0f) * Time.deltaTime * Speed;
        }

        else
        {
            _canDash = false;
        }
    }

    private void AnimateRun()
    {
        if (_movementX > 0 && _isGrounded)
        {
            _animator.SetBool("Run", true);
            _animator.SetBool("Idle", false);
            CreateDust();
        }

        if (_movementX == 0 && _isGrounded)
        {
            _animator.SetBool("Idle", true);
            _animator.SetBool("Run", false);
        }

        if (_movementX < 0 && _isGrounded)
        {
            _animator.SetBool("Run", true);
            _animator.SetBool("Idle", false);
            CreateDust();
        }
    }

    private void TurnAround()
    {
        if (_movementX < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        if (_movementX > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }


    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _animator.SetTrigger("TakeOf");
            _rigidBody2D.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
            _isGrounded = false;
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


    private void Attack()
    {
        // "Fire1" is the name Unity assigned this button.
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

    private void OnDrawGizmosSelected()
    {
        if (_attackPoint == null)
            return;
        Gizmos.DrawWireSphere(_attackPoint.position, AttackRange);
    }

    private void Dash()
    {
        if (Input.GetButtonDown("Dash") && _canDash && Time.time >= _nextDashTime)
        {
            _animator.SetTrigger("Dash");
            FindObjectOfType<AudioManager>().Play("DashSound");
            CreateDashTrail();

            _isDashing = true;
            _canDash = false;
            _dashingDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            if (_dashingDirection == Vector2.zero )
            {
                if (_transform.rotation.y != 0)
                {
                    _dashingDirection = new Vector2(-1, 0);
                }

                else
                {
                    _dashingDirection = new Vector2(1, 0);
                }
            }

            StartCoroutine(nameof(StopDash));
            _nextDashTime = Time.time + 1f / DashingRate;
        }

        if (_isDashing)
        {
            _rigidBody2D.velocity = _dashingDirection.normalized * DashingVelocity;
            return;
        }
    }

    private IEnumerator StopDash()
    {
        yield return new WaitForSeconds(DashingTime);
        _isDashing = false;
        _rigidBody2D.velocity = _dashingDirection.normalized * 0;
    }


    private void TakeDamage()
    {
        _invincibleTimer += Time.deltaTime;

        if (_takeDamage == true && _invincibleTimer >= 1)
        {
            _animator.SetTrigger("TakeDamage");
            FindObjectOfType<AudioManager>().Play("TakeDamageSound");

            _currentHealth -= 1;
            _invincibleTimer = 0;
            transform.position = CheckPoint.ReachedPoint;
        }

        if (_currentHealth <= 0)
        {
            ScoreManager.Instance.ChangeDeathCounter();
            IsDead = true;
            _currentHealth = 3;
        }
    }

    private void CheckHearts()
    {
        if (_currentHealth < 1)
        {
            _hearts[0].gameObject.SetActive(false);
        }

        else if (_currentHealth < 2)
        {
            _hearts[1].gameObject.SetActive(false);
        }

        else if (_currentHealth < 3)
        {
            _hearts[2].gameObject.SetActive(false);
        }
    }


    private void CreateDashTrail()
    {
        _dashTrail.Play();
    }

    private void CreateDust()
    {
        _dust.Play();
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

        // On entering the platform the character moves along the platform. Otherwise the character would fall off the platform.
        if (collision.gameObject.CompareTag("Platform"))
        {
            _transform.parent = collision.transform;
            _isGrounded = true;
            _canDash = true;
        }

        if (collision.gameObject.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Thorns") || collision.gameObject.CompareTag("Enemy"))
        {
            _takeDamage = true;
        }

        else
        {
            _takeDamage = false;
        }
    }

    // On exiting the platform the characters movements are independent of the platforms movements.
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            _transform.parent = null;
        }
    }
}