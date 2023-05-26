using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiContoller : MonoBehaviour
{
    [SerializeField] private ItemsContoller _itemsPanel;
    [SerializeField] private StatisticPanelController _statisticPanel;
    [SerializeField] private MapPanelController _mapPanel;
    
    [SerializeField] private DungeonMapPanelController _dungeonMapPanel;
    
    [SerializeField] private DescriptionController _description;


    private void Start()
    {
        
    }

    public void OpenPanel(GameObject panel)
    {
        panel.SetActive(true);
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