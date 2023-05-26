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
            // var s = _map.MiniMap.bounds.size;
            // _miniMap.sprite = _map.MiniMap;
            //
            // var rectTransform = _miniMap.GetComponent<RectTransform>();
            // rectTransform.sizeDelta = new Vector2(s.x * 28.5f, s.y * 28.5f);
            //
            // var go = this.GetComponent<RectTransform>();
            // go.sizeDelta = new Vector2(s.x * 28.5f, s.y * 28.5f);
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