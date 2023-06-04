using UnityEngine;

[CreateAssetMenu(fileName = "HeartContainer", menuName = "Items/HeartContainer")]
public class HeartContainerData : ItemData
{
    [SerializeField] private int numbHeartbeat;
    
    public int NumbHeartbeat => numbHeartbeat;
}