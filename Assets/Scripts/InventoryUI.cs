using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] protected List<InventorySlotUI> slotsUI;
    [SerializeField] private ItemSpawner itemSpawner;
    
    public Inventory Inventory { get; protected set; }
    public ItemSpawner ItemSpawner => itemSpawner;

    protected virtual void Start() //todo: add check is inventory null
    {
        var slots = Inventory.GetInventorySlots();
        for (int i = 0; i < slotsUI.Count; i++)
        {
            if (slotsUI[i] != null)
            {
                if (i < Inventory.Capacity)
                {
                    slotsUI[i].gameObject.SetActive(true);
                    slotsUI[i].SetSlot(slots[i]);
                }
                else
                {
                    slotsUI[i].gameObject.SetActive(false);
                }
            }
        }
    }

    public List<InventorySlotUI> GetAllSlotsUI()
    {
        return slotsUI;
    }
}