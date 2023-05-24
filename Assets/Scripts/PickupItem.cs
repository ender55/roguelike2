using System.Collections;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    [SerializeField] private IInventoryItem item;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private CircleCollider2D collider2D;

    public IInventoryItem Item
    {
        get => item;
    }

    public void SetItem(IInventoryItem item)
    {
        this.item = item;
        spriteRenderer.sprite = this.item.ItemInfo.ItemSprite;
    }

    private IEnumerator Start()
    {
        spriteRenderer.sprite = item.ItemInfo.ItemSprite;
        collider2D.enabled = false;
        yield return new WaitForSeconds(2f);
        collider2D.enabled = true;
    }
}
