public interface IItemDropper
{
    public ItemSpawner ItemSpawner { get; }

    public float DropChance { get; }

    public void DropItem();
}