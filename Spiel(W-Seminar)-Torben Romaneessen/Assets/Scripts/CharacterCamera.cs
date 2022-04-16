using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCamera : MonoBehaviour
{
    private Transform character;

    private Vector3 temPos;


    void Start()
    {
        character = GameObject.FindWithTag("Character").transform;
    }


    void Update()
    {
        //CheckForDash();
        temPos = transform.position;
        temPos.x = character.position.x;

        transform.position = temPos;


    }

    //private void CheckForDash()
    //{
    //if (Input.GetKeyDown(KeyCode.F))
    //{
    //this.temPos = new Vector3(temPos.x + 2.2f,temPos.y, temPos.z);
    //}
    //}

}
