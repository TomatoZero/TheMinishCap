using System;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    [SerializeField] private EnemyMoveController _moveController;
    [SerializeField] private Animator _animator;
    [SerializeField] private Animator _effectAnimator;
    
    private void Update()
    {
        _animator.SetFloat("Horizontal", _moveController.DirectionView.x);
        _animator.SetFloat("Vertical", _moveController.DirectionView.y);
    }

    public void PushAwayEventHandler(Vector2 direction)
    {
        // _animator.SetBool("Hit", true);
        // _effectAnimator.SetBool("Hit", true);
        
        _animator.SetTrigger("Hit");
        _effectAnimator.SetTrigger("Hit");
    }

    public void DieEventHandler()
    {
        _effectAnimator.SetTrigger("Die");
    }
}
