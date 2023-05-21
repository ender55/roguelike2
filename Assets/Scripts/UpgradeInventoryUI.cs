using UnityEngine;

class UpgradeInventoryUI : InventoryUI
{
    [SerializeField] private Player player;

    protected override void Awake()
    {
        Inventory = player.UpgradeInventory;
        base.Awake();
    }
}