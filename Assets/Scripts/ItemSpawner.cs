using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ItemSpawner : MonoBehaviour //todo: split spawner and dropper
{
    [SerializeField] private PickupItem pickupItemPrefab;
    [SerializeField] private new Transform transform; //todo: inject
    [SerializeField] private List<Upgrade> upgradesList;
    [SerializeField] private List<InventoryWeaponItem> weaponsList;

    private void Start()
    {
        SpawnItem(weaponsList[0], new Vector2(0, -3));
        SpawnItem(weaponsList[1], new Vector2(-3, -3));
    }

    public void SpawnItem(InventoryItem item, Vector2 position)
    {
        var tempItem = Instantiate(pickupItemPrefab, position, quaternion.identity);
        item.Awake();
        tempItem.SetItem(item);
    }

    public void SpawnItem(InventoryItem item)
    {
        var tempItem = Instantiate(pickupItemPrefab, transform.position, quaternion.identity);
        tempItem.SetItem(item);
    }

    public void SpawnRandomUpgrade(Vector2 position)
    {
        if(upgradesList.Count > 0)
        {
            var randomItem = upgradesList[UnityEngine.Random.Range(0, upgradesList.Count)];
            SpawnItem(randomItem, position);
        }
    }
}
