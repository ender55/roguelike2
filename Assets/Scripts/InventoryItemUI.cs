using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItemUI : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private Image imageIcon;
    
    private CanvasGroup _canvasGroup;
    private Canvas _canvas;
    private RectTransform _rectTransform;
    
    public InventoryItem Item { get; private set; }
    public event Action OnItemDropped;

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvas = GetComponentInParent<Canvas>();
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        var slotTransform = _rectTransform.parent;
        var inventoryTransform = slotTransform.parent;
        slotTransform.SetAsLastSibling();
        inventoryTransform.SetAsLastSibling();
        _canvasGroup.blocksRaycasts = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.localPosition = Vector3.zero;
        _canvasGroup.blocksRaycasts = true;
    }
    
    public void Refresh(InventorySlot slot)
    {
        if (slot.IsEmpty)
        {
            imageIcon.gameObject.SetActive(false);
            return;
        }

        Item = slot.Item;
        imageIcon.sprite = Item.ItemInfo.ItemSprite;
        imageIcon.gameObject.SetActive(true);
    }

    public void DropItem()
    {
        OnItemDropped?.Invoke();
    }
}
