using System;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private int _count;
    
    private void Awake()
    {
        for (var i = 0; i < _count; i++)
        {
            CreateInstance();
        }
    }

    public void CreateInstance()
    {
        if (Instantiate(_prefab, this.transform).TryGetComponent(out EnemyController controller))
        {
            controller.SetSpawner(this);
        }
    }
}