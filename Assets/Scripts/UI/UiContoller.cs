using System;
using UnityEngine;
using UnityEngine.Serialization;

public class UiContoller : MonoBehaviour
{
    [SerializeField] private ItemsContoller _itemsPanel;
    [SerializeField] private StatisticPanelController _statisticPanel;
    [SerializeField] private MapPanelController _mapPanel;
    [SerializeField] private DungeonMapPanelController _dungeonMapPanel;

    private void Start()
    {
        
    }
}