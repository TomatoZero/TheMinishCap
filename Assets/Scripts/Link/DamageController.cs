using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour
{
    [SerializeField] private Collider2D _collider;
    [SerializeField] private MovementController _movementController;

    [SerializeField] private PlayerTakeDamageEvent _takeDamageEvent;
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private LayerMask _rollColliderIgnore;

    private Collider2D[] _childCollider;
    private List<Collider2D> _otherColliders;
    
    private bool _isRoll = false;
    private bool _canStartTimer = true;

    public bool IsRoll
    {
        get => _isRoll;
        set
        {
            _isRoll = value;
            
            if(_isRoll)
            {
                if (_canStartTimer)
                    StartCoroutine(EnableLayerCollision());
            }
                
        }
    }

    private void Awake()
    {
        _childCollider = this.GetComponentsInChildren<Collider2D>();
        _otherColliders = new List<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_movementController.CurrentState == State.FallFromEdge) return;
        if (other.CompareTag("Water"))
        {
            _takeDamageEvent.Invoke(1, State.FallInWatter, other.transform.position);
        }
        else if (other.CompareTag("Deep"))
        {
            _takeDamageEvent.Invoke(1, State.FallFromEdge, other.transform.position);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (_movementController.CurrentState == State.FallFromEdge) return;
        if (other.collider.CompareTag("Enemy"))
        {
            if (!IsRoll) _takeDamageEvent.Invoke(1, State.PushAwayPrepare, other.transform.position);
        }
    }

    public void RollEventHandler()
    {
        IsRoll = true;
    }

    private IEnumerator EnableCollision()
    {
        _canStartTimer = false;
        
        yield return new WaitForSeconds(_movementController.RollTime);
        IgnoreCollision(false);
        _otherColliders = new List<Collider2D>();
        
        _canStartTimer = true;
        _isRoll = false;
    }

    private IEnumerator EnableLayerCollision()
    {
        _canStartTimer = false;
        
        Debug.Log($"111111");
        
        Physics2D.IgnoreLayerCollision(7, 3, true);
        yield return new WaitForSeconds(_movementController.RollTime);
        Physics2D.IgnoreLayerCollision(7, 3, false);

        _canStartTimer = true;
        _isRoll = false;
    }

    private void IgnoreCollision(bool mod)
    {
        foreach (var otherCollider in _otherColliders)
        {
            Physics2D.IgnoreCollision(_collider, otherCollider, mod);
            var otherChildren = otherCollider.transform.GetComponentsInChildren<Collider2D>();

            foreach (var otherChild in otherChildren)
            {
                Physics2D.IgnoreCollision(_collider, otherChild, mod);

                foreach (var child in _childCollider)
                {
                    Physics2D.IgnoreCollision(child, otherChild, mod);
                }
            }
        }
    }

    private void IgnoreCollision(Collision2D other, bool mod)
    {
        Physics2D.IgnoreCollision(_collider, other.collider, mod);

        var otherChildren = other.transform.GetComponentsInChildren<Collider2D>();

        foreach (var otherChild in otherChildren) Physics2D.IgnoreCollision(_collider, otherChild, mod);

        foreach (var otherChild in otherChildren)
        {
            foreach (var child in _childCollider)
            {
                Physics2D.IgnoreCollision(child, otherChild, mod);
            }
        }
    }

    public void EnemyDamage(int hp, Vector2 position)
    {
        _takeDamageEvent.Invoke(hp, State.PushAwayPrepare, position);
    }
}