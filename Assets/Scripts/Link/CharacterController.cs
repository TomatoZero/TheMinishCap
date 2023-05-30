using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rowSpeedMultiplier = 5f;
    [SerializeField] private float _climbSpeedMultiplier;
    [SerializeField] private float _hoveringSpeedMultiplier;
    [SerializeField] private float _rollTime = 0.5f;

    private Rigidbody2D _rigidbody;
    private Vector2 _moveDirection;
    private Vector2 _rollDirection;
    private float _currentSpeed;

    private List<SpriteRenderer> _childSprite;
    private State _currentState = State.Ground;

    public Vector2 MoveDirection
    {
        get => _moveDirection;
        set
        {
            if(_currentState != State.Roll)
            {
                _moveDirection = value;
                
                if (Math.Abs(_moveDirection.x) > Math.Abs(_moveDirection.y)) _moveDirection.y = 0;
                else _moveDirection.x = 0;
                
                if (_currentState == State.Ladder)
                    _moveDirection.x = 0;
            }
        }
    }

    private void Awake()
    {
        _rigidbody = this.GetComponent<Rigidbody2D>();
        _currentSpeed = _moveSpeed;

        _childSprite = this.GetComponents<SpriteRenderer>().ToList();
    }

    private void FixedUpdate()
    {
        _rigidbody.MovePosition(_rigidbody.position + MoveDirection * (_currentSpeed * Time.fixedDeltaTime));
    }

    public void ChangeLayer(string layer)
    {
        foreach (var spriteRenderer in _childSprite)
        {
            spriteRenderer.sortingLayerName = layer;
        }

        var layInt = LayerMask.NameToLayer(layer);
        gameObject.layer = layInt;
    }

    public void Roll()
    {
        if(_currentState != State.Ladder & _currentState != State.Roll)
        {
            _currentSpeed = _moveSpeed * _rowSpeedMultiplier;

            StartCoroutine(StopRolling());
            _currentState = State.Roll;
        }
    }

    public void ClimbingStart()
    {
        _currentSpeed = _moveSpeed * _climbSpeedMultiplier;
        _currentState = State.Ladder;
    }

    public void ClimbingEnd()
    {
        _currentSpeed = _moveSpeed;
        _currentState = State.Ground;
    }

    public void JumpFromEdge()
    {
        
    }

    private IEnumerator StopRolling()
    {
        yield return new WaitForSeconds(_rollTime);
        _currentSpeed = _moveSpeed;
        _currentState = State.Ground;
    }

    enum State
    {
        Ground,
        Ladder,
        Roll
    }
}
