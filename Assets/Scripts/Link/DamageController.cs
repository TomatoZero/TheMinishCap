using UnityEngine;
using UnityEngine.Events;

public class DamageController : MonoBehaviour
{
    [SerializeField] private MovementController _movementController;
    [SerializeField] private UnityEvent _trapDamageEvent;
    [SerializeField] private PlayerTakeDamageEvent _enemyDamageEvent;
    [SerializeField] private UnityEvent _fallFromTheEdgeEvent;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_movementController.CurrentState == State.AfterDeath) return;
        if (other.CompareTag("Water"))
        {
            _fallFromTheEdgeEvent.Invoke();
        }
        else if (other.CompareTag("Deep"))
        {
            _fallFromTheEdgeEvent.Invoke();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (_movementController.CurrentState == State.AfterDeath) return;
        if (other.collider.CompareTag("Enemy"))
        {
            var pushDirection = new Vector2(-_movementController.MoveDirection.x, -_movementController.MoveDirection.y);
            _enemyDamageEvent.Invoke(1);
            _movementController.PushAway(pushDirection);
        }
    }

    public void EnemyDamage(int hp, Vector2 direction)
    {
        _enemyDamageEvent.Invoke(hp);
        _movementController.PushAway(direction);
    }
}