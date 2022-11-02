using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCamera : MonoBehaviour
{
    private Transform _characterTransform;
    private Vector3 _newPosition;


    void Start()
    {
        _characterTransform = GameObject.FindWithTag("Character").transform;
    }

    void Update()
    {
        _newPosition = transform.position;
        _newPosition.x = _characterTransform.position.x;
        transform.position = _newPosition;
    }
}
