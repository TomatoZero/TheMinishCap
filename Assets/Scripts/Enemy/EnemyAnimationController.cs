using System;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    [SerializeField] private EnemyMoveController _moveController;
    [SerializeField] private Animator _animator;
    
    private void Update()
    {
        _animator.SetFloat("Horizontal", _moveController.DirectionView.x);
        _animator.SetFloat("Vertical", _moveController.DirectionView.y);
        
    }
}
