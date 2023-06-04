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
            if (_currentState == State.Roll || _currentState == State.PushAway) return;
            
            var tempDirection = value;

            var xAbs = Math.Abs(tempDirection.x);
            var yAbs = Math.Abs(tempDirection.y);

            if (xAbs > yAbs && xAbs != 1) tempDirection.y = 0;
            else if(yAbs > xAbs && yAbs != 1) tempDirection.x = 0;
            
            tempDirection.Normalize();
            
            if (_currentState == State.Ladder)
                tempDirection.x = 0;

            if (tempDirection != Vector2.zero)
            {
                _directionView = tempDirection;
            }

            if (tempDirection != Vector2.zero && _currentState == State.PushAway)
                _directionView = new Vector2(-tempDirection.x, -tempDirection.y);
            
            Rotation?.Invoke(_directionView);
            _moveDirection = tempDirection;
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
        if (_currentState == State.PushAway)
        {
            // Debug.Log("Fixed update");
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
        MoveDirection = Vector2.zero;
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
        MoveDirection = direction;
        _currentState = State.PushAway;
        StartCoroutine(FinishPush());
    }

    private IEnumerator FinishPush()
    {
        // var count = 4;
        // var partPushTime = _pushTime / count;
        //
        // while (count <= 0)
        // {
        //     _currentSpeed -= 0.2f;
        //     yield return new WaitForSeconds(partPushTime);
        //     count--;
        // }

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
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, _directionView * 1f);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, MoveDirection * 2f);
    }
}