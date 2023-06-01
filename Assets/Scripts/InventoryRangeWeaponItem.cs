using UnityEngine;

[CreateAssetMenu(fileName = "RangeWeapon", menuName = "SO/Item/Weapon/Range Weapon")]
public class InventoryRangeWeaponItem : InventoryWeaponItem
{
    public override void Awake()
    {
        if (weaponPrefab.TryGetComponent(out Weapon weapon))
        {
            upgradeInventory = new RangeWeaponUpgradeInventory(weapon.WeaponData.UpgradesAmount);
        }
    }
}