using System;
using UnityEngine;

public class SnakeEyesController : EnemyEyesController
{
    [SerializeField] private SnakeAttackEvent _attackEvent;

    protected override void ItIsVisible(Vector2 enemyPosition)
    {
        // Debug.Log("check player");
        
        var snakePos = transform.position;

        if (Math.Abs(snakePos.x - enemyPosition.x) < 0.2 || Math.Abs(snakePos.y - enemyPosition.y) < 0.2)
        {
            Debug.Log($"{enemyPosition}");
            _attackEvent.Invoke(enemyPosition);
        }
    }
    
    
}