using System;
using UnityEngine;

public class RangeWeapon : Weapon
{
    [SerializeField] private Projectile projectile;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private RangeWeaponStats rangeWeaponStats;

    public RangeWeaponStats RangeWeaponStats => rangeWeaponStats;

    public override void Attack()
    {
        base.Attack();
        var projectileInstance = Instantiate(projectile, attackPoint.position, gameObject.transform.rotation);
        projectileInstance.OnHit += ApplyDamage;
    }

    public override void AlternativeAttack()
    {
        base.AlternativeAttack();
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
