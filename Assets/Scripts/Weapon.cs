using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected List<Upgrade> upgrades;
    [SerializeField] protected WeaponStats weaponStats;
    
    public List<Upgrade> Upgrades => upgrades;
    public WeaponStats WeaponStats => weaponStats;
    
    public event Action OnAttack;
    public event Action OnAlternativeAttack;
    public event Action<IDamageable> OnHit;

    public virtual void Attack()
    {
        OnAttack?.Invoke();
    }

    public virtual void AlternativeAttack()
    {
        OnAlternativeAttack?.Invoke();
    }

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
    
    protected void ApplyDamage(IDamageable damageable)
    {
        Damage tempDamage = weaponStats.BaseDamage;
        tempDamage.DamageValue = (int)(tempDamage.DamageValue * weaponStats.DamageModifier);
        damageable.TakeDamage(tempDamage);
        OnHit?.Invoke(damageable);
    }
}