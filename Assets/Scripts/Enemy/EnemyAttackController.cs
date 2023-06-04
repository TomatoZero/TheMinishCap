using UnityEngine;
using UnityEngine.Events;

public class EnemyAttackController : MonoBehaviour
{
    [SerializeField] private UnityEvent _attackEvent;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player hit");
            _attackEvent.Invoke();
        }
    }

    public void ChangeWeaponDirection(Vector2 direction)
    {
        var targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        targetAngle += 180;
        transform.rotation = Quaternion.Euler(0f, 0f, targetAngle);
    }
}