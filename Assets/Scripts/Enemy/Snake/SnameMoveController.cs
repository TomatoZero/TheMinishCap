using UnityEngine;

public class SnameMoveController : EnemyMoveController
{
    private void OnEnable()
    {
        MoveDirection = Vector2.zero;
    }

    private void FixedUpdate()
    {
        Debug.Log("Snake test");
    }
}