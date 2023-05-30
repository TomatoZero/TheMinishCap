using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuController : MonoBehaviour
{
    [SerializeField] private UiController _uiController;
    [SerializeField] private TMP_Text _currenWindowName;
    [SerializeField] private DescriptionController _description;
    [SerializeField] private List<WindowController> _windows;
    [SerializeField] private EventSystem _eventSystem;
    
    private int _currentWindow;
    
    public string CurrentWindowName
    {
        get => _currenWindowName.text;
        set => _currenWindowName.text = value;
    }

    public GameObject FirstSelected
    {
        get => _eventSystem.currentSelectedGameObject;
        set => _eventSystem.SetSelectedGameObject(value);
    }

    private void Awake()
    {
        // InputSystem.onDeviceChange += (device, change) =>
        // {
        //    Debug.Log($"{device} {change}");
        // };
        
    }

    private void OnEnable()
    {
        foreach (var window in _windows)
        {
            window.Hide();
        }
        
        _windows[0].Show();
        _currentWindow = 0;
        CurrentWindowName = _windows[0].WindowName;
    }

    public void NextWindow()
    {
        _windows[_currentWindow].Hide();
        
        if(_currentWindow + 1 < _windows.Count)
        {
            _currentWindow++;
            CurrentWindowName = _windows[_currentWindow].WindowName;
            _windows[_currentWindow].Show();
        }
        else
        {
            _currentWindow = 0;
            CurrentWindowName = _windows[0].WindowName;
            _windows[0].Show();
        }
        
        HideDescription();
    }
    
    public void PrevWindow()
    {
        _windows[_currentWindow].Hide();
        
        if(_currentWindow - 1 >= 0)
        {
            _currentWindow--;
            CurrentWindowName = _windows[_currentWindow].WindowName;
            _windows[_currentWindow].Show();
        }
        else
        {
            _currentWindow = _windows.Count - 1;
            CurrentWindowName = _windows[_currentWindow].WindowName;
            _windows[^1].Show();
        }
        
        HideDescription();
    }
    
    public void SetDescription(string description)
    {
        _description.SetDescription(description);
    }

    public void HideDescription()
    {
        _description.Close();
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
