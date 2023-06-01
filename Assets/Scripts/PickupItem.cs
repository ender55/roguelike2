using System.Collections;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private new CircleCollider2D collider;

    public InventoryItem Item
    {
        get;
        private set;
    }

    public void SetItem(InventoryItem item)
    {
        Item = item;
        spriteRenderer.sprite = Item.ItemInfo.ItemSprite;
    }

    private IEnumerator Start()
    {
        spriteRenderer.sprite = Item.ItemInfo.ItemSprite;
        collider.enabled = false;
        yield return new WaitForSeconds(2f);
        collider.enabled = true;
    }
}
