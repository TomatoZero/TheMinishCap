using System;
using Unity.VisualScripting;
using UnityEngine;

public class SnakeEyesController : EnemyEyesController
{
    [SerializeField] private SnakeAttackEvent _attackEvent;
    
    private bool _isPlayerFind = false;
    private Vector2 _direction;
 
    protected override void ItIsVisible(Vector2 enemyPosition)
    {
        var snakePos = transform.position;

        if (Math.Abs(snakePos.x - enemyPosition.x) < 0.2 || Math.Abs(snakePos.y - enemyPosition.y) < 0.2)
        {
            _isPlayerFind = true;
            _direction = (enemyPosition - (Vector2)this.transform.position);
            var hasHit = Physics2D.BoxCast(transform.position, Collider.size / 2f, 0f,
                _direction.normalized,_direction.magnitude, _layerMask);

            // if (hasHit2)
            // {
            //     Debug.Log($"hit info {hasHit2.transform.name} {hasHit2.transform.tag}");
            // }
            
            if(hasHit && hasHit.transform.name == "Link")
                _attackEvent.Invoke(enemyPosition);

        }
        else
        {
            _isPlayerFind = false;
        }
    }

    private void OnDrawGizmos()
    {
        if(_isPlayerFind)
        {
            Gizmos.color = Color.magenta;

            Gizmos.DrawRay(transform.position, _direction.normalized * _direction.magnitude);
        }
    }
}