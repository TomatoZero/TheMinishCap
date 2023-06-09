using UnityEngine;

public class WeaponsController : MonoBehaviour
{
    [SerializeField] private BaseWeapon _firstWeapon;
    
    public void UseFirstWeaponEventHandler()
    {
        _firstWeapon.UseWeapon();
    }
    
    public void ReleaseFirstWeaponEventHandler()
    {
        _firstWeapon.ReleaseWeapon();
    }

    public void ChangeSortingLayerInWeapon(string layer)
    {
        _firstWeapon.ChangeSortingLayer(layer);
    }
    
}
