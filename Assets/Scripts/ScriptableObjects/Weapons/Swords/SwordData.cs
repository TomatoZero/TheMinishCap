using UnityEngine;

[CreateAssetMenu(fileName = "SwordData", menuName = "Items/Sword")]
public class SwordData : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private Sprite _icon;
    [SerializeField] private Sprite _firstWeaponSprite;
    [SerializeField] private Sprite _secondWeaponSprite;
    [SerializeField] private int _numCopies;
}
