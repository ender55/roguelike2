using System;
using UnityEngine;

[Serializable]
public class WeaponModifiers
{
    [SerializeField] private float damageModifier = 1;
    [SerializeField] private float attackSpeedModifier = 1;
    
    public float DamageModifier
    {
        get => damageModifier;
        set => damageModifier = value;
    }

    public float AttackSpeedModifier
    {
        get => attackSpeedModifier;
        set => attackSpeedModifier = value;
    }
}
