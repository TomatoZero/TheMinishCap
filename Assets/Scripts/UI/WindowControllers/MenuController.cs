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
    private SubWindowController _currentSubWindow;

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
        if(_currentSubWindow != null) _currentSubWindow.Hide();
        if(_windows[_currentWindow].gameObject.activeSelf)
            _windows[_currentWindow].Hide();

        if (_currentWindow + 1 < _windows.Count)
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
        if(_currentSubWindow != null) _currentSubWindow.Hide();
        if(_windows[_currentWindow].gameObject.activeSelf) _windows[_currentWindow].Hide();

        if (_currentWindow - 1 >= 0)
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

    public void OpenSubWindow(SubWindowController subWindow)
    {
        subWindow.Show();
        _currentSubWindow = subWindow;
    }

    public void CloseSubWindow()
    {
        if(_currentSubWindow == null) return;
        
        _currentSubWindow.Hide();
        _currentSubWindow = null;
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