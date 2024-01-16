class RangeWeaponUpgradeInventory : UpgradeInventory
{
    public RangeWeaponUpgradeInventory(int capacity) : base(capacity)
    {
    }
    
    public override bool TryAddItem(InventoryItem item)
    {
        if (item is RangeWeaponUpgrade || item is GeneralWeaponUpgrade)
        {
            return BaseTryAddItem(item);
        }

        return false;
    }
}