using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiContoller : MonoBehaviour
{
    [SerializeField] private ItemsContoller _itemsPanel;
    [SerializeField] private StatisticPanelController _statisticPanel;
    [SerializeField] private MapPanelController _mapPanel;
    [Space]
    [SerializeField] private DungeonMapPanelController _dungeonMapPanel;

    [Space] [SerializeField] private TMP_Text _currenWindowName;
    [SerializeField] private DescriptionController _description;
    [SerializeField] private List<WindowController> _windows;

    private int _currentWindow;
    
    public string WindowName
    {
        get => _currenWindowName.text;
        set => _currenWindowName.text = value;
    }

    private void OnEnable()
    {
        foreach (var window in _windows)
        {
            window.Hide();
        }
        
        _windows[0].Show();
        _currentWindow = 0;
        WindowName = _windows[0].WindowName;
    }

    public void NextWindow()
    {
        _windows[_currentWindow].Hide();
        
        if(_currentWindow + 1 < _windows.Count)
        {
            _currentWindow++;
            WindowName = _windows[_currentWindow].WindowName;
            _windows[_currentWindow].Show();
        }
        else
        {
            _currentWindow = 0;
            WindowName = _windows[0].WindowName;
            _windows[0].Show();
        }
        
    }
    
    public void PrevWindow()
    {
        _windows[_currentWindow].Hide();
        
        if(_currentWindow - 1 >= 0)
        {
            _currentWindow--;
            WindowName = _windows[_currentWindow].WindowName;
            _windows[_currentWindow].Show();
        }
        else
        {
            _currentWindow = _windows.Count - 1;
            WindowName = _windows[_currentWindow].WindowName;
            _windows[^1].Show();
        }
    }

    public void SetDescription(string description)
    {
        _description.SetDescription(description);
    }
    

    public void HideDescription()
    {
        _description.Close();
    }
}