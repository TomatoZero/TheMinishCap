using UnityEngine;

public abstract class EnemyEyesController : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ItIsVisible(other.transform.position);
        }
    }

    protected abstract void ItIsVisible(Vector2 enemyPosition);

    public void SetActive(bool activeMode)
    {
        gameObject.SetActive(activeMode);
    }
}