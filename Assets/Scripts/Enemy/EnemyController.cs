using UnityEngine;
using UnityEngine.Events;

public class EnemyController : MonoBehaviour
{
    public void Dead()
    {
        gameObject.SetActive(false);
    }
}