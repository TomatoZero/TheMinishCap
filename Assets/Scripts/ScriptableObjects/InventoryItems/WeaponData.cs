using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Items/WeaponItem")]
public class WeaponData : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private Sprite _icon;
    [SerializeField] private Sprite _side;
    [SerializeField] private Sprite _front;
    [SerializeField] private Sprite _inGame;
    [SerializeField] private int _lvl;
    
    public string Name => _name;
    public Sprite Icon => _icon;
    public Sprite Side => _side;
    public Sprite Front => _front;
    public Sprite InGame => _inGame;
    public int Lvl => _lvl;

}