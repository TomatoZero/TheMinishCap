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
            var pushDirection = other.transform.position - transform.position;
            _enemyDamageEvent.Invoke(1);
            _movementController.PushAway(new Vector2(-pushDirection.x, -pushDirection.y).normalized);
        }
    }

    public void EnemyDamage(int hp, Vector2 direction)
    {
        var pushDirection = direction - (Vector2)transform.position;
        _enemyDamageEvent.Invoke(1);
        _movementController.PushAway(new Vector2(-pushDirection.x, -pushDirection.y).normalized);
    }
}