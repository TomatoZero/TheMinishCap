using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryData", menuName = "Items/Inventory")]
public class InventoryData : ScriptableObject
{
    private List<WeaponData>_weapons;
    private List<InventoryData> _bottles;
    private List<InventoryData> _questItem;
    private List<KinstonePieceData> _kinstonePieces;
    private List<TigerScrollData> _tigerScrolls;

    public List<WeaponData> Weapons => _weapons;

    public void Clear()
    {
        _weapons = new List<WeaponData>();
    }
    
    public void AddWeapon(WeaponData weaponData)
    {
        if (_weapons.Count >= 12) return;
        
        var weaponType = weaponData.ToString().Trim().Split('_');
        for (var i = 0; i < _weapons.Count(); i++)
        {
            if(_weapons[i].ToString() == weaponData.ToString())
            {
                return;
            }
            
            if (_weapons[i].ToString().Trim().Split('_', ' ')[0] == weaponType[0])
            {
                if (_weapons[i].Lvl < weaponData.Lvl)
                {
                    _weapons[i] = weaponData;
                    return;
                }
                else
                {
                    return;
                }
            }
        }
        
        _weapons.Add(weaponData);
    }

    public WeaponData GetWeaponByType(string type)
    {
        for (var i = 0; i < _weapons.Count(); i++)
        {
            // Debug.Log(_weapons[i].ToString().Trim().Split('_', ' '));
            if (_weapons[i].ToString().Trim().Split('_', ' ')[0] == type)
            {
                return _weapons[i];
            }
        }

        return null;
    }
}
