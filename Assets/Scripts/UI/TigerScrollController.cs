using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TigerScrollController : SubWindow
{
    [SerializeField] private GameObject _container;
    [SerializeField] private Image _description;
    [SerializeField] private TMP_Text _scrollName;

    public void PointEnter(TigerScrollItem scroll)
    {
        _description.sprite = scroll.TigerScroll.Description;
        _scrollName.text = scroll.TigerScroll.Name;
    }

    public void PointExit()
    {
        
    }
}
