using System;
using System.Collections;
using UnityEngine;

public class EnemyMoveController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private float _pushSpeed;
    [SerializeField] private float _pushTime;
    
    private bool _push;
    private Vector2 _direction;

    private void FixedUpdate()
    {
        if (_push)
        {
            _rigidbody.MovePosition(_rigidbody.position + _direction * (_pushSpeed * Time.fixedDeltaTime));
        }
    }

    public void PushAway(Vector2 direction)
    {
        _push = true;
        _direction = direction;
        StartCoroutine(Timer());
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(_pushTime);
        _push = false;
    }
}