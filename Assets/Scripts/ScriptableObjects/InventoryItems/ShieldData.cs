using UnityEngine;

[CreateAssetMenu(fileName = "ShieldData", menuName = "Items/Shield")]
public class ShieldData : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private Sprite _icon;
    [SerializeField] private Sprite _side;
    [SerializeField] private Sprite _front;

    public string Name => _name;
    public Sprite Icon => _icon;
    public Sprite Side => _side;
    public Sprite Front => _front;
}
