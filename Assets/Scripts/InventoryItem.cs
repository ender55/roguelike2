using UnityEngine;

public class InventoryItem: ScriptableObject
{
    [SerializeField] protected InventoryItemInfo itemInfo;
    
    public IInventoryItemInfo ItemInfo => itemInfo;

    public virtual void Awake()
    {
    }
}
