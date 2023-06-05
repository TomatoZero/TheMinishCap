using System;
using UnityEngine;

public class BaseWeapon : MonoBehaviour
{
    [SerializeField] protected GameObject _collider;
    [SerializeField] private WeaponData _weaponData;
    [SerializeField] private SpriteRenderer _image;

    protected bool IsRelease = false;
    protected bool IsHold = false;
    protected Vector2 Direction;
    
    public delegate void HoldWeaponEventHandler();
    public static event HoldWeaponEventHandler HoldWeaponEvent;
    
    private void Awake()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _image.sprite = _weaponData.InGame;
        MovementController.Rotation += Rotate;
        IsRelease = false;

        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    private void OnDisable()
    {
        MovementController.Rotation -= Rotate;
    }

    private void Rotate(Vector2 direction)
    {
        if(direction == Vector2.zero) return;
        if(IsHold) return;
        
        var targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        targetAngle += 90;
        transform.rotation = Quaternion.Euler(0f, 0f, targetAngle);
        Direction = direction;
        IsHold = true;
    }

    public void ChangeSortingLayer(string layer)
    {
        _image.sortingLayerName = layer;
    }
    
    public virtual void UseWeapon()
    {
        IsRelease = false;
        Debug.Log($"Base attack");
    }

    protected void HoldWeapon()
    {
        HoldWeaponEvent?.Invoke();
    }
    
    public virtual void ReleaseWeapon()
    {
        gameObject.SetActive(false);
        Debug.Log($"Base release weapon");
    }
}