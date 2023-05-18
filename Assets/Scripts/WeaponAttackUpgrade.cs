﻿using AYellowpaper.SerializedCollections;
using UnityEngine;

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

    protected override void OnTriggerEnter2D(Collider2D col) //todo: remove when upgrades will be tested
    {
        if (col.TryGetComponent(out Player player))
        {
            player.Weapon.AddUpgrade(this);
        }
    }
}