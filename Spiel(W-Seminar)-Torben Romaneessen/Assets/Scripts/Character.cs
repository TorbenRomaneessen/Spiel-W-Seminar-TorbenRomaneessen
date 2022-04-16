using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    private float speed = 10f;

    //public PlayerHearts = 5f;
    public CharacterHearts CharacterHearts;

    [SerializeField]
    private float jumpForce = 10f;

    //private float dashForce = 2.2f;

    private float movementX;

    public Transform character;

    [SerializeField]
    private Rigidbody2D rigidBody2D;

    //[SerializeField]
    //private SpriteRenderer spriteRenderer;

    [SerializeField]
    private Animator animator;
    private string runAnimation = "Run";
    private string jumpAnimation = "Jump";
    private string idleAnimation = "Idle";
    private string attackAnimation = "Attack";
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
    //private string dashAnimation = "Dash";
<<<<<<< HEAD

    private string takeDamageAnimation = "TakeDamage";
=======
>>>>>>> parent of 7c097d5 (Versuch Fehler CS0246 zu beheben)

    //private string dashAnimation = "Dash";
=======
>>>>>>> parent of 1c1bfb5 (Spieler erleidet Schaden)
=======
>>>>>>> parent of 7c097d5 (Versuch Fehler CS0246 zu beheben)
=======
>>>>>>> parent of 7c097d5 (Versuch Fehler CS0246 zu beheben)
=======
>>>>>>> parent of 7c097d5 (Versuch Fehler CS0246 zu beheben)
=======
    //private string dashAnimation = "Dash";
>>>>>>> parent of 1c1bfb5 (Spieler erleidet Schaden)
=======
>>>>>>> parent of 7c097d5 (Versuch Fehler CS0246 zu beheben)

    private bool isGrounded = false;
    private string groundTag = "Ground";

    private bool canDoubleJump;

    public Transform attackPoint;
    public float attackrange = 0.5f;
    public LayerMask enemyLayers;
    public int attackDamage = 40;

    public float attackRate = 2f;
    private float nextAttackTime = 0f;

    public bool isFlipped = false;



    public void Awake()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();

        //spriteRenderer = GetComponent<SpriteRenderer>();

        animator = GetComponent<Animator>();

        character = GetComponent<Transform>();

        CharacterHearts CharacterHearts = new CharacterHearts(5);

    }

    void Start()
    {

    }


    void Update()
    {
        CharacterRun();
        AnimateWalk();
        //AnimateJump();
        DoubleJump();
        PlayerJump();
        FlipCharacter();
<<<<<<< HEAD
        AttackCooldown();
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD

<<<<<<< HEAD
<<<<<<< HEAD
        DamageingObjects();
=======
>>>>>>> parent of 1c1bfb5 (Spieler erleidet Schaden)
=======
>>>>>>> parent of 1c1bfb5 (Spieler erleidet Schaden)
        //StartCoroutine(Dash());
=======
        DamageingObjects();      
>>>>>>> parent of 7c097d5 (Versuch Fehler CS0246 zu beheben)
=======
        DamageingObjects();      
>>>>>>> parent of 7c097d5 (Versuch Fehler CS0246 zu beheben)
=======
        DamageingObjects();      
>>>>>>> parent of 7c097d5 (Versuch Fehler CS0246 zu beheben)
=======
        Attack();
        
        /*if (Time.time >= nextAttackTime)
        {
            if (Input.GetMouseButtonDown(0))
            {
                //Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }*/
=======
=======
>>>>>>> parent of 7c097d5 (Versuch Fehler CS0246 zu beheben)
        DamageingObjects();      
    }
>>>>>>> parent of 7c097d5 (Versuch Fehler CS0246 zu beheben)

        //StartCoroutine(Dash());



>>>>>>> parent of 0e35c16 (Weitere Turtle Sachen)
=======

        //StartCoroutine(Dash());
>>>>>>> parent of 1c1bfb5 (Spieler erleidet Schaden)
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
        if (Input.GetButtonDown("Jump"))//if(Input.GetKeyDown(KeyCode.Space))
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

            rigidBody2D.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);//rigidBody2D.velocity = Vector2.up * jumpForce;
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
    }







    void AnimateWalk()
    {

        if (movementX > 0 && isGrounded)
        {
            animator.SetBool(runAnimation, true);
            animator.SetBool(jumpAnimation, false);
            animator.SetBool(idleAnimation, false);
            //spriteRenderer.flipX = false;

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
            //spriteRenderer.flipX = true;

        }
    }

    /*void AnimateJump()
    {
   
       

        if ( movementX < 0 && !isGrounded)
        {
            animator.SetBool(jumpAnimation, true);
            animator.SetBool(runAnimation, false);
            animator.SetBool(idleAnimation, false);
            //animator.SetBool(dashAnimation, false);
            //spriteRenderer.flipX = true;
       
        }

        if (movementX > 0 && !isGrounded)
        {
            animator.SetBool(jumpAnimation, true);
            animator.SetBool(runAnimation, false);
            animator.SetBool(idleAnimation, false);
            //animator.SetBool(dashAnimation, false);
            //spriteRenderer.flipX = false;
           
        }

        if ( movementX == 0 && !isGrounded)
        {
            animator.SetBool(jumpAnimation, true);
            animator.SetBool(runAnimation, false);
            animator.SetBool(idleAnimation, false);
            //animator.SetBool(dashAnimation, false);
            
        }
    }*/

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

<<<<<<< HEAD
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
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD



    public void DamageingObjects()
    {
        if (CollisionWithThorns == true && counter >= 2)
        {
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

        animator.SetBool("Dead", true);

        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }
=======
>>>>>>> parent of 1c1bfb5 (Spieler erleidet Schaden)
=======
>>>>>>> parent of 1c1bfb5 (Spieler erleidet Schaden)
=======
>>>>>>> parent of 0e35c16 (Weitere Turtle Sachen)
=======
>>>>>>> parent of 1c1bfb5 (Spieler erleidet Schaden)
}

    //private ienumerator dash()
    //{
    //    if (input.getkeydown(keycode.f) && movementx > 0)
    //    {
    //        animator.setbool(dashanimation, true);
    //        animator.setbool(jumpanimation, false);
    //        animator.setbool(walkanimation, false);
    //        animator.setbool(idleanimation, false);

    //        yield return new waitforseconds(0.4f);
    //        animator.setbool(dashanimation, false);
    //        animator.setbool(jumpanimation, false);
    //        animator.setbool(walkanimation, false);
    //        animator.setbool(idleanimation, true);
    //        this.transform.position = new vector3(player.position.x + dashforce, player.position.y, player.position.z);
    //    }

    //    else if (input.getkeydown(keycode.f) && movementx < 0)
    //    {
    //        animator.setbool(dashanimation, true);
    //        animator.setbool(jumpanimation, false);
    //        animator.setbool(walkanimation, false);
    //        animator.setbool(idleanimation, false);

    //        yield return new waitforseconds(0.4f);
    //        animator.setbool(dashanimation, false);
    //        animator.setbool(jumpanimation, false);
    //        animator.setbool(walkanimation, false);
    //        animator.setbool(idleanimation, true);
    //        this.transform.position = new vector3(player.position.x - dashforce, player.position.y, player.position.z);
    //    }
    //    else
    //    {
    //        animator.setbool(dashanimation, false);
    //    }

    //    yield return new waitforseconds(10f);

    //}
    // control k + c und control k + u alles aukommentieren



    

