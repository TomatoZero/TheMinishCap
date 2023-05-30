using System;
using UnityEngine;
using UnityEngine.UI;

public class MiniMapController : MonoBehaviour
{
    [SerializeField] private MapData _map;
    [SerializeField] private Image _miniMap;
    [SerializeField] private MenuController _menuController;
    [SerializeField] private UiController _uiController;
    public MapData Map => _map;

    private void Awake()
    {
        if (_map != null)
        {
            _miniMap.sprite = _map.MiniMap;
        }
        else
        {
            _miniMap.sprite = null;
            _miniMap.color = Color.clear;
        }
    }

    public void Selected()
    {
        _menuController.SetDescription(_map.Name);
        _uiController.SetFirstButtonHint("Select");
    }

    public void Away()
    {
        _menuController.HideDescription();
        _uiController.HideButtonHint(0);
    }
    
}