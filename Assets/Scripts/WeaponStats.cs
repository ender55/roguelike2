using System;
using UnityEngine;

[Serializable]
public abstract class WeaponStats
{
    //stats
    [SerializeField] private Damage baseDamage;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float energyConsumption;
    [SerializeField] private float knockback;
    [SerializeField] private int criticalDamageChance;
    [SerializeField] private float criticalDamageMultiplier;
    [SerializeField] private float attackRange;
    //modifiers
    [SerializeField] private float damageModifier;
    [SerializeField] private float attackSpeedModifier;
    [SerializeField] private float energyConsumptionModifier;

    public Damage BaseDamage => baseDamage;
    public float AttackSpeed => attackSpeed;
    public float EnergyConsumption => energyConsumption;
    public float Knockback => knockback;
    public int CriticalDamageChance => criticalDamageChance;
    public float CriticalDamageMultiplier => criticalDamageMultiplier;
    public float AttackRange => attackRange;
    public float DamageModifier => damageModifier;
    public float AttackSpeedModifier => attackSpeedModifier;
    public float EnergyConsumptionModifier => energyConsumptionModifier;
}

[Serializable]
public class MeleeWeaponStats : WeaponStats
{
}

[Serializable]
public class RangeWeaponStats : WeaponStats
{
    [SerializeField] private int amountOfProjectiles;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float projectileSize;
    [SerializeField] private int projectilePiercePower;
    [SerializeField] private float spread;

    [SerializeField] private float projectileSpeedModifier;
    [SerializeField] private float spreadModifier;

    public int AmountOfProjectiles => amountOfProjectiles;
    public float ProjectileSpeed => projectileSpeed;
    public float ProjectileSize => projectileSize;
    public int ProjectilePiercePower => projectilePiercePower;
    public float Spread => spread;
    public float ProjectileSpeedModifier => projectileSpeedModifier;
    public float SpreadModifier => spreadModifier;
}