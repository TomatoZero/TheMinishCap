using UnityEngine;

[CreateAssetMenu(fileName = "ShieldData", menuName = "Items/Shield")]
public class ShieldData : InventoryItemData
{
    [SerializeField] private Sprite _side;
    [SerializeField] private Sprite _front;

    public Sprite Side => _side;
    public Sprite Front => _front;
}
