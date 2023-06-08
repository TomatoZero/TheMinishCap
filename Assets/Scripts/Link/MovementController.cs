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
                    _currentSpeed = _moveSpeed * 0.2f;
                    break;
                case State.Roll:
                    _currentSpeed = _moveSpeed * _rollSpeedMultiplier;
                    break;
                case State.FallFromEdge:
                case State.FallInWatter:
                case State.MeleeAttack:
                case State.MeleeAttackPrepare:
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
                CurrentState == State.StopClimb || CurrentState == State.MeleeAttack) return;

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

            if (CurrentState == State.MeleeAttackPrepare)
                CurrentState = State.MeleeAttack;
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

    public void ClimbingEventHandler()
    {
        var isStart = CurrentState != State.Climb;
        CurrentState = State.StopClimb;
        StartCoroutine(MoveWhileClimb(isStart));
    }

    private IEnumerator MoveWhileClimb(bool isStart)
    {
        yield return new WaitForSeconds(0.7f);
        if (!isStart) CurrentState = State.Ground;
        else CurrentState = State.Climb;
    }

    public void UseWeaponEventHandler()
    {
        CurrentState = State.MeleeAttackPrepare;
    }

    private void WeaponEventHold()
    {
        if(CurrentState == State.MeleeAttack)
            CurrentState = State.HoldWeapon;
    }

    public void ReleaseWeaponEventHandler()
    {
        CurrentState = State.Ground;
    }

    public void TakeDamageEventHandler(int hp, State state, Vector2 position)
    {
        if(CurrentState == State.FallFromEdge || CurrentState == State.FallInWatter) return;
        
        CurrentState = state;

        switch (state)
        {
            case State.PushAwayPrepare:
                var pushDirection = (-_rigidbody.position + position).normalized;
                MoveDirection = new Vector2(-pushDirection.x, -pushDirection.y);
                StartCoroutine(FinishPush());
                break;
            case State.FallInWatter:
            case State.FallFromEdge:
                var direction = (_rigidbody.position - position);

                _rigidbody.position += MoveDirection * 1.25f;
                StartCoroutine(WaitAfterFall(new Vector2(-_moveDirection.x, -_moveDirection.y)));
                break;
        }
    }

    private IEnumerator FinishPush()
    {
        yield return new WaitForSeconds(_pushTime);
        
        if(CurrentState != State.FallFromEdge || CurrentState != State.FallInWatter)
            CurrentState = State.Ground;
    }

    private IEnumerator WaitAfterFall(Vector2 position)
    {
        yield return new WaitForSeconds(_fallTime);
        _rigidbody.position = _rigidbody.position + position * 1.5f;
        Debug.Log($"Current state {CurrentState}");
        CurrentState = State.Ground;
    }

    private IEnumerator StopRolling()
    {
        yield return new WaitForSeconds(_rollTime);
        if(CurrentState != State.FallFromEdge || CurrentState != State.FallInWatter)
            CurrentState = State.Ground;
    }
}