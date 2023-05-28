using System;

[Serializable]
public class WeaponInventory : Inventory
{
    public WeaponInventory(int capacity) : base(capacity)
    {
    }
    
    public override bool TryAddItem(InventoryItem item)
    {
        if (item is InventoryWeapon)
        {
            return base.TryAddItem(item);
        }

        return false;
    }
}