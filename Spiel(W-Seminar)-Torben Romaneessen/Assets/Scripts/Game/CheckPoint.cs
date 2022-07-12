using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public static Vector3 ReachedPoint;
   


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