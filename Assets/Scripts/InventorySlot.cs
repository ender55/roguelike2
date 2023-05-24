using System;
using UnityEngine;

[Serializable]
public class InventorySlot
{
     public IInventoryItem Item { get; private set; }

     public event Action OnSlotChanged;

     public bool IsEmpty => Item == null;

     public void SetItem(IInventoryItem item) //todo: add checking for emptiness
     {
          Item = item;
          OnSlotChanged?.Invoke();
     }

     public void Clear()
     {
          if (!IsEmpty)
          {
               Item = null; 
               OnSlotChanged?.Invoke();
          }
     }
}
