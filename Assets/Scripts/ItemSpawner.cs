using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemSpawner", menuName = "SO/Item Spawner")]
public class ItemSpawner : ScriptableObject
{
    [SerializeField] private PickupItem pickupItemPrefab;

    private Transform objectTransform;

    public void SpawnItem(InventoryItem item, Vector2 position)
    {
        var tempItem = Instantiate(pickupItemPrefab, position, quaternion.identity);
        tempItem.SetItem(item);
    }

    public void InitialSpawnItem(InventoryItem item, Vector2 position)
    {
        var tempItem = Instantiate(pickupItemPrefab, position, quaternion.identity);
        tempItem.SetItem(item);
        tempItem.Item.Awake();
    }
}
