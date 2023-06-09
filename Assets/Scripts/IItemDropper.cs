public interface IItemDropper
{
    public ItemSpawner ItemSpawner { get; }

    public void DropItem(InventoryItem item);
}
