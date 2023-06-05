using System;
using UnityEngine;

[Serializable]
public class Damage
{
    [SerializeField] private DamageType damageType;
    [SerializeField] private int damageValue;

    public Damage(DamageType damageType, int damageValue)
    {
        this.damageType = damageType;
        this.damageValue = damageValue;
    }
    
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
}
