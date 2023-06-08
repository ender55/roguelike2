using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlotUI : MonoBehaviour, IDropHandler
{
    [SerializeField] private InventoryItemUI itemUI;

    private InventoryUI _inventoryUI;

    protected InventorySlot Slot { get; private set; }

    private void Awake()
    {
        _inventoryUI = GetComponentInParent<InventoryUI>();
    }

    protected virtual void OnEnable()
    {
        Refresh();
        if (Slot != null)
        {
            Slot.OnSlotChanged += Refresh;
        }

        itemUI.OnItemDropped += DropItem;
    }

    protected virtual void OnDisable()
    {
        if (Slot != null)
        {
            Slot.OnSlotChanged -= Refresh;
        }
        
        itemUI.OnItemDropped -= DropItem;
    }

    public virtual void SetSlot(InventorySlot slot)
    {
        Slot = slot;
        Slot.OnSlotChanged += Refresh;
        Refresh();
    }
    
    public void OnDrop(PointerEventData eventData)
    {
        var otherItemUI = eventData.pointerDrag.GetComponent<InventoryItemUI>();
        var otherSlotUI = otherItemUI.GetComponentInParent<InventorySlotUI>();
        var otherSlot = otherSlotUI.Slot;
        var inventory = _inventoryUI.Inventory;
        var type1 = gameObject.transform.parent.GetComponent(typeof(InventoryUI)).GetType();
        var type2 = otherSlotUI.transform.parent.GetComponent(typeof(InventoryUI)).GetType();
        if (type1 == type2)
        {
            inventory.TransitFromSlotToSlot(otherSlot, Slot);
        }
    }

    public void Refresh()
    {
        if (Slot != null)
        {
            itemUI.Refresh(Slot);
        }
    }

    private void DropItem()
    {
        _inventoryUI.ItemSpawner.SpawnItem(itemUI.Item);
        Slot.Clear();
    }
}