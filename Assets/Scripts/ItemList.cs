using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "itemList", menuName = "SO/Item List")]
public class ItemList : ScriptableObject
{
    [SerializeField] private List<Upgrade> upgrades;
    [SerializeField] private List<InventoryWeaponItem> weapons;

    public Upgrade GetRandomUpgrade()
    {
        if (upgrades.Count > 0)
        {
            return upgrades[UnityEngine.Random.Range(0, upgrades.Count)];
        }
        return null;
    }

    public InventoryWeaponItem GetRandomWeapon()
    {
        if (weapons.Count > 0)
        {
            return weapons[UnityEngine.Random.Range(0, weapons.Count)];
        }
        return null;
    }
}