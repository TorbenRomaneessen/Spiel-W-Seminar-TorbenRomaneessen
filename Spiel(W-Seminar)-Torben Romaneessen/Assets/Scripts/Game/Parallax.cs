using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float _startPosition;
    [SerializeField]
    private GameObject _camera;
    [SerializeField]
    private float _parallaxEffect;


    void Start()
    {
        _startPosition = transform.position.x;
    }

    void Update()
    {
        float distance = _camera.transform.position.x * _parallaxEffect;
        transform.position = new Vector3(_startPosition + distance, transform.position.y, transform.position.z);
    }
}
