using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TigerScrollItem : MonoBehaviour
{
    [SerializeField] private TigerScrollData _tigerScroll;
    [SerializeField] private Image _ico;

    public TigerScrollData TigerScroll => _tigerScroll;

    private void Awake()
    {
        _ico.sprite = _tigerScroll.Icon;
    }
}
