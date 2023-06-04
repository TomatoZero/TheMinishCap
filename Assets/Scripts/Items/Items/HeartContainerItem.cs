using UnityEngine;

public class HeartContainerItem : Item
{
    [SerializeField] private int _countHeartIncrease;

    public int CountHeartIncrease => _countHeartIncrease;
}