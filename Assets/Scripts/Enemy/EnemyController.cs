using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Spawner _spawner;

    public void SetSpawner(Spawner spawner)
    {
        _spawner = spawner;
    }

    public void Dead()
    {
        gameObject.SetActive(false);
        _spawner.CreateInstance();
    }
}