using UnityEngine;

[CreateAssetMenu(fileName = "InventoryItemData", menuName = "Items/InventoryItem")]
public class InventoryItemData : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private Sprite _icon;

    public string Name => _name;
    public Sprite Icon => _icon;
}
