using UnityEngine;

[CreateAssetMenu(fileName = "RangeWeapon", menuName = "SO/Item/Weapon/Range Weapon")]
public class InventoryRangeWeaponItem : InventoryWeaponItem
{
    public override void Awake()
    {
        upgradeInventory = new RangeWeaponUpgradeInventory(weaponPrefab.UpgradesAmount);
    }
}