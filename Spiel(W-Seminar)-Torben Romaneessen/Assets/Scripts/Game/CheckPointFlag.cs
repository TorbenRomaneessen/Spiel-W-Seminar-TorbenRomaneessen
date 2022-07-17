using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointFlag : MonoBehaviour
{
    public Animator animator;
    public GameObject Flag;



    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Character"))
        {
            animator.SetTrigger("NewCheckPoint");

        }
    }

}
