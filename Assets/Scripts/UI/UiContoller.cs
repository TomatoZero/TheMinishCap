using System.Collections.Generic;
using UnityEngine;

public class UiContoller : MonoBehaviour
{
    [SerializeField] private ItemsContoller _itemsPanel;
    [SerializeField] private StatisticPanelController _statisticPanel;
    [SerializeField] private MapPanelController _mapPanel;
    
    [SerializeField] private DungeonMapPanelController _dungeonMapPanel;
    
    [SerializeField] private DescriptionController _description;
    [SerializeField] private List<GameObject> _windows;

    private void Start()
    {
        
    }

    public void NextWindow(GameObject currentWindow)
    {
        currentWindow.SetActive(false);
        var currentIndex = _windows.IndexOf(currentWindow);
        
        if(currentIndex + 1 < _windows.Count) _windows[currentIndex + 1].SetActive(true);
        else _windows[0].SetActive(true);
    }
    
    public void PrevWindow(GameObject currentWindow)
    {
        currentWindow.SetActive(false);
        var currentIndex = _windows.IndexOf(currentWindow);
        
        if(currentIndex - 1 >= 0) _windows[currentIndex - 1].SetActive(true);
        else _windows[^1].SetActive(true);
    }

    public void SetDescription(string description)
    {
        _description.SetDescription(description);
    }
    

    public void HideDescription()
    {
        _description.Close();
    }
}