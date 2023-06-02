using TNRD;
using UnityEngine;

class UpgradeInventoryUI : InventoryUI
{
    [SerializeField] private SerializableInterface<IUpgradeCollector> upgradeCollector;

    protected override void Start()
    {
        if (upgradeCollector.Value != null)
        {
            Inventory = upgradeCollector.Value.UpgradeInventory;
            base.Start();
        }
    }

    public void SetCollector(IUpgradeCollector collector)
    {
        upgradeCollector.Value = collector;
        Start();
    }
}