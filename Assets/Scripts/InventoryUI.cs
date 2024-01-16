using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] protected List<InventorySlotUI> slotsUI;
    [SerializeField] private Player player;

    public Inventory Inventory { get; protected set; }
    public Player Player => player;

    protected virtual void Start()
    {
        var slots = Inventory.GetInventorySlots();
        for (int i = 0; i < slotsUI.Count; i++)
        {
            if (slotsUI[i] != null)
            {
                if (i < Inventory.Capacity)
                {
                    slotsUI[i].SetSlot(slots[i]);
                    slotsUI[i].gameObject.SetActive(true);
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