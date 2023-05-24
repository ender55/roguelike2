using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour //todo: add list of all items
{
    [SerializeField] protected List<InventorySlotUI> slotsUI;
    public Inventory Inventory { get; protected set; }

    protected virtual void Awake()
    {
        var slots = Inventory.GetInventorySlots();
        for (int i = 0; i < Inventory.Capacity; i++)
        {
            if (slotsUI[i] != null)
            {
                slotsUI[i].SetSlot(slots[i]);
            }
        }
    }
}