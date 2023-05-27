using UnityEngine;
using UnityEngine.UI;

public class IventoryItem : MonoBehaviour
{
    [SerializeField] private Image _ico;
    [SerializeField] private UiContoller _uiController;
        
    private ItemData _item;

    public ItemData Item
    {
        get => _item;
        set
        {
            _item = value;
            SetItem();
        }
    }

    private void Awake()
    {
        
    }

    private void SetItem()
    {
        if (_item != null)
        {
            _ico.sprite = _item.Icon;
        }
        else
        {
            _ico.sprite = null;
            _ico.color = Color.clear;
        }
    }
    
    public void Selected()
    {
        if(_item != null) _uiController.SetDescription(_item.Name);
    }

    public void Away()
    {
        _uiController.HideDescription();
    }
}
