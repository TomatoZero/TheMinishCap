using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class MovementController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [Space] 
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rollSpeedMultiplier = 5f;
    [SerializeField] private float _climbSpeedMultiplier;
    [SerializeField] private float _hoveringSpeedMultiplier;
    [SerializeField] private float _pushSpeedMultiplier;
    [Space]
    [SerializeField] private float _rollTime = 0.5f;
    [SerializeField] private float _pushTime;
    [SerializeField] private float _fallTime = 0.5f;
    
    public delegate void RotateEventHandler(Vector2 direction);
    public static event RotateEventHandler Rotation;
    
    private Vector2 _moveDirection;
    private Vector2 _directionView;
    private float _currentSpeed;
    private float _weaponHoldMultiplier = 1;
    private State _currentState = State.Ground;
    
    public State CurrentState
    {
        get => _currentState;
        private set
        {
            _currentState = value;

            switch (value)
            {
                case State.HoldWeapon:
                    _currentSpeed = _moveSpeed * _weaponHoldMultiplier;
                    break;
                case State.Ground:
                    _currentSpeed = _moveSpeed;
                    break;
                case State.Climb:
                    _currentSpeed = _moveSpeed * _climbSpeedMultiplier;
                    break;
                case State.StopClimb:
                    _currentSpeed = _moveSpeed * 0.1f;
                    break;
                case State.Roll:
                    _currentSpeed = _moveSpeed * _rollSpeedMultiplier;
                    break;
                case State.FallFromEdge:
                case State.FallInWatter:
                    _currentSpeed = 0;
                    break;
                case State.MeleeAttack:
                    _currentSpeed = 0;
                    break;
                case State.PushAwayPrepare:
                    StartCoroutine(FinishPush());
                    _currentSpeed = _moveSpeed * _pushSpeedMultiplier;
                    break;
                case State.PushAway:
                    _currentSpeed = _moveSpeed * _pushSpeedMultiplier;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }
        }
    }
    
    public Vector2 MoveDirection
    {
        get => _moveDirection;
        set
        {
            if (CurrentState == State.Roll || CurrentState == State.PushAway || 
                CurrentState == State.StopClimb) return;

            var tempDirection = value.normalized;

            var xTempAbs = Math.Abs(tempDirection.x);
            var yTempAbs = Math.Abs(tempDirection.y);
            
            if(Math.Abs(xTempAbs - yTempAbs) < 0.1)
            {
                if (Math.Abs(xTempAbs - 0.7) < 0.1) tempDirection.y = 0;
                else if (Math.Abs(yTempAbs - 0.7) < 0.1) tempDirection.x = 0;
            }
            
            _moveDirection = tempDirection.normalized;

            if (CurrentState == State.PushAwayPrepare)
            {
                CurrentState = State.PushAway;
                return;
            }
            
            if(CurrentState == State.HoldWeapon) return;
            
            if (tempDirection != Vector2.zero)
            {
                _directionView = tempDirection;
            }

            Rotation?.Invoke(_directionView);
        }
    }

    public Vector2 DirectionView => _directionView;
    public float CurrentSpeed => _currentSpeed;
    public float RollTime => _rollTime;

    private void Awake()
    {
        _currentSpeed = _moveSpeed;
    }

    private void FixedUpdate()
    {
        _rigidbody.MovePosition(_rigidbody.position + MoveDirection * (_currentSpeed * Time.fixedDeltaTime));
    }

    public void OnEnable()
    {
        BaseWeapon.HoldWeaponEvent += WeaponEventHold;
    }

    private void OnDisable()
    {
        BaseWeapon.HoldWeaponEvent -= WeaponEventHold;
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, _directionView * 1f);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, MoveDirection * 2f);
    }

    public void RollEventHandler()
    {
        if (CurrentState != State.Climb & CurrentState != State.Roll)
        {
            StartCoroutine(StopRolling());
            CurrentState = State.Roll;
        }
    }

    public void ClimbingStartEventHandler()
    {
        MoveDirection = Vector2.up;
        CurrentState = State.StopClimb;
        StartCoroutine(MoveWhileClimb());
    }

    public void ClimbingEndEventHandler()
    {
        MoveDirection = Vector2.down;
        CurrentState = State.StopClimb;
        StartCoroutine(MoveWhileClimb());
        
        CurrentState = State.Ground;
    }

    private IEnumerator MoveWhileClimb()
    {
        yield return new WaitForSeconds(0.7f);
        CurrentState = State.Climb;
    }
    
    public void JumpFromEdge()
    {
    }

    public void FallFromEdge(State state)
    {
        CurrentState = state;
        var reverseDirection = new Vector2(-MoveDirection.x, -MoveDirection.y);
        _rigidbody.position += reverseDirection * 0.5f;
        MoveDirection = Vector2.zero;
        StartCoroutine(WaitAfterFall());
    }

    public void UseWeaponEventHandler()
    {
        CurrentState = State.MeleeAttack;
    }

    private void WeaponEventHold()
    {
        CurrentState = State.HoldWeapon;
    }

    public void ReleaseWeaponEventHandler()
    {
        CurrentState = State.Ground;
    }

    public void TakeDamageEventHandler(int hp, State state, Vector2 position)
    {
        CurrentState = state;

        switch (state)
        {
            case State.PushAwayPrepare:
                MoveDirection = new Vector2(-position.x, -position.y);
                StartCoroutine(FinishPush());
                break;
            case State.FallInWatter:
            case State.FallFromEdge:
                _rigidbody.position = position;
                StartCoroutine(WaitAfterFall());
                break;
        }
    }

    private IEnumerator FinishPush()
    {
        yield return new WaitForSeconds(_pushTime);
        CurrentState = State.Ground;
    }

    private IEnumerator WaitAfterFall()
    {
        yield return new WaitForSeconds(_fallTime);
        
        var reverseDirection = new Vector2(-MoveDirection.x, -MoveDirection.y);
        _rigidbody.position += reverseDirection * 1.5f;
        
        CurrentState = State.Ground;
    }

    private IEnumerator StopRolling()
    {
        yield return new WaitForSeconds(_rollTime);
        CurrentState = State.Ground;
    }
}