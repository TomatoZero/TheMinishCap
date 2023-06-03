using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private HealthController _healthController;
    [SerializeField] private MovementController _movementController;
    [SerializeField] private WeaponsController _weaponsController;
    private List<SpriteRenderer> _childSprite;

    public Vector2 MoveDirection
    {
        get => _movementController.MoveDirection;
        set => _movementController.MoveDirection = value;
    }

    private void Awake()
    {
        _childSprite = this.GetComponents<SpriteRenderer>().ToList();
    }

    public void ChangeLayer(string layer)
    {
        foreach (var spriteRenderer in _childSprite)
        {
            spriteRenderer.sortingLayerName = layer;
        }
    }

    public void Roll() => _movementController.Roll();
    public void ClimbingStart() => _movementController.ClimbingStart();
    public void ClimbingEnd() => _movementController.ClimbingEnd();

    public void UseFirstWeapon() => _weaponsController.UseFirstWeapon();
    public void ReleaseFirstWeapon() => _weaponsController.ReleaseFirstWeapon();
}
