using System;
using UnityEngine;
using UnityEngine.Events;

public class AttackController : MonoBehaviour
{
    [SerializeField] private BaseWeapon _weapon;

    private void OnEnable()
    {
        _weapon.UseWeapon();
    }


    public delegate void AttackEventHandler(State state);
    public static event AttackEventHandler Attack;

    protected virtual void OnAttack()
    {
        Attack?.Invoke(State.MeleeAttack);
    }
}