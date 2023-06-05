using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour
{
    [SerializeField] private ItemData _data;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private UnityEvent _pickUpEvent;

    public ItemData Data => _data;

    private void Awake()
    {
        _spriteRenderer.sprite = _data.Icon;
    }

    public void PicUp()
    {
        gameObject.SetActive(false);
        Destroy(this);
        _pickUpEvent.Invoke();
    }
    
}