using UnityEngine;

[CreateAssetMenu(fileName = "MapData", menuName = "Items/Map")]
public class MapData : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private Sprite _map;
    [SerializeField] private Sprite _miniMap;

    public string Name => _name;
    public Sprite Map => _map;
    public Sprite MiniMap => _miniMap;
}