using UnityEngine;
using UnityEngine.UI;

public class WeaponItem : MonoBehaviour
{
    [SerializeField] private Image _ico;
    [SerializeField] private UiContoller _uiController;
        
    private WeaponData _weapon;

    public WeaponData Weapon
    {
        get => _weapon;
        set
        {
            _weapon = value;
            SetItem();
        }
    }

    private void Awake()
    {
        
    }

    private void SetItem()
    {
        if (_weapon != null)
        {
            _ico.sprite = _weapon.Icon;
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
    
    public void Selected()
    {
        if(_weapon != null) _uiController.SetDescription(_weapon.Name);
    }

    public void Away()
    {
        _uiController.HideDescription();
    }
}
