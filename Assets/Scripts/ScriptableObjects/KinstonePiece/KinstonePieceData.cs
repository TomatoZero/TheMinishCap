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
}
