﻿using AYellowpaper.SerializedCollections;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Item/Upgrade/Weapon Upgrade/Attack Upgrade", fileName = "AttackUpgrade")]
public class WeaponAttackUpgrade : WeaponUpgrade
{
    [SerializeField] private SerializedDictionary<UpgradeRarity, float> damageModifierByRarity;
    [SerializeField] private SerializedDictionary<int, float> damageModifierByLevel;

    public SerializedDictionary<UpgradeRarity, float> DamageModifierByRarity => damageModifierByRarity;
    public SerializedDictionary<int, float> DamageModifierByLevel => damageModifierByLevel;
    
    public override void Activate()
    {
        currentWeapon.WeaponStats.DamageModifier += damageModifierByRarity[upgradeRarity] + damageModifierByLevel[currentLevel];
    }

    public override void Deactivate()
    {
        currentWeapon.WeaponStats.DamageModifier -= damageModifierByRarity[upgradeRarity] + damageModifierByLevel[currentLevel];
    }
}