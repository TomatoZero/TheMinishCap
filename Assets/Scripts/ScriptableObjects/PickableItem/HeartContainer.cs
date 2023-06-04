using UnityEngine;

[CreateAssetMenu(fileName = "HeartContainer", menuName = "Items/HeartContainer")]
public class HeartContainer : ItemData
{
    [SerializeField] private int numbHeartbeat;
    
    public int NumbHeartbeat => numbHeartbeat;
}