using System;
using UnityEngine;
using UnityEngine.Events;

public class SnakeEyesController : EnemyEyesController
{
    [SerializeField] private SnakeAttackEvent _attackEvent;

    protected override void ItIsVisible(Vector2 enemyPosition)
    {
        var snakePos = transform.position;

        if (Math.Abs(snakePos.x - enemyPosition.x) < 0.1 || Math.Abs(snakePos.y - enemyPosition.y) < 0.1)
        {
            _attackEvent.Invoke(enemyPosition);
        }
    }
}