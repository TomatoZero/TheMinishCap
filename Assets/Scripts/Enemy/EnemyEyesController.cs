using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyEyesController : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _collider;
    
    private bool _isLookForPlayer = true;
    protected int _layerMask;
    
    public BoxCollider2D Collider => _collider;
    

    private void Awake()
    {
        _layerMask = ~(LayerMask.GetMask("Ignore Raycast", "Enemy"));
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(!_isLookForPlayer) return;
        if (other.CompareTag("Player"))
        {
            ItIsVisible(other.transform.position);
        }
    }

    protected abstract void ItIsVisible(Vector2 enemyPosition);

    public virtual List<Vector2> CheckAvailableDirection()
    {
        var availableDirection = new List<Vector2>();

        if (CheckOneDirection(Vector2.up)) availableDirection.Add(Vector2.up);
        if (CheckOneDirection(Vector2.down)) availableDirection.Add(Vector2.down);
        if (CheckOneDirection(Vector2.right)) availableDirection.Add(Vector2.right);
        if (CheckOneDirection(Vector2.left)) availableDirection.Add(Vector2.left);

        return availableDirection;
    }
    
    protected virtual bool CheckOneDirection(Vector2 direction)
    {
        var hasHit = Physics2D.BoxCast(transform.position, _collider.size / 2f, 0f,
            direction,1f, _layerMask);
        
        // Debug.Log($"Hit {hasHit.transform.name} {hasHit.transform.tag}");
        
        Debug.DrawRay(transform.position, direction, Color.yellow, 1);
        
        return !hasHit;
    }
    
    private void OnDrawGizmosSelected()
    {
        if(!_isLookForPlayer) return;
        
        Gizmos.color = Color.black;
        
        Gizmos.DrawRay(transform.position, Vector2.up * 3f);
        Gizmos.DrawRay(transform.position, Vector2.down * 3f);
        Gizmos.DrawRay(transform.position, Vector2.right * 3f);
        Gizmos.DrawRay(transform.position, Vector2.left * 3f);
    }

    public void SetLookingMode(bool activeMode)
    {
        _isLookForPlayer = activeMode;
    }
}