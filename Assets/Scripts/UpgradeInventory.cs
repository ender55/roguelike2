using System;

[Serializable]
public class UpgradeInventory : Inventory
{
    public UpgradeInventory(int capacity) : base(capacity)
    {
    }
    
    public override bool TryAddItem(InventoryItem item)
    {
        if (item is Upgrade)
        {
            return base.TryAddItem(item);
        }

        return false;
    }
}