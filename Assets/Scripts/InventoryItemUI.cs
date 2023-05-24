using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItemUI : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler //todo: add method for dropping item
{
    [SerializeField] private Image imageIcon; //todo: remove 
    
    private CanvasGroup _canvasGroup;
    private Canvas _canvas;
    private RectTransform _rectTransform;

    public IInventoryItem Item { get; private set; }

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
        slotTransform.SetAsLastSibling();
        _canvasGroup.blocksRaycasts = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.localPosition = Vector3.zero;
        _canvasGroup.blocksRaycasts = true;
    }
    
    public void Refresh(InventorySlot slot) //todo: check method for correctness
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
}
