using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryBackgroundUI : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        var otherItemUI = eventData.pointerDrag.GetComponent<InventoryItemUI>();
        otherItemUI.DropItem();
    }
}
