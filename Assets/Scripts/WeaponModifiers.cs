using System;
using AYellowpaper.SerializedCollections;
using UnityEngine;

[Serializable]
public abstract class WeaponModifiers
{
    //general
    [SerializeField] private int baseDamage;
    [SerializeField] private DamageType baseDamageType;
    //[SerializeField] private SerializedDictionary<DamageType, float> damageMultipliers;
    [SerializeField] private float baseAttackSpeed;
    //[SerializeField] private float attackSpeedMultiplier;
    [SerializeField] private float knockback;
    [SerializeField] private int criticalDamageChance;
    [SerializeField] private float criticalDamageMultiplier;
    //for range weapon
    [SerializeField] private int amountOfProjectiles;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float projectileSize;
    [SerializeField] private int projectilePiercePower;
    //for melee weapon
    [SerializeField] private float attackRange;
    //modifiers
    [SerializeField] private Damage bonusDamage;
    [SerializeField] private SerializedDictionary<DamageType, float> damageMultipliers;
    [SerializeField] private float attackSpeedMultiplier;
    [SerializeField] private int bonusCriticalDamageChance;
    [SerializeField] private float bonusCriticalDamageMultiplier;
}
