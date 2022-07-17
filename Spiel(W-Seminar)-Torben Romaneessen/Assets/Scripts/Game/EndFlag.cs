using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndFlag : MonoBehaviour
{
    public Animator animator;
    public GameObject Flag;



    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Character"))
        {
            animator.SetTrigger("LevelCompleted");

        }
    }
}
