using UnityEngine;

[CreateAssetMenu(fileName = "SwordData", menuName = "Items/Sword")]
public class SwordData : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private Sprite _icon;
    [SerializeField] private Sprite _inGame;
    [SerializeField] private Sprite _inHand;
    [SerializeField] private int _numCopies;
}
