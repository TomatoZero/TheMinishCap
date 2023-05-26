using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private InventoryData _inventory;

    private void Awake()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<WeaponItem>(out var weapon))
        {
            _inventory.AddWeapon(weapon.Weapon);
        }
    }
}
