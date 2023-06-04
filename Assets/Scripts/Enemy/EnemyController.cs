using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public void Dead()
    {
        gameObject.SetActive(false);
    }
}