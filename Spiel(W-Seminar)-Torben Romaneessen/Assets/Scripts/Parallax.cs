using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length, startposition;
    public GameObject camera;
    public float parallaxEffect;

    void Start()
    {
        startposition = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    
    void Update()
    {
        //float temp = (camera.transform.position.x * (1 - parallaxEffect));
        float distance = (camera.transform.position.x * parallaxEffect);

        transform.position = new Vector3(startposition + distance, transform.position.y, transform.position.z);

        /*if (temp > startposition + length)
        {
            startposition += length;
        }
        else if (temp < startposition - length)
        {
            startposition -= length;
        }*/
    }
}
