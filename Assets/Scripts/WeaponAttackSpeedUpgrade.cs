using AYellowpaper.SerializedCollections;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Item/Upgrade/Weapon Upgrade/Attack Speed Upgrade", fileName = "AttackSpeedUpgrade")]
class WeaponAttackSpeedUpgrade : GeneralWeaponUpgrade
{
    [SerializeField] private SerializedDictionary<UpgradeRarity, float> attackSpeedModifierByRarity;
    [SerializeField] private SerializedDictionary<int, float> attackSpeedModifierByLevel;

    public SerializedDictionary<UpgradeRarity, float> DamageModifierByRarity => attackSpeedModifierByRarity;
    public SerializedDictionary<int, float> DamageModifierByLevel => attackSpeedModifierByLevel;
    
    public override void Activate()
    {
        currentWeapon.WeaponModifiers.AttackSpeedModifier += attackSpeedModifierByRarity[upgradeRarity] + attackSpeedModifierByLevel[currentLevel];
    }

    public override void Deactivate()
    {
        currentWeapon.WeaponModifiers.AttackSpeedModifier -= attackSpeedModifierByRarity[upgradeRarity] + attackSpeedModifierByLevel[currentLevel];
    }
}