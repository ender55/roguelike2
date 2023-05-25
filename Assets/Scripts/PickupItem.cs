using System.Collections;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private CircleCollider2D collider2D;

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
        collider2D.enabled = false;
        yield return new WaitForSeconds(2f);
        collider2D.enabled = true;
    }
}
