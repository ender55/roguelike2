﻿using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Inventory
{
    [SerializeField] private List<InventorySlot> slots;

    public List<InventorySlot> Slots => slots; //todo: encapsulate it using method GetAllSlots()

    public int Capacity { get; set; }

    public Inventory(int capacity)
    {
        Capacity = capacity;
        slots = new List<InventorySlot>(capacity);
        for (int i = 0; i < capacity; i++)
        {
            slots.Add(new InventorySlot());
        }
    }

    public virtual bool TryAddItem(Item item)
    {
        foreach (var slot in slots)
        {
            if (slot.IsEmpty)
            {
                slot.SetItem(item);
                return true;
            }
        }

        return false;
    }

    public void TransitFromSlotToSlot(InventorySlot fromSlot, InventorySlot toSlot) //todo: check if we try to put an item to the same slot
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