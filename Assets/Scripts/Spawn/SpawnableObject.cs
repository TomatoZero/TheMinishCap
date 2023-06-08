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

    public void SetLvl(string lvl)
    {
        if (gameObject.TryGetComponent(out SpriteRenderer spriteRenderer))
        {
            spriteRenderer.sortingLayerName = lvl;
            spriteRenderer.sortingOrder = 1;
        }

        var children = transform.GetComponentInChildren<SpriteRenderer>();

        if (children != null)
        {
            children.sortingLayerName = lvl;
            children.sortingOrder = 1;
        }
    }
}
