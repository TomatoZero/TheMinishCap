using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class UiContoller : MonoBehaviour
{
    [SerializeField] private ItemsContoller _itemsPanel;
    [SerializeField] private StatisticPanelController _statisticPanel;
    [SerializeField] private MapPanelController _mapPanel;
    [Space]
    [SerializeField] private DungeonMapPanelController _dungeonMapPanel;
    [Space]
    [SerializeField] private TMP_Text _currenWindowName;
    [SerializeField] private DescriptionController _description;
    [SerializeField] private List<WindowController> _windows;
    [SerializeField] private List<ButtonHintController> _buttonsHint;
    
    private int _currentWindow;
    
    public string WindowName
    {
        get => _currenWindowName.text;
        set => _currenWindowName.text = value;
    }

    private void Awake()
    {
        InputSystem.onDeviceChange += (device, change) =>
        {
           Debug.Log($"{device} {change}");
        };
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

    public void SetFirstButtonHint(IventoryItem item)
    {
        SetButtonHint(0, item.Item.Icon);
    }
    
    public void SetSecondButtonHint(IventoryItem item)
    {
        SetButtonHint(1, item.Item.Icon);
    }

    public void SetFirstButtonHint(string hint)
    {
        SetButtonHint(0, hint);
    }

    public void HideButtonHint(int id)
    {
        _buttonsHint[id].Hide();
    }

    private void SetButtonHint(int id, Sprite itemIco)
    {
        _buttonsHint[id].Show();
        _buttonsHint[id].ItemIco = itemIco;
    }

    private void SetButtonHint(int id, string hint)
    {
        _buttonsHint[id].Show();
        _buttonsHint[id].TextHint = hint;
    }
}