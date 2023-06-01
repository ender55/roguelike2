using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private PickupItem pickupItemPrefab;
    [SerializeField] private new Transform transform;
    [SerializeField] private List<Upgrade> upgradesList;
    [SerializeField] private List<InventoryWeaponItem> weaponsList;

    private void Start()
    {
        SpawnItem(weaponsList[0], new Vector2(0, -3));
        SpawnItem(weaponsList[1], new Vector2(-3, -3));
        SpawnItem(upgradesList[1], new Vector2(0, +3));
    }

    public void SpawnItem(InventoryItem item, Vector2 positionOffset)
    {
        var tempItem = Instantiate(pickupItemPrefab, (Vector2)transform.position + positionOffset, quaternion.identity);
        item.Awake();
        tempItem.SetItem(item);
    }

    public void SpawnItem(InventoryItem item)
    {
        var tempItem = Instantiate(pickupItemPrefab, transform.position, quaternion.identity);
        tempItem.SetItem(item);
    }
}
