using System;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private ItemData _data;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer.sprite = _data.Icon;

        var x = _data as HealthData;
        Debug.Log($"Test item {x.NumbHealthRestore}");
    }

    protected void SetSprite(Sprite sprite)
    {
        _spriteRenderer.sprite = sprite;

        var x = _data as HealthData;
        Debug.Log($"Test item {x.NumbHealthRestore}");
    }
}