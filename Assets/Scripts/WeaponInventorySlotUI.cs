using UnityEngine;

class WeaponInventorySlotUI : InventorySlotUI
{
    [SerializeField] private UpgradeInventoryUI upgradeInventoryUI;

    protected override void OnEnable()
    {
        if (Slot != null)
        {
            SetCollector();
            Slot.OnSlotChanged += SetCollector;
        }
        base.OnEnable();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        if (Slot != null)
        {
            Slot.OnSlotChanged -= SetCollector;
        }
    }

    public override void SetSlot(InventorySlot slot)
    {
        base.SetSlot(slot);
        Slot.OnSlotChanged += SetCollector;
    }

    private void SetCollector()
    {
        if (upgradeInventoryUI != null)
        {
            //Debug.Log();
            if (Slot.Item == null)
            {
                upgradeInventoryUI.SetCollector(null);
                foreach (var slot in upgradeInventoryUI.GetAllSlotsUI())
                {
                    slot.gameObject.SetActive(false);
                }
            }
            else
            {
                if (Slot.Item is InventoryWeaponItem item)
                {
                    upgradeInventoryUI.SetCollector(item);
                    foreach (var slot in upgradeInventoryUI.GetAllSlotsUI())
                    {
                        if (slot.enabled)
                        {
                            slot.Refresh();
                        }
                    }
                }
            }
        }
    }
}