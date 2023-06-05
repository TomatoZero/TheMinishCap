using UnityEngine;
using UnityEngine.Events;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private UnityEvent _enemyDie;

    public UnityEvent EnemyDie => _enemyDie;
    
    public void Dead()
    {
        _enemyDie.Invoke();
        gameObject.SetActive(false);
    }
}