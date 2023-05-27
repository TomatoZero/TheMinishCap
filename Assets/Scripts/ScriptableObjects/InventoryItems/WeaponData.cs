using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Items/WeaponItem")]
public class WeaponData : ItemData
{
    [SerializeField] private Sprite _side;
    [SerializeField] private Sprite _front;
    [SerializeField] private Sprite _inGame;
    [SerializeField] private int _lvl;
    
    public Sprite Side => _side;
    public Sprite Front => _front;
    public Sprite InGame => _inGame;
    public int Lvl => _lvl;

}