using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private const int MaxHealth = 3;
    private int _currentHealth;
    private float _timePassed;
    private const float Speed = 0.002f;

    private Transform _transform;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;


    private void Awake()
    {
        _transform = GetComponent<Transform>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _currentHealth = MaxHealth;
    }

    private void Update()
    {
        StartCoroutine(nameof(EnemyMovement));
    }

    private IEnumerator EnemyMovement()
    {

        if (_timePassed < 1)
        {
            yield return new WaitForSeconds(1f);

            this.transform.position = new Vector3(_transform.position.x + Speed, _transform.position.y, _transform.position.z);
            this.transform.localScale = new Vector3(-1, _transform.localScale.y, _transform.localScale.z);

            _timePassed += 1;
        }

        else
        {
            yield return new WaitForSeconds(1f);

            this.transform.position = new Vector3(_transform.position.x - Speed, _transform.position.y, _transform.position.z);
            this.transform.localScale = new Vector3(1, _transform.localScale.y, _transform.localScale.z);

            _timePassed -= 1;
        }
    }


    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        _animator.SetTrigger("Hurt");

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        _animator.SetBool("Dead", true);
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }
}