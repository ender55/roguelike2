using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlotUI : MonoBehaviour, IDropHandler
{
    [SerializeField] private InventoryItemUI itemUI;

    private InventoryUI _inventoryUI;
    
    public InventorySlot Slot { get; private set; } //make private if need

    private void Awake()
    {
        _inventoryUI = GetComponentInParent<InventoryUI>();
    }

    private void OnEnable()
    {
        if (Slot != null)
        {
            Slot.OnSlotChanged += Refresh;
        }
    }

    private void OnDisable()
    {
        Slot.OnSlotChanged -= Refresh;
    }

    public void SetSlot(InventorySlot slot)
    {
        Slot = slot;
        Slot.OnSlotChanged += Refresh;
    }
    
    public void OnDrop(PointerEventData eventData)
    {
        var otherItemUI = eventData.pointerDrag.GetComponent<InventoryItemUI>();
        var otherSlotUI = otherItemUI.GetComponentInParent<InventorySlotUI>();
        var otherSlot = otherSlotUI.Slot;
        var inventory = _inventoryUI.Inventory;
        
        inventory.TransitFromSlotToSlot(otherSlot, Slot);
    }

    private void Refresh()
    {
        if (Slot != null)
        {
            itemUI.Refresh(Slot);
        }
    }
}
