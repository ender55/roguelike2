using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "SO/Item/Weapon")]
public class InventoryWeapon : InventoryItem
{
    [SerializeField] private GameObject weaponPrefab;

    public GameObject Weapon => weaponPrefab;
}
