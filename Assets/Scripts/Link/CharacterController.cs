using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private HealthController _healthController;
    private MovementController _movementController;

    private List<SpriteRenderer> _childSprite;

    public Vector2 MoveDirection
    {
        get => _movementController.MoveDirection;
        set => _movementController.MoveDirection = value;
    }

    private void Awake()
    {
        _childSprite = this.GetComponents<SpriteRenderer>().ToList();

        _healthController = GetComponent<HealthController>();
        _movementController = GetComponent<MovementController>();
    }

    public void ChangeLayer(string layer)
    {
        foreach (var spriteRenderer in _childSprite)
        {
            spriteRenderer.sortingLayerName = layer;
        }

        var layInt = LayerMask.NameToLayer(layer);
        gameObject.layer = layInt;
    }

    public void Roll() => _movementController.Roll();
    public void ClimbingStart() => _movementController.ClimbingStart();
    public void ClimbingEnd() => _movementController.ClimbingEnd();
    public void Fall() => _movementController.FallFromEdge();
}
