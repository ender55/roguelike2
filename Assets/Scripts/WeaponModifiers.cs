using System;
using UnityEngine;

[Serializable]
public class WeaponModifiers
{
    [SerializeField] private float damageModifier = 1;
    [SerializeField] private float attackSpeedMultiplier = 1;
    
    public float DamageModifier
    {
        get => damageModifier;
        set => damageModifier = value;
    }

    public float AttackSpeedMultiplier
    {
        get => attackSpeedMultiplier;
        set => attackSpeedMultiplier = value;
    }
}
