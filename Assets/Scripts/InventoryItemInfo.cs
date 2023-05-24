using UnityEngine;

[CreateAssetMenu(fileName = "InventoryItemInfo", menuName = "SO/Inventory Item Info")]
class InventoryItemInfo : ScriptableObject, IInventoryItemInfo
{
    [SerializeField] private Sprite itemSprite;

    public Sprite ItemSprite => itemSprite;
}