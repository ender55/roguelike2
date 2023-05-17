using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class Weapon : MonoBehaviour, IUpgradable
{
    [SerializeField] protected List<Upgrade> upgrades;
    [SerializeField] protected WeaponStats weaponStats;
    
    public List<Upgrade> Upgrades => upgrades;
    public WeaponStats WeaponStats => weaponStats;
    
    public abstract void Attack();
    public abstract void AlternativeAttack();

    public void AddUpgrade(WeaponUpgrade upgrade) //todo: возможно стоит поместить метод и в интерфейс IUpgradable
    {
        upgrades.Add(upgrade);
        upgrade.Equip(this);
    }

    public void RemoveUpgrade(WeaponUpgrade upgrade) //todo: возможно стоит поместить метод и в интерфейс IUpgradable
    {
        upgrade.Unequip();
        upgrades.Remove(upgrade);
    }
}