using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class MovementController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [Space] [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rowSpeedMultiplier = 5f;
    [SerializeField] private float _climbSpeedMultiplier;
    [SerializeField] private float _hoveringSpeedMultiplier;
    [SerializeField] private float _rollTime = 0.5f;
    [FormerlySerializedAs("_pushSpeed")] [Space] [SerializeField] private float _pushSpeedMultiplier;
    [SerializeField] private float _pushTime;

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
                case State.Ladder:
                    _currentSpeed = _moveSpeed * _climbSpeedMultiplier;
                    break;
                case State.Roll:
                    _currentSpeed = _moveSpeed * _rowSpeedMultiplier;
                    break;
                case State.AfterDeath:
                    _currentSpeed = _moveSpeed;
                    break;
                case State.MeleeAttack:
                    _currentSpeed = _moveSpeed;
                    break;
                case State.PushAway:
                case State.PushAwayPrepare:
                    _currentSpeed = _moveSpeed * _pushSpeedMultiplier;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }
        }
    }

    public delegate void RotateEventHandler(Vector2 direction);

    public static event RotateEventHandler Rotation;


    public Vector2 MoveDirection
    {
        get => _moveDirection;
        set
        {
            if (CurrentState == State.Roll || CurrentState == State.PushAway) return;

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
            
            if (tempDirection != Vector2.zero)
            {
                _directionView = tempDirection;
            }

            Rotation?.Invoke(_directionView);
        }
    }

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
        if (CurrentState != State.Ladder & CurrentState != State.Roll)
        {
            StartCoroutine(StopRolling());
            CurrentState = State.Roll;
        }
    }

    public void ClimbingStart()
    {
        CurrentState = State.Ladder;
    }

    public void ClimbingEnd()
    {
        CurrentState = State.Ground;
    }

    public void JumpFromEdge()
    {
    }

    public void FallFromEdge()
    {
        CurrentState = State.AfterDeath;
        var reverseDirection = new Vector2(-MoveDirection.x, -MoveDirection.y);
        _rigidbody.position += reverseDirection * 0.5f;
        MoveDirection = Vector2.zero;
        StartCoroutine(WaitAfterDeath());
    }

    private void UseWeapon(State state)
    {
        CurrentState = State.MeleeAttack;
    }

    private void WeaponEventHold()
    {
        CurrentState = State.HoldWeapon;
    }

    private void ReleaseWeapon()
    {
        CurrentState = State.Ground;
    }

    public void PushAway(Vector2 direction)
    {
        CurrentState = State.PushAwayPrepare;
        MoveDirection = direction;
        StartCoroutine(FinishPush());
    }

    private IEnumerator FinishPush()
    {
        yield return new WaitForSeconds(_pushTime);
        CurrentState = State.Ground;
    }

    private IEnumerator WaitAfterDeath()
    {
        yield return new WaitForSeconds(1);
        CurrentState = State.Ground;
    }

    private IEnumerator StopRolling()
    {
        yield return new WaitForSeconds(_rollTime);
        CurrentState = State.Ground;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, _directionView * 1f);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, MoveDirection * 2f);
    }

}