using System;
using UnityEngine;

public class RangeWeapon : Weapon
{
    [SerializeField] private Projectile projectile;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private RangeWeaponStats rangeWeaponStats;

    public RangeWeaponStats RangeWeaponStats => rangeWeaponStats;

    public event Action OnAttack;
    public event Action OnAlternativeAttack;
    public event Action<IDamageable> OnHit; 

    public override void Attack()
    {
        OnAttack?.Invoke();
        var projectileInstance = Instantiate(projectile, attackPoint.position, gameObject.transform.rotation);
        projectileInstance.OnHit += ApplyDamage;
    }

    public override void AlternativeAttack()
    {
        OnAlternativeAttack?.Invoke();
    }

    private void ApplyDamage(IDamageable damageable)
    {
        Debug.Log("damaged");
        damageable.TakeDamage(weaponStats.BaseDamage);
        OnHit?.Invoke(damageable);
    }
    
    public void AddUpgrade(RangeWeaponUpgrade upgrade)
    {
        upgrades.Add(upgrade);
        upgrade.Equip(this);
    }
    
    public void RemoveUpgrade(RangeWeaponUpgrade upgrade)
    {
        upgrade.Unequip();
        upgrades.Remove(upgrade);
    }
}
