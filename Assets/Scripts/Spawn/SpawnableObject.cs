using UnityEngine;
using UnityEngine.Events;

public class SpawnableObject : MonoBehaviour
{
    [SerializeField] private UnityEvent _destroyEvent;
    public UnityEvent DestroyEvent => _destroyEvent;

    public void Destroy()
    {
        _destroyEvent.Invoke();
    }
}
