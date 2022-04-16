using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charactercamera : MonoBehaviour
{
    private Transform character;

    private Vector3 tempos;


    void Start()
    {
        character = GameObject.FindWithTag("character").transform;
    }


    void Update()
    {
        //checkfordash();
        tempos = transform.position;
        tempos.x = character.position.x;

        transform.position = tempos;
    }

    //private void checkfordash()
    //{
    //if (input.getkeydown(keycode.f))
    //{
    //this.tempos = new vector3(tempos.x + 2.2f,tempos.y, tempos.z);
    //}
    //}

}
