using TNRD;
using UnityEngine;

public class UpgradeInventoryUI : InventoryUI //todo: make PlayerUpgradeInventoryUI
{
    [SerializeField] private SerializableInterface<IUpgradeCollector> upgradeCollector; //todo: inject

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