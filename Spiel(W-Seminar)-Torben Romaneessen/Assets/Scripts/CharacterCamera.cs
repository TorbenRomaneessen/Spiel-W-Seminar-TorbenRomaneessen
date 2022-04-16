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
        temPos = transform.position;
        temPos.x = character.position.x;

        transform.position = temPos;
    }
}
