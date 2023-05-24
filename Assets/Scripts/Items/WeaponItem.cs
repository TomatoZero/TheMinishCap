using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WeaponItem : MonoBehaviour
{
    [SerializeField] private Image _ico;
        
    private WeaponData _data;

    public WeaponData Data
    {
        get => _data;
        set
        {
            _data = value;
            SetItem();
        }
    }

    private void Awake()
    {
        // if (_data != null)
        // {
        //     _ico.sprite = _data.Icon;
        // }
        // else
        // {
        //     _ico.sprite = null;
        //     _ico.color = Color.clear;
        // }
    }

    private void SetItem()
    {
        if (_data != null)
        {
            _ico.sprite = _data.Icon;
        }
        else
        {
            _ico.sprite = null;
            _ico.color = Color.clear;
        }
    }
    
    private void SetAsSelected()
    {
        
    }
}
