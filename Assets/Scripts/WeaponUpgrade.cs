using AYellowpaper.SerializedCollections;
using UnityEngine;

public abstract class WeaponUpgrade : Upgrade
{
    protected Weapon currentWeapon;

    public Weapon CurrentWeapon => currentWeapon;

    public void Equip(Weapon weapon)
    {
        currentWeapon = weapon;
        Activate();
    }

    public void Unequip()
    {
        Deactivate();
        currentWeapon = null;
    }
}

public class WeaponAttackUpgrade : WeaponUpgrade //todo: test and move to another file
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