using UnityEngine;

public class WeaponsController : MonoBehaviour
{
    [SerializeField] private BaseWeapon _firstWeapon;
    [SerializeField] private BaseWeapon _secondWeapon;

    public void UseFirstWeapon()
    {
        _firstWeapon.UseWeapon();
        UseWeapon?.Invoke(State.MeleeAttack);
    }

    public void HoldFirstWeapon()
    {
    }

    public void ReleaseFirstWeapon()
    {
        _firstWeapon.ReleaseWeapon();
        ReleaseWeapon?.Invoke();
    }
    
    public delegate void UseWeaponEventHandler(State state);
    public delegate void ReleaseWeaponEventHandler();

    public static event UseWeaponEventHandler UseWeapon;
    public static event ReleaseWeaponEventHandler ReleaseWeapon;
    
    // public delegate void HoldWeaponEventHandler();
    // public static event HoldWeaponEventHandler HoldWeapon;
}
