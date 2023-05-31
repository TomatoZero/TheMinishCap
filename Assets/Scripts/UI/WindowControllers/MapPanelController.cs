using UnityEngine;

public class MapPanelController : WindowController
{
    [SerializeField] private LocationMapController _location;
    [SerializeField] private GameObject _miniMaps;

    private void OnEnable()
    {
        MenuController.FirstSelected = FirstSelected;
    }

    public void OpenMap(MiniMapController map)
    {
        _location.CurrentMap = map.Map.Map;
        MenuController.OpenSubWindow(_location);
    }

    public void HideMap()
    {
        _location.Hide();
        _miniMaps.SetActive(true);
    }
}
