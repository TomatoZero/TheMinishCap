using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Random = System.Random;

public class EnemyMoveController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;

    [FormerlySerializedAs("_moveSpeed")] [SerializeField]
    private float _baseSpeed;

    [FormerlySerializedAs("_pushSpeed")] [SerializeField]
    private float _pushSpeedMultiplier;

    [FormerlySerializedAs("_attackSpeedMultiplier")] [FormerlySerializedAs("_attackSpeed")] [SerializeField]
    private float _runSpeedMultiplier;

    [SerializeField] private float _pushTime;
    [SerializeField] private EnemyEyesController _eyesController;
    [SerializeField] private SnakeAttackEvent _changeDirectionEvent;

    public Vector2 MoveDirection
    {
        get => _moveDirection;
        private set
        {
            _moveDirection = value.normalized;

            if (_moveDirection != Vector2.zero) _changeDirectionEvent.Invoke(_moveDirection);

            //---------------
            //
            // if (_currentState == EnemyState.PushAway) return;
            //
            // _moveDirection = value;
            //
            // var xAbs = Math.Abs(_moveDirection.x);
            // var yAbs = Math.Abs(_moveDirection.y);
            //     
            // if (xAbs > yAbs && xAbs != 1) _moveDirection.y = 0;
            // else if(yAbs > xAbs && yAbs != 1) _moveDirection.x = 0;
            //     
            // if (_moveDirection != Vector2.zero)
            // {
            //     if (_currentState == EnemyState.PushAway) return;
            //         _directionView = _moveDirection;
            //         Rotation?.Invoke(_directionView);
            // }
        }
    }
    
    private EnemyState _currentState = EnemyState.StartMove;
    private float _currentSpeed;
    private Vector2 _moveDirection;

    private EnemyState CurrentState
    {
        get => _currentState;
        set
        {
            _currentState = value;
            switch (value)
            {
                case EnemyState.Move:
                case EnemyState.StartMove:
                    _currentSpeed = _baseSpeed;
                    break;
                case EnemyState.Run:
                    _currentSpeed = _baseSpeed * _runSpeedMultiplier;
                    break;
                case EnemyState.PushAway:
                    _currentSpeed = _baseSpeed * _pushSpeedMultiplier;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }
        }
    }

    private void Awake()
    {
        _currentSpeed = _baseSpeed;
    }

    private void FixedUpdate()
    {
        if (CurrentState == EnemyState.Run || CurrentState == EnemyState.PushAway || CurrentState == EnemyState.Move)
            _rigidbody.MovePosition(_rigidbody.position + MoveDirection * (_currentSpeed * Time.fixedDeltaTime));
        else if (CurrentState == EnemyState.StartMove)
        {
            CurrentState = EnemyState.Move;
            var availableDirection = _eyesController.CheckAvailableDirection();
            var randomIndex = (new Random()).Next(0, availableDirection.Count);
            MoveDirection = availableDirection[randomIndex];
            StartCoroutine(ChangeMoveDirection());
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if ((CurrentState == EnemyState.Run || CurrentState == EnemyState.Move ||
             CurrentState == EnemyState.PushAway) &&
            (other.collider.CompareTag("Enemy") || other.collider.CompareTag("Wall")) ||
            other.collider.CompareTag("Player"))
        {
            StopRun();
        }
    }

    public void PushAway(Vector2 direction)
    {
        CurrentState = EnemyState.PushAway;
        MoveDirection = direction;
        StartCoroutine(StopPushTimer());
    }

    public void RunOnEnemyDirection(Vector2 enemyPosition)
    {
        _eyesController.SetLookingMode(false);

        MoveDirection = enemyPosition - (Vector2)transform.position;
        CurrentState = EnemyState.Run;
    }

    public void StopRun()
    {
        MoveDirection = Vector2.zero;
        CurrentState = EnemyState.StartMove;
        StartCoroutine(AfterPlayerHitAction());
    }

    public void PlayerHit()
    {
        MoveDirection = Vector2.zero;
        CurrentState = EnemyState.StartMove;
        StartCoroutine(AfterPlayerHitAction());
    }

    private IEnumerator StopPushTimer()
    {
        yield return new WaitForSeconds(_pushTime);
        CurrentState = EnemyState.Move;
    }

    private IEnumerator AfterPlayerHitAction()
    {
        yield return new WaitForSeconds(1f);

        _eyesController.SetLookingMode(true);
    }

    private IEnumerator ChangeMoveDirection()
    {
        yield return new WaitForSeconds(10f);

        if (CurrentState != EnemyState.Run)
            CurrentState = EnemyState.StartMove;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, MoveDirection * 1f);
    }
}