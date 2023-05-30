using UnityEngine;

[CreateAssetMenu(fileName = "TigerScrollData", menuName = "Items/TigerScroll")]
public class TigerScrollData : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private Sprite _icon;
    [SerializeField] private Sprite _description;

    public string Name => _name;
    public Sprite Icon => _icon;
    public Sprite Description => _description;
}
