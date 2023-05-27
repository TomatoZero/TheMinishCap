using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Items/Item")]
public class ItemData : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private Sprite _icon;

    public string Name => _name;
    public Sprite Icon => _icon;
}
