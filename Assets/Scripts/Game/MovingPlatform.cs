using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    private Transform _startPosition;
    [SerializeField]
    private Transform _position1;
    [SerializeField]
    private Transform _position2;
    [SerializeField]
    private Transform _position3;
    [SerializeField]
    private Transform _position4;
    private Vector3 _nextPosition;
    private Transform _transform;

    [SerializeField]
    private float _speed = 2;


    private void Start()
    {
        _nextPosition = _startPosition.position;
        _transform = GetComponent<Transform>();
    }

    private void Update()
    {
        if (Dialog.Instance.IsTalking == false)
        {
            if (_transform.position == _position1.position)
            {
                _nextPosition = _position2.position;
            }
        }

            if (_transform.position == _position2.position)
            {
                _nextPosition = _position3.position;
            }

            if (_transform.position == _position3.position)
            {
                _nextPosition = _position4.position;
            }

            if (_transform.position == _position4.position)
            {
                _nextPosition = _position1.position;
            }

        _transform.position = Vector3.MoveTowards(_transform.position, _nextPosition, _speed * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(_position1.position, _position2.position);
        Gizmos.DrawLine(_position2.position, _position3.position);
        Gizmos.DrawLine(_position3.position, _position4.position);
        Gizmos.DrawLine(_position4.position, _position1.position);
    }
}
