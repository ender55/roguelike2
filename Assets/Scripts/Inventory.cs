using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Inventory //todo: add interface
{
    [SerializeField] protected List<InventorySlot> slots;

    public event Action OnInventoryChange;
    public int Capacity { get; private set; }

    public Inventory(int capacity)
    {
        Capacity = capacity;
        slots = new List<InventorySlot>(capacity);
        for (int i = 0; i < capacity; i++)
        {
            slots.Add(new InventorySlot());
            slots[i].OnSlotChanged += () => OnInventoryChange?.Invoke();
        }
    }

    public virtual bool TryAddItem(InventoryItem item)
    {
        foreach (var slot in slots)
        {
            if (slot.IsEmpty)
            {
                slot.SetItem(item);
                OnInventoryChange?.Invoke();
                return true;
            }
        }

        return false;
    }

    public List<InventorySlot> GetInventorySlots()
    {
        return slots;
    }

    public void TransitFromSlotToSlot(InventorySlot fromSlot, InventorySlot toSlot)
    {
        if (fromSlot.IsEmpty)
        {
            return;
        }

        if (!toSlot.IsEmpty)
        {
            var temp = toSlot.Item;
            toSlot.SetItem(fromSlot.Item);
            fromSlot.SetItem(temp);
        }
        else
        {
            toSlot.SetItem(fromSlot.Item);
            fromSlot.Clear();
        }
    }
}