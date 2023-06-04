using UnityEngine;

public abstract class InventoryWeaponItem : InventoryItem, IUpgradeCollector
{
    [SerializeField] protected GameObject weaponPrefab; //todo: add check for weapon component
    protected UpgradeInventory upgradeInventory;
    
    public GameObject WeaponPrefab => weaponPrefab;
    public UpgradeInventory UpgradeInventory => upgradeInventory;
}