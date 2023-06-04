using System;
using UnityEngine;
using UnityEngine.Events;

public class HealthController : MonoBehaviour
{
    [SerializeField, Min(4)] private int _baseHp;
    [SerializeField] private UnityEvent _playerDie;
    [SerializeField] private PlayerHpEvent _playerTakeDamageEvent;
    [SerializeField] private PlayerHpEvent _playerHealEvent;
    [SerializeField] private PlayerHpEvent _playerSetHpHpEvent;
    [SerializeField] private UnityEvent _increaseHp;

    private int _currentHp;
    private int _currentMaxHp;
    private int _currentHeartPart;

    private void Awake()
    {
        _playerSetHpHpEvent.Invoke(_baseHp);
        _currentHp = _baseHp;
        _currentMaxHp = _baseHp;
    }

    public void TakeDamage()
    {
        TakeDamage(1);
    }

    public void HeartContainerPickUp(int count)
    {
        _currentHeartPart += count;

        while(_currentHeartPart >= 4)
        {
            _currentHeartPart -= 4;
            IncreaseHp();
            Debug.Log($"current count: {_currentHeartPart} + {count}");
        }
    }
    
    public void TakeDamage(int hp)
    {
        if (_currentHp <= 0) throw new Exception("Player hp less or equal zero");

        _currentHp -= hp;
        
        Debug.Log($"Current max hp {_currentMaxHp} _current hp {_currentHp}");

        if(_currentHp <= 0)
        {
            Debug.Log("Player die");
            _playerDie.Invoke();
        }
        
        _playerTakeDamageEvent.Invoke(hp);
    }

    public void Heal(int hp)
    {
        if (_currentHp == _currentMaxHp)
        {
            Debug.Log("Over Heal");
            return;
        }

        _currentHp += hp;

        if (_currentHp > _currentMaxHp)
            _currentHp = _currentMaxHp;
        
        _playerHealEvent.Invoke(hp);
    }

    public void IncreaseHp()
    {
        _currentMaxHp += 4;
        _currentHp = _currentMaxHp;
        
        Debug.Log($"Current max hp {_currentMaxHp} _current hp {_currentHp}");
        
        _increaseHp.Invoke();
    }
}