using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _spawnObject;
    [SerializeField] private int _count;
    [SerializeField] private string _lvl = "Lvl0";
    
    private void Awake()
    {
        for (var i = 0; i < _count; i++) Spawn();
    }

    public void Spawn()
    {
        StartCoroutine(Spawne());
    }

    private IEnumerator Spawne()
    {
        yield return new WaitForSeconds(0.5f);
        
        var snake = Instantiate(_spawnObject, transform.position, Quaternion.identity, transform);
        snake.TryGetComponent(out SpawnableObject controller);
        controller?.DestroyEvent.AddListener(Spawn);
        controller?.SetLvl(_lvl);
    }
}