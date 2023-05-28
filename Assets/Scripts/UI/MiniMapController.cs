using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MiniMapController : MonoBehaviour
{
    [SerializeField] private MapData _map;
    [SerializeField] private Image _miniMap;
    [SerializeField] private UiContoller _uiController;
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
        _uiController.SetDescription(_map.Name);
    }

    public void Away()
    {
        _uiController.HideDescription();
    }
}