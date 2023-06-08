using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private UnityEvent _enemyDie;

    public UnityEvent EnemyDie => _enemyDie;
    
    public void Dead()
    {
        _enemyDie.Invoke();
        StartCoroutine(Die());
    }

    private IEnumerator Die()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }
}