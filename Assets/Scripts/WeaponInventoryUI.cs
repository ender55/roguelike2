using UnityEngine;

class WeaponInventoryUI : InventoryUI
{
    [SerializeField] private Player player;

    protected override void Start()
    {
        Inventory = player.WeaponInventory;
        base.Start();
    }
}