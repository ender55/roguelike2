using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] protected List<InventorySlotUI> slotsUI;
    public Inventory Inventory { get; protected set; }

    protected virtual void Awake()
    {
        for (int i = 0; i < Inventory.Capacity; i++)
        {
            if (slotsUI[i] != null)
            {
                slotsUI[i].SetSlot(Inventory.Slots[i]);
            }
        }
    }
}