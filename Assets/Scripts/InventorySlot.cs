using System;
using UnityEngine;

[Serializable]
public class InventorySlot
{
     [SerializeField] private Item item;

     public Item Item => item;

     public event Action OnSlotChanged;

     public bool IsEmpty => item == null;

     public void SetItem(Item item)
     {
          this.item = item;
          OnSlotChanged?.Invoke();
     }

     public void Clear()
     {
          if (!IsEmpty)
          {
               item = null; 
               OnSlotChanged?.Invoke();
          }
     }
}
