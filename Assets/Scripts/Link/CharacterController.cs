using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rollSpeed;
    [SerializeField] private float _speedMultiplier = 5f;
    [SerializeField] private float _rollTime = 0.5f;
    
    private Rigidbody2D _rigidbody;
    private Vector2 _moveDirection;
    private Vector2 _rollDirection;
    private float _currentSpeed;
    private bool _isRoll;

    public Vector2 MoveDirection
    {
        get => _moveDirection;
        set => _moveDirection = value;
    }

    public bool IsRoll
    {
        get => _isRoll;
        set
        {
            _isRoll = value;
            _currentSpeed = _moveSpeed * _speedMultiplier;
            StartCoroutine(Test());
        }
    }

    private void Awake()
    {
        _rigidbody = this.GetComponent<Rigidbody2D>();
        _currentSpeed = _moveSpeed;
    }

    private void FixedUpdate()
    {
        _rigidbody.MovePosition(_rigidbody.position + MoveDirection * (_currentSpeed * Time.fixedDeltaTime));
    }

    private IEnumerator Test()
    {
        yield return new WaitForSeconds(_rollTime);
        _currentSpeed = _moveSpeed;
        _isRoll = false;
    }
}
