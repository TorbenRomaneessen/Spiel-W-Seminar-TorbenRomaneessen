using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform position1, position2, position3, position4;
    public float speed;
    public Transform startposition;

    Vector3 nextposition;



    private void Start()
    {
        nextposition = startposition.position;
    }


    private void Update()
    {
        if(transform.position == position1.position)
        {
            nextposition = position2.position;
        }

        if(transform.position == position2.position)
        {
            nextposition = position3.position;
        }

        if (transform.position == position3.position)
        {
            nextposition = position4.position;
        }

        if (transform.position == position4.position)
        {
            nextposition = position1.position;
        }

        transform.position = Vector3.MoveTowards(transform.position, nextposition, speed * Time.deltaTime);
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(position1.position, position2.position);
    }
}
