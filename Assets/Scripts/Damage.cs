using System;
using AYellowpaper.SerializedCollections;
using UnityEngine;

[Serializable]
public class Damage
{
    [SerializeField] private DamageType damageType;
    [SerializeField] private int damageValue;

    public DamageType DamageType
    {
        get => damageType;
        set => damageType = value;
    }

    public int DamageValue
    {
        get => damageValue;
        set => damageValue = value;
    }

    public void ApplyDamage(IDamageable damageable)
    {

    }
}
