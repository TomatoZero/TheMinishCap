using System;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private MovementController _movementController;
    [SerializeField] private Animator _animator;

    private State _prevState;

    private void Update()
    {
        _animator.SetFloat("Horizontal", _movementController.DirectionView.x);
        _animator.SetFloat("Vertical", _movementController.DirectionView.y);
        _animator.SetFloat("CurrentSpeed",
            _movementController.CurrentSpeed * _movementController.MoveDirection.sqrMagnitude);
  
        switch (_movementController.CurrentState)
        {
            case State.Ground:
                if (_prevState == State.FallInWatter) _animator.SetBool("Drowning", false);
                else if (_prevState == State.FallFromEdge) _animator.SetBool("Fall", false);
                else if(_prevState == State.Climb) _animator.SetBool("Climb", false);
                break;
        }

        _prevState = _movementController.CurrentState;
    }

    public void TakeDamageEventHandler(int hp, State state, Vector2 position)
    {
        switch (state)
        {
            case State.PushAway:
            case State.PushAwayPrepare:
                _animator.SetTrigger("PushAway");
                break;
            case State.FallFromEdge:
                _animator.SetBool("Fall", true);
                break;
            case State.FallInWatter:
                _animator.SetBool("Drowning", true);
                break;
        }
    }

    public void ClimbStartEventHandler()
    {
        var isStart = _prevState != State.Climb;
        _animator.SetBool("Climb", isStart);
    }
    
    public void ClimbEndEventHandler()
    {
        _animator.SetBool("Climb", false);
    }

    public void UseWeaponEventHandler()
    {
        _animator.SetBool("SwordAttack", true);
    }

    public void ReleaseWeaponEventHandler()
    {
        _animator.SetBool("SwordAttack", false);
    }
}