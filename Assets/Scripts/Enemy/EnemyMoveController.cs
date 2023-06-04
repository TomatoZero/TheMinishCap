using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Random = System.Random;

public class EnemyMoveController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _pushSpeed;
    [SerializeField] private float _attackSpeed;
    [SerializeField] private float _pushTime;
    [SerializeField] private EnemyEyesController _eyesController;
    [SerializeField] private SnakeAttackEvent _changeDirectionEvent;

    public Vector2 MoveDirection
    {
        get => _movementDirection;
        private set
        {
            _movementDirection = value.normalized;
            
            if(_movementDirection != Vector2.zero) _changeDirectionEvent.Invoke(_movementDirection);
        }
    }
    
    private EnemyState _state = EnemyState.StartMove;
    private float _currentSpeed;
    private Vector2 _movementDirection;
    
    private Vector2[] directions = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };

    private void Awake()
    {
        _currentSpeed = _moveSpeed;
    }

    private void FixedUpdate()
    {
        if(_state == EnemyState.Run || _state == EnemyState.Push || _state == EnemyState.Move)
            _rigidbody.MovePosition(_rigidbody.position + MoveDirection * (_currentSpeed * Time.fixedDeltaTime));
        else if(_state == EnemyState.StartMove)
        {
            _state = EnemyState.Move;
            var rnd = new Random();
            int randomIndex = rnd.Next(0, directions.Length);
            MoveDirection = directions[randomIndex];
            StartCoroutine(ChangeMoveDirection());
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (_state == EnemyState.Run && (other.collider.CompareTag("Enemy") || other.collider.CompareTag("Wall")))
        {
            StopRun();
        }   
    }
    
    public void PushAway(Vector2 direction)
    {
        _state = EnemyState.Push;
        MoveDirection = direction;
        _currentSpeed = _pushSpeed;
        StartCoroutine(StopPushTimer());
    }

    public void RunOnEnemyDirection(Vector2 enemyPosition)
    {
<<<<<<< HEAD
        _eyesController.SetLookingMode(false);
=======
        _eyesController.SetActive(false);
>>>>>>> parent of 4fecc1f (refactor(game logic): fix errors in the behavior of the enemy and the user when interacting with him)
        
        MoveDirection = enemyPosition - (Vector2)transform.position;
        _currentSpeed = _attackSpeed;
        _state = EnemyState.Run;
    }

    public void StopRun()
    {
<<<<<<< HEAD
        _eyesController.SetLookingMode(true);
=======
        _eyesController.SetActive(true);
>>>>>>> parent of 4fecc1f (refactor(game logic): fix errors in the behavior of the enemy and the user when interacting with him)
        _currentSpeed = _moveSpeed;
    }

    public void PlayerHit()
    {
        StartCoroutine(AfterPlayerHitAction());
    }

    private IEnumerator StopPushTimer()
    {
        yield return new WaitForSeconds(_pushTime);
        _state = EnemyState.Move;
    }

    private IEnumerator AfterPlayerHitAction()
    {
        _state = EnemyState.Move;
        _currentSpeed = _moveSpeed;
        yield return new WaitForSeconds(0.5f);
        
<<<<<<< HEAD
        _eyesController.SetLookingMode(true);
=======
        _eyesController.SetActive(true);
>>>>>>> parent of 4fecc1f (refactor(game logic): fix errors in the behavior of the enemy and the user when interacting with him)
    }

    private IEnumerator ChangeMoveDirection()
    {
        yield return new WaitForSeconds(2f);
        _state = EnemyState.StartMove;
    }
}