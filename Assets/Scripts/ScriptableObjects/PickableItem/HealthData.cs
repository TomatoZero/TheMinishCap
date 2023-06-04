using UnityEngine;

[CreateAssetMenu(fileName = "HealthData", menuName = "Items/Health")]
public class HealthData : ItemData
{
    [SerializeField] private int _numbHealthRestore;

    public int NumbHealthRestore => _numbHealthRestore;
}
