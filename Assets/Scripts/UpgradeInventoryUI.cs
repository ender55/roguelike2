using TNRD;
using UnityEngine;

class UpgradeInventoryUI : InventoryUI
{
    [SerializeField] private SerializableInterface<IUpgradeCollector> upgradeCollector;

    protected override void Start()
    {
        Inventory = upgradeCollector.Value.UpgradeInventory;
        base.Start();
    }
}