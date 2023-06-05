using UnityEngine;

public class WeaponsController : MonoBehaviour
{
    [SerializeField] private BaseWeapon _firstWeapon;
    // [SerializeField] private BaseWeapon _secondWeapon;
    
    public delegate void UseWeaponEventHandler(State state);
    public delegate void ReleaseWeaponEventHandler();

    public static event UseWeaponEventHandler UseWeapon;
    public static event ReleaseWeaponEventHandler ReleaseWeapon;
    

    public void UseFirstWeapon()
    {
        _firstWeapon.UseWeapon();
        UseWeapon?.Invoke(State.MeleeAttack);
    }
    
    public void ReleaseFirstWeapon()
    {
        _firstWeapon.ReleaseWeapon();
        ReleaseWeapon?.Invoke();
    }

    public void ChangeSortingLayerInWeapon(string layer)
    {
        _firstWeapon.ChangeSortingLayer(layer);
    }
    
}
