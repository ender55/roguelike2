using TNRD;
using UnityEngine;

class WeaponInventoryUI : InventoryUI
{
    [SerializeField] private SerializableInterface<IWeaponCollector> weaponCollector;

    protected override void Start()
    {
        Inventory = weaponCollector.Value.WeaponInventory;
        base.Start();
    }
}