using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private ItemData _data;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    public ItemData Data => _data;

    private void Awake()
    {
        _spriteRenderer.sprite = _data.Icon;
    }

    public void PicUp()
    {
        Destroy(this);
    }
    
}