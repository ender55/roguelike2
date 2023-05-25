using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private PickupItem pickupItemPrefab;
    [SerializeField] private Transform transform;
    [SerializeField] private List<Upgrade> upgradesList;

    private void Start()
    {
        SpawnItem(upgradesList[0], new Vector2(0, -3));
    }

    public void SpawnItem(InventoryItem item, Vector2 position)
    {
        var tempItem = Instantiate(pickupItemPrefab, position, quaternion.identity);
        tempItem.SetItem(item);
    }

    public void SpawnItem(InventoryItem item)
    {
        var tempItem = Instantiate(pickupItemPrefab, transform.position, quaternion.identity);
        tempItem.SetItem(item);
    }
}
