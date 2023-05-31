using UnityEngine;

public class MapPanelController : WindowController
{
    [SerializeField] private LocationMapController _location;
    [SerializeField] private GameObject _miniMaps;

    private void OnEnable()
    {
        MenuController.FirstSelected = FirstSelected;
    }

    private void OnDisable()
    {
        // _miniMaps.SetActive(true);
        // _location.Hide();
    }

    public void OpenMap(MiniMapController map)
    {
        _location.CurrentMap = map.Map.Map;
        MenuController.OpenSubWindow(_location);
        
        // _miniMaps.SetActive(false);
        // _location.OpenMap(map.Map.Map);
    }

    public void HideMap()
    {
        _location.Hide();
        _miniMaps.SetActive(true);
    }
}
