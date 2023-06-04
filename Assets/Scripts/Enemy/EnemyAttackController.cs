using System;
using UnityEngine;
using UnityEngine.Events;

public class EnemyAttackController : MonoBehaviour
{
    [SerializeField] private UnityEvent _attackEvent;
    [SerializeField] private EnemyMoveController _movementController;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _attackEvent.Invoke();

            if (other.TryGetComponent(out DamageController player))
            {
                player.EnemyDamage(1, transform.position);
            }
            else
            {
                throw new Exception("Object with player tag and without DamgeController");
            }

        }
    }

    public virtual void ChangeWeaponDirection(Vector2 direction)
    {
        var targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        targetAngle += 180;
        transform.rotation = Quaternion.Euler(0f, 0f, targetAngle);
    }
}