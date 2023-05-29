using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartUiController : MonoBehaviour
{
    [SerializeField] private List<Sprite> _hearts;
    [SerializeField] private Image _image;

    private int _currentHeartId;

    public bool IsEmpty
    {
        get; 
        private set;
    }

    public bool IsFull
    {
        get;
        private set;
    }

    private void Awake()
    {
        _image.sprite = _hearts[0];
        IsEmpty = false;
        IsFull = true;
    }

    public void TakeDamage()
    {
        if (_currentHeartId + 1 <= _hearts.Count - 1)
        {
            _currentHeartId++;
            _image.sprite = _hearts[_currentHeartId];

            if (_currentHeartId == _hearts.Count - 1)
            {
                IsEmpty = true;
                IsFull = false;
                return;
            }
            
            IsEmpty = false;
            IsFull = false;
        }
        else
        {
            Debug.Log("Wrong heart selected. Take damage");
        }
    }

    public void Heal()
    {
        if (_currentHeartId - 1 >= 0)
        {
            _currentHeartId--;
            _image.sprite = _hearts[_currentHeartId];

            if (_currentHeartId == 0)
            {
                IsFull = true;
                IsEmpty = false;
                return;
            }

            IsFull = false;
            IsEmpty = false;
        }
        else
        {
            Debug.Log("Wrong heart selected. Heal");
        }
    }

    public void SetFullHp()
    {
        _currentHeartId = 0;
        _image.sprite = _hearts[0];
    }

    public void SetZeroHp()
    {
        _currentHeartId = _hearts.Count - 1;
        _image.sprite = _hearts[^1];
    }
    
    
}
