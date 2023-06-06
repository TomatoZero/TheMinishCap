using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private HealthController _healthController;
    [SerializeField] private MovementController _movementController;
    [SerializeField] private WeaponsController _weaponsController;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    [FormerlySerializedAs("_climbStart")] [SerializeField] private UnityEvent _climbStartEvent;
    [FormerlySerializedAs("_climbEnd")] [SerializeField] private UnityEvent _climbEndEvent;
    
    private List<SpriteRenderer> _childSprite;

    public Vector2 MoveDirection
    {
        get => _movementController.MoveDirection;
        set => _movementController.MoveDirection = value;
    }

    private void Awake()
    {
        _childSprite = GetComponentsInChildren<SpriteRenderer>().ToList();
    }

    public void ChangeLayer(string layer)
    {
        _spriteRenderer.sortingLayerName = layer;
        _weaponsController.ChangeSortingLayerInWeapon(layer);
        
        foreach (var spriteRenderer in _childSprite)
        {
            Debug.Log($"{spriteRenderer.GameObject().name}");
            spriteRenderer.sortingLayerName = layer;
        }
    }

    public void ClimbingStart() => _climbStartEvent.Invoke();
    public void ClimbingEnd() => _climbEndEvent.Invoke();

}
