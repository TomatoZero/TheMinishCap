using System;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealthController : MonoBehaviour
{
    [SerializeField] private int _baseHp;
    [SerializeField] private UnityEvent _deathEvent;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private EnemyMoveController _moveController;
    
    private int _currentHp;

    private void Awake()
    {
        _currentHp = _baseHp;
    }

    public void TakeDamage(Vector2 direction)
    {
        if (_currentHp <= 0)
            throw new Exception("Enemy hp is less then or equal to zero ");

        _currentHp--;
        _moveController.PushAway(direction);
        
        if (_currentHp == 0)
        {
            // DeathEvent?.Invoke();
            _deathEvent.Invoke();
        }
    }
}