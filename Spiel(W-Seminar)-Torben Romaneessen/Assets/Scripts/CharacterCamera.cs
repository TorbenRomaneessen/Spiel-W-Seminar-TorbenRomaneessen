using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCamera : MonoBehaviour
{
    private Transform character;
    private Vector3 tempos;



    void Start()
    {
        character = GameObject.FindWithTag("Character").transform;
    }


    void Update()
    {
        tempos = transform.position;
        tempos.x = character.position.x;
        transform.position = tempos;
    }
}
