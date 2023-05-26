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
    [SerializeField] private InventoryData _inventory;
    
    
    private void Awake()
    {
        for (var i = 0; i < _inventory.Weapons.Count; i++)
        {
            Debug.Log($"{i}: {_inventory.Weapons[i]}");
        }
    }

    private void Start()
    {
        _sword.Weapon = _inventory.GetWeaponByType("Sword");
        _jar.Weapon = _inventory.GetWeaponByType("GustJar");
        _staff.Weapon = _inventory.GetWeaponByType("CaneOfPacci");
        _boomerang.Weapon = _inventory.GetWeaponByType("Boomerang");
        
        _shield.Weapon = _inventory.GetWeaponByType("Shield");
        _mitts.Weapon = _inventory.GetWeaponByType("MoleMitts");
        _lanter.Weapon = _inventory.GetWeaponByType("FlameLantern");
        _bomb.Weapon = _inventory.GetWeaponByType("Bomb");
        
        _boots.Weapon = _inventory.GetWeaponByType("PegasusBoots");
        _cape.Weapon = _inventory.GetWeaponByType("RocsCape");
        _ocarina.Weapon = _inventory.GetWeaponByType("OcarinaOfWind");
        _bow.Weapon = _inventory.GetWeaponByType("Bow");
    }

    public void HidePanel()
    {
        this.gameObject.SetActive(false);
    }

    public void OpenPanel(GameObject panel)
    {
        
    }
}
