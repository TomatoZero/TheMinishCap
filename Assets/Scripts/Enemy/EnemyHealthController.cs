using System;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealthController : MonoBehaviour
{
    [SerializeField] private int _baseHp;
    [SerializeField] private UnityEvent _deathEvent;
    [SerializeField] private SnakeAttackEvent _pushAwayEvent;
    [SerializeField] private EnemyMoveController _moveController;
    
    private int _currentHp;

    private void Awake()
    {
        _currentHp = _baseHp;
    }

    public virtual void TakeDamage(Vector2 direction)
    {
        if (_currentHp <= 0)
            throw new Exception("Enemy hp is less then or equal to zero ");

        _currentHp--;
        _pushAwayEvent.Invoke(direction);
        
        if (_currentHp == 0)
        {
            _deathEvent.Invoke();
        }
    }
}