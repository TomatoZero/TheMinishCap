using DefaultNamespace;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "KinstonePieceData", menuName = "Items/KinstonePiece")]
public class KinstonePieceData : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private Sprite _icon;
    [SerializeField] private KinstonePieceType _type;
    [SerializeField] private int _class;
    [SerializeField] private bool _isPlayerPiece;
    
    public string Name => _name;
    public Sprite Icon => _icon;
    public KinstonePieceType Type => _type;
    public int Class => _class;
    public bool IsPlayerPiece => _isPlayerPiece;
}
