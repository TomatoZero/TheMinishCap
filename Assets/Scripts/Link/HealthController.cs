using System;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField, Min(4)] private int _baseHp;
    [SerializeField] private UiController _uiController;
    [SerializeField] private MovementController _movementController;

    private int _currentHp;
    private int _currentMaxHp;

    private void Awake()
    {
        _uiController.SetCurrentHp(_baseHp);
        _currentHp = _baseHp;
        _currentMaxHp = _baseHp;

        Debug.Log($"Current hp {_currentHp}");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_movementController.CurrentState == State.AfterDeath) return;
        if (other.CompareTag("Water"))
        {
            TakeDamage();
            _movementController.FallFromEdge();
        }
        else if (other.CompareTag("Deep"))
        {
            TakeDamage();
            _movementController.FallFromEdge();
        }
    }

    public void TakeDamage()
    {
        if (_currentHp <= 0)
            throw new Exception("Player hp is less then or equal to zero ");

        _currentHp--;

        _uiController.ReduceHp();
    }

    public void Heal()
    {
        if (_currentHp == _currentMaxHp)
        {
            Debug.Log("Over Heal");
            return;
        }

        _currentHp++;
        _uiController.RestoreHp();
    }

    public void IncreaseHp()
    {
        _currentMaxHp += 4;
        _currentHp = _currentMaxHp;
        _uiController.SetCurrentHp(_currentHp);
    }
}