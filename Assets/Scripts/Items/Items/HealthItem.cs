using UnityEngine;

public class HealthItem : Item
{
    [SerializeField] private HealthData _item;

    public HealthData Item => _item;

    private void Awake()
    {
        SetSprite(_item.Icon);
    }
}