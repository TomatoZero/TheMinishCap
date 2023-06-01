using System;
using System.Collections;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [Space]
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rowSpeedMultiplier = 5f;
    [SerializeField] private float _climbSpeedMultiplier;
    [SerializeField] private float _hoveringSpeedMultiplier;
    [SerializeField] private float _rollTime = 0.5f;
    [Space]
    [SerializeField] private float _pushSpeed;
    [SerializeField] private float _pushTime;
    
    private Vector2 _moveDirection;
    private Vector2 _directionView;
    private float _currentSpeed;
    private float _weaponHoldMultiplier = 1;

    private State _currentState = State.Ground;

    public State CurrentState => _currentState;
    
    public delegate void RotateEventHandler(Vector2 direction);
    public static event RotateEventHandler Rotation;


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

                if (_moveDirection != Vector2.zero)
                    _directionView = _moveDirection;
                
                Rotation?.Invoke(_directionView);
            }
        }
    }

    private void Awake()
    {
        _currentSpeed = _moveSpeed;
    }

    private void FixedUpdate()
    {
        if(_currentState != State.AfterDeath && _currentState != State.MeleeAttack)
        {
            _rigidbody.MovePosition(_rigidbody.position + MoveDirection * (_currentSpeed * _weaponHoldMultiplier * Time.fixedDeltaTime));
        }
        if (_currentState == State.Push)
        {
            _rigidbody.MovePosition(_rigidbody.position + MoveDirection * (_pushSpeed * Time.fixedDeltaTime));
        }
    }

    public void OnEnable()
    {
        WeaponsController.UseWeapon += UseWeapon;
        WeaponsController.ReleaseWeapon += ReleaseWeapon;
        BaseWeapon.HoldWeaponEvent += WeaponEventHold;
    }

    private void OnDisable()
    {
        WeaponsController.UseWeapon -= UseWeapon;
        WeaponsController.ReleaseWeapon -= ReleaseWeapon;        
        BaseWeapon.HoldWeaponEvent -= WeaponEventHold;
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

    public void FallFromEdge()
    {
        _currentState = State.AfterDeath;
        var reverseDirection = new Vector2(-MoveDirection.x, -MoveDirection.y);
        _rigidbody.position += reverseDirection * 0.5f;
        _moveDirection = Vector2.zero;
        StartCoroutine(WaitAfterDeath());
    }

    private void UseWeapon(State state)
    {
        _currentState = State.MeleeAttack;
    }

    private void WeaponEventHold()
    {
        _currentState = State.HoldWeapon;
    }
    
    private void ReleaseWeapon()
    {
        _currentState = State.Ground;
    }
    
    public void PushAway(Vector2 direction)
    {
        _currentState = State.Push;
        _moveDirection = direction;
        StartCoroutine(FinishPush());
    }

    private IEnumerator FinishPush()
    {
        yield return new WaitForSeconds(_pushTime);
        _currentState = State.Ground;
    }

    private IEnumerator WaitAfterDeath()
    {
        yield return new WaitForSeconds(1);
        _currentState = State.Ground;
    }
    
    private IEnumerator StopRolling()
    {
        yield return new WaitForSeconds(_rollTime);
        _currentSpeed = _moveSpeed;
        _currentState = State.Ground;
    }

}