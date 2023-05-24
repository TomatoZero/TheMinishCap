using DefaultNamespace;
using UnityEngine;

[CreateAssetMenu(fileName = "KinstonePieceData", menuName = "Items/KinstonePiece")]
public class KinstonePieceData : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private Sprite _icon;
    [SerializeField] private KinstonePieceType _type;
    [SerializeField] private int _class;
    [SerializeField] private int _number;
    [SerializeField] private int _countInBag;
    
    public string Name => _name;
    public Sprite Icon => _icon;
    public KinstonePieceType Type => _type;
    public int Class => _class; 
    public int Number => _number;
    public int CountInBag => _countInBag;
}
