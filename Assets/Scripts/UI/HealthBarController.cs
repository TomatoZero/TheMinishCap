using System;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarController : MonoBehaviour
{
    [SerializeField] private GameObject _heartPrefab;

    private List<HeartUiController> _hearts = new List<HeartUiController>();
    private int _currentHeartId; 
    private float _currentHeartStatus;

    public void SetCurrentHp(int hp)
    {
        for (var i = 0; i < hp / 4; i++) IncreaseHp();
    }
    
    public void IncreaseHp()
    {
        foreach (var heart in _hearts) heart.SetFullHp();

        var newHeart =  Instantiate(_heartPrefab, this.transform);
        var newHeartController = newHeart.GetComponent<HeartUiController>();
        _hearts.Add(newHeartController);
        _currentHeartId = _hearts.Count - 1;
    }

    public void ReduceHp()
    {
        if (_currentHeartId < 0 || _currentHeartId > _hearts.Count - 1)
        {
            Debug.Log($"Current id {_currentHeartId}");
            return;
        }
        
        if (_hearts[_currentHeartId].IsEmpty)
        {
            if (_currentHeartId - 1 > 0)
                _currentHeartId--;
            else
            {
                Debug.Log($"Over Damage? {_currentHeartId}");
                return;
            }
        }
        
        _hearts[_currentHeartId].TakeDamage();
        if (_hearts[_currentHeartId].IsEmpty && _currentHeartId > 0)
        {
            _currentHeartId--;
            Debug.Log($"Take damage decree {_currentHeartId}");

        }
    }

    public void RestoreHp()
    {
        if (_currentHeartId < 0)
        {
            throw new ArgumentOutOfRangeException();
        }
        
        if (_hearts[_currentHeartId].IsFull)
        {
            if (_currentHeartId + 1 <= _hearts.Count - 1)
                _currentHeartId++;
            else
            {
                Debug.Log($"Over heal {_currentHeartId}");
                return;
            }
        }

        if (_currentHeartId > _hearts.Count - 1)
        {
            Debug.Log($"Current heart {_currentHeartId}");
            return;
        }
        
        _hearts[_currentHeartId].Heal();
        if (_hearts[_currentHeartId].IsFull && _currentHeartId < _hearts.Count - 1)
        {
            _currentHeartId++;
            Debug.Log($"Heal increase {_currentHeartId}");
        }
        
    }
}