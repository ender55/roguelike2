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
            return BaseTryAddItem(item);
        }

        return false;
    }

    protected bool BaseTryAddItem(InventoryItem item)
    {
        return base.TryAddItem(item);
    }
}