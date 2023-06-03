using TNRD;
using UnityEngine;

class WeaponInventoryUI : InventoryUI //todo: inventory doesnt update at first time
{
    [SerializeField] private SerializableInterface<IWeaponCollector> weaponCollector;

    protected override void Start()
    {
        Inventory = weaponCollector.Value.WeaponInventory;
        base.Start();
    }
}