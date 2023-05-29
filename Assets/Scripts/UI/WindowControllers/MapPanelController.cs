using System;
using UnityEngine;

public class MapPanelController : WindowController
{
    [SerializeField] private LocationMapController _location;
    [SerializeField] private GameObject _miniMaps;

    private void OnDisable()
    {
        _miniMaps.SetActive(true);
        _location.Hide();
    }

    public void OpenMap(MiniMapController map)
    {
        _miniMaps.SetActive(false);
        _location.OpenMap(map.Map.Map);
    }

    public void HideMap()
    {
        _location.Hide();
        _miniMaps.SetActive(true);
    }
}