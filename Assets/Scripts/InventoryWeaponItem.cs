using UnityEngine;

public abstract class InventoryWeaponItem : InventoryItem, IUpgradeCollector
{
    [SerializeField] protected Weapon weaponPrefab;
    protected UpgradeInventory upgradeInventory;
    
    public Weapon WeaponPrefab => weaponPrefab;
    public UpgradeInventory UpgradeInventory => upgradeInventory;
}