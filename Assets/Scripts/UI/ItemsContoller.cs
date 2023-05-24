using System;
using TMPro;
using UnityEngine;

public class ItemsContoller : MonoBehaviour
{
    [SerializeField] private WeaponItem _sword;
    [SerializeField] private WeaponItem _jar;
    [SerializeField] private WeaponItem _staff;
    [SerializeField] private WeaponItem _boomerang;
    [Space]
    [SerializeField] private WeaponItem _shield;
    [SerializeField] private WeaponItem _mitts;
    [SerializeField] private WeaponItem _lanter;
    [SerializeField] private WeaponItem _bomb;
    [Space]
    [SerializeField] private WeaponItem _boots;
    [SerializeField] private WeaponItem _cape;
    [SerializeField] private WeaponItem _ocarina;
    [SerializeField] private WeaponItem _bow;
    [Space] 
    [SerializeField] private TextMeshPro _description;
    [SerializeField] private InventoryData _inventory;
    
    private void Awake()
    {
        // for (var i = 0; i < _inventory.Weapons.Count; i++)
        // {
        //     Debug.Log($"{i}: {_inventory.Weapons[i]}");
        // }
    }

    private void Start()
    {
        _sword.Data = _inventory.GetWeaponByType("Sword");
        _jar.Data = _inventory.GetWeaponByType("GustJar");
        _staff.Data = _inventory.GetWeaponByType("CaneOfPacci");
        _boomerang.Data = _inventory.GetWeaponByType("Boomerang");
        
        _shield.Data = _inventory.GetWeaponByType("Shield");
        _mitts.Data = _inventory.GetWeaponByType("MoleMitts");
        _lanter.Data = _inventory.GetWeaponByType("FlameLantern");
        _bomb.Data = _inventory.GetWeaponByType("Bomb");
        
        _boots.Data = _inventory.GetWeaponByType("PegasusBoots");
        _cape.Data = _inventory.GetWeaponByType("RocsCape");
        _ocarina.Data = _inventory.GetWeaponByType("OcarinaOfWind");
        _bow.Data = _inventory.GetWeaponByType("Bow");
    }

    public void HidePanel()
    {
        this.gameObject.SetActive(false);
    }
}
