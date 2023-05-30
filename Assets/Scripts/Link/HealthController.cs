using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField, Min(3)] private int _baseHp;
    [SerializeField] private UiController _uiController;

    private int _currentHp;

    private void Awake()
    {
        _uiController.SetCurrentHp(_baseHp);
        _currentHp = _baseHp;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Water"))
        {
            TakeDamage();
        }
    }

    public void TakeDamage()
    {
        if (_currentHp <= 0)
            throw new Exception("Player hp is less then or equal to zero ");
        
        _currentHp--;
        _uiController.RestoreHp();
    }
}
