using System;
using UnityEngine;
using UnityEngine.Serialization;

public class ItemsContoller : WindowController
{
    [SerializeField] private IventoryItem _sword;
    [SerializeField] private IventoryItem _jar;
    [SerializeField] private IventoryItem _staff;
    [SerializeField] private IventoryItem _boomerang;
    [Space]
    [SerializeField] private IventoryItem _shield;
    [SerializeField] private IventoryItem _mitts;
    [SerializeField] private IventoryItem _lanter;
    [SerializeField] private IventoryItem _bomb;
    [Space]
    [SerializeField] private IventoryItem _boots;
    [SerializeField] private IventoryItem _cape;
    [SerializeField] private IventoryItem _ocarina;
    [SerializeField] private IventoryItem _bow;
    [Space] 
    [SerializeField] private InventoryData _inventory;
    [SerializeField] private MenuController _menuController;
    
    
    private void Awake()
    {
        // for (var i = 0; i < _inventory.Weapons.Count; i++)
        // {
        //     Debug.Log($"{i}: {_inventory.Weapons[i]}");
        // }
    }

    private void Start()
    {
        _sword.Item = _inventory.GetWeaponByType("Sword");
        _jar.Item = _inventory.GetWeaponByType("GustJar");
        _staff.Item = _inventory.GetWeaponByType("CaneOfPacci");
        _boomerang.Item = _inventory.GetWeaponByType("Boomerang");
        
        _shield.Item = _inventory.GetWeaponByType("Shield");
        _mitts.Item = _inventory.GetWeaponByType("MoleMitts");
        _lanter.Item = _inventory.GetWeaponByType("FlameLantern");
        _bomb.Item = _inventory.GetWeaponByType("Bomb");
        
        _boots.Item = _inventory.GetWeaponByType("PegasusBoots");
        _cape.Item = _inventory.GetWeaponByType("RocsCape");
        _ocarina.Item = _inventory.GetWeaponByType("OcarinaOfWind");
        _bow.Item = _inventory.GetWeaponByType("Bow");
    }

    private void OnEnable()
    {
        // _uiContoller.SetFirstButtonHint();
    }
}
