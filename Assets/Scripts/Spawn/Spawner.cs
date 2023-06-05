using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _spawnObject;

    private void Awake()
    {
        Spawn();
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
    }
}