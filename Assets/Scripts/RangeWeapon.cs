using UnityEngine;

public class RangeWeapon : Weapon
{
    [SerializeField] private RangeWeaponData rangeWeaponData;
    [SerializeField] private Projectile projectile;
    [SerializeField] private Transform attackPointTransform;

    public RangeWeaponData RangeWeaponStats => rangeWeaponData;
    public Projectile Projectile => projectile;
    public Transform AttackPointTransform => attackPointTransform;

    public override void Attack()
    {
        base.Attack();
        var projectileInstance = Instantiate(projectile, attackPointTransform.position, gameObject.transform.rotation);
        projectileInstance.OnHit += ApplyDamage;
    }

    public override void AlternativeAttack()
    {
        base.AlternativeAttack();
    }

    public override void AddUpgrade(Upgrade upgrade)
    {
        if (upgrade is RangeWeaponUpgrade weaponUpgrade)
        {
            _upgrades.Add(weaponUpgrade);
            weaponUpgrade.Equip(this);
        }
        base.AddUpgrade(upgrade);
    }
}
