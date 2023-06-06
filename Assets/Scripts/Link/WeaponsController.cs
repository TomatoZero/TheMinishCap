using UnityEngine;

public class WeaponsController : MonoBehaviour
{
    [SerializeField] private BaseWeapon _firstWeapon;
    // [SerializeField] private BaseWeapon _secondWeapon;
    

    public void UseFirstWeapon()
    {
        _firstWeapon.UseWeapon();
    }
    
    public void ReleaseFirstWeapon()
    {
        _firstWeapon.ReleaseWeapon();
    }

    public void ChangeSortingLayerInWeapon(string layer)
    {
        _firstWeapon.ChangeSortingLayer(layer);
    }
    
}
