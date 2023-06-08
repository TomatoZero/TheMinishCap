using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = System.Random;

public class EnemyMoveController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private float _baseSpeed;
    [SerializeField] private float _pushSpeedMultiplier;
    [SerializeField] private float _runSpeedMultiplier;
    [SerializeField] private float _pushTime;
    [SerializeField] private EnemyEyesController _eyesController;
    [SerializeField] private ChangeDirectionViewEvent _rotation;

    public Vector2 DirectionView => _directionView;

    protected virtual Vector2 MoveDirection
    {
        get => _moveDirection;
        private protected set
        {
            if (CurrentState == EnemyState.PushAway) return;

            var tempDirection = value.normalized;

            var xTempAbs = Math.Abs(tempDirection.x);
            var yTempAbs = Math.Abs(tempDirection.y);

            if (Math.Abs(xTempAbs - yTempAbs) < 0.1)
            {
                if (Math.Abs(xTempAbs - 0.7) < 0.1) tempDirection.y = 0;
                else if (Math.Abs(yTempAbs - 0.7) < 0.1) tempDirection.x = 0;
            }

            _moveDirection = tempDirection.normalized;

            if (CurrentState == EnemyState.PushAwayPrepare)
            {
                CurrentState = EnemyState.PushAway;
                return;
            }

            if (tempDirection != Vector2.zero)
            {
                _directionView = tempDirection;
            }

            _rotation.Invoke(_directionView);
        }
    }


    protected virtual EnemyState CurrentState
    {
        get => _currentState;
        private protected set
        {
            _currentState = value;
            switch (value)
            {
                case EnemyState.Move:
                    _currentSpeed = _baseSpeed;
                    break;
                case EnemyState.StartMove:
                    _currentSpeed = _baseSpeed;
                    
                    var availableDirection = _eyesController.CheckAvailableDirection();
                    
                    // Debug.Log($"availableDirection {String.Join(' ', availableDirection)}");
                    
                    var randomIndex = (new Random()).Next(0, availableDirection.Count);
                    MoveDirection = availableDirection[randomIndex];
                    StartCoroutine(ChangeMoveDirection());
                    
                    CurrentState = EnemyState.Move;
                    break;
                case EnemyState.Run:
                    _currentSpeed = _baseSpeed * _runSpeedMultiplier;
                    break;
                case EnemyState.PushAway:
                case EnemyState.PushAwayPrepare:
                    _currentSpeed = _baseSpeed * _pushSpeedMultiplier;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }
        }
    }

    private Vector2 _moveDirection;
    private Vector2 _directionView;
    private EnemyState _currentState;
    private float _currentSpeed;

    private void Awake()
    {
        CurrentState = EnemyState.StartMove;
    }

    private void FixedUpdate()
    {
        if (CurrentState == EnemyState.StartMove)
        {
            CurrentState = EnemyState.Move;
            var availableDirection = _eyesController.CheckAvailableDirection();
            var randomIndex = (new Random()).Next(0, availableDirection.Count);
            MoveDirection = availableDirection[randomIndex];
            StartCoroutine(ChangeMoveDirection());
            return;
        }

        _rigidbody.MovePosition(_rigidbody.position + MoveDirection * (_currentSpeed * Time.fixedDeltaTime));
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if ((CurrentState != EnemyState.StartMove) &&
            (other.collider.CompareTag("Enemy") || other.collider.CompareTag("Wall")) ||
            other.collider.CompareTag("Player") || other.collider.CompareTag("EnemyZone"))
        {
            StopRun();
        }
    }

    public virtual void PushAwayEventHandler(Vector2 direction)
    {
        MoveDirection = Vector2.zero;
        CurrentState = EnemyState.PushAwayPrepare;
        MoveDirection = direction;
        StartCoroutine(StopPushTimer());
    }

    public virtual void RunOnEnemyDirection(Vector2 enemyPosition)
    {
        _eyesController.SetLookingMode(false);

        MoveDirection = enemyPosition - (Vector2)transform.position;
        CurrentState = EnemyState.Run;
    }

    public virtual void StopRun()
    {
        MoveDirection = Vector2.zero;
        CurrentState = EnemyState.StartMove;
        StartCoroutine(AfterPlayerHitAction());
    }

    public virtual void PlayerHit()
    {
        MoveDirection = Vector2.zero;
        CurrentState = EnemyState.StartMove;
        if(enabled)
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
        Gizmos.DrawRay(transform.position, _directionView * 1f);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, MoveDirection * 2f);
    }
}