using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public static Vector3 ReachedPoint;
    public GameObject Flag;
    public Animator animator;
    public static CheckPoint instance;



    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

    }


    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Character"))
        {
            //if (this.transform.position.x > ReachedPoint.x)
            //{
                ReachedPoint = this.transform.position;
                
            //}
        }
    }
}