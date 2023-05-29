using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatisticPanelController : WindowController
{
    [SerializeField] private IventoryItem _earthElement;
    [SerializeField] private IventoryItem _waterElement;
    [SerializeField] private IventoryItem _fireElement;
    [SerializeField] private IventoryItem _windElement;
    [Space]
    [SerializeField] private IventoryItem _gripRing;
    [SerializeField] private IventoryItem _powerBracelet;
    [SerializeField] private IventoryItem _flippers;
    [Space]
    [SerializeField] private TMP_Text _tigerScrollsCount;
    [SerializeField] private TMP_Text _secretSeashellCount;
    [SerializeField] private TMP_Text _heartsCounter;
    [SerializeField] private Image _heartsCounterImage;
    [Space]
    [SerializeField] private WindowController _kinstonePieceBug;
    [SerializeField] private WindowController _tigerScrolls;
    
    public override void Hide()
    {
        _kinstonePieceBug.Hide();
        _tigerScrolls.Hide();
        base.Hide();
    }

    public void OpenSubWindow(WindowController subWindow)
    {
        subWindow.Show();
        gameObject.SetActive(false);
    }

    public void HideSubWindow(WindowController subWindow)
    {
        subWindow.Hide();
        gameObject.SetActive(true);
    }
    
    
}
