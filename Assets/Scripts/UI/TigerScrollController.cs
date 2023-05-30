using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TigerScrollController : WindowController
{
    [SerializeField] private GameObject _container;
    [SerializeField] private Image _description;
    [SerializeField] private TMP_Text _scrollName;

    private void OnEnable()
    {
        MenuController.FirstSelected = FirstSelected;
    }

    public void PointEnter(TigerScrollItem scroll)
    {
        _description.sprite = scroll.TigerScroll.Description;
        _scrollName.text = scroll.TigerScroll.Name;
    }

    public void PointExit()
    {
        
    }
}
