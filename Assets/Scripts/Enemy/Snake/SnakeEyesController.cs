using System;
using Unity.VisualScripting;
using UnityEngine;

public class SnakeEyesController : EnemyEyesController
{
    [SerializeField] private SnakeAttackEvent _attackEvent;
    
    private bool b = false;
    private Vector2 _direction;
    private int _layerMask;
    
    private void Awake()
    {
        _layerMask = ~(LayerMask.GetMask("Ignore Raycast", "Enemy"));
    }

    protected override void ItIsVisible(Vector2 enemyPosition)
    {
        var snakePos = transform.position;

        if (Math.Abs(snakePos.x - enemyPosition.x) < 0.2 || Math.Abs(snakePos.y - enemyPosition.y) < 0.2)
        {
            b = true;
            _direction = (enemyPosition - (Vector2)this.transform.position);
            var hasHit = Physics2D.Raycast((Vector2)transform.position, _direction.normalized, _direction.magnitude, _layerMask);

            var hasHit2 = Physics2D.BoxCast(transform.position, Collider.size / 2f, 0f,
                _direction.normalized,_direction.magnitude, _layerMask);
            
            if(hasHit2 && hasHit2.transform.name == "Link")
                _attackEvent.Invoke(enemyPosition);

        }
        else
        {
            b = false;
        }
    }

    private void OnDrawGizmos()
    {
        if(b)
        {
            Gizmos.color = Color.magenta;

            Gizmos.DrawRay(transform.position, _direction.normalized * _direction.magnitude);
        }
    }
}