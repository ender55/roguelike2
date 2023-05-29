using System;
using UnityEngine;

[Serializable]
public class InventorySlot
{
     [SerializeField] private InventoryItem item;

     public InventoryItem Item
     {
          get => item;
          private set => item = value;
     }

     public event Action OnSlotChanged;
     public event Action OnSlotCleared;

     public bool IsEmpty => Item == null;

     public void SetItem(InventoryItem item) //todo: add checking for emptiness
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
               OnSlotCleared?.Invoke();
          }
     }
}
