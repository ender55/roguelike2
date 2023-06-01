using UnityEngine;

public class RangeWeapon : Weapon //todo: clear code and handle with weapon stats
{
    [SerializeField] private RangeWeaponData rangeWeaponData;
    [SerializeField] private Projectile projectile;
    [SerializeField] private Transform attackPointTransform;

    public RangeWeaponData RangeWeaponStats => rangeWeaponData;

    public Projectile Projectile => projectile;

    public Transform AttackPointTransform => attackPointTransform;

    private void Awake()
    {
        //upgradeInventory = new RangeWeaponUpgradeInventory(weaponData.UpgradesAmount);
        //upgradeInventory = weaponData
    }

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

    //public void AddUpgrade(RangeWeaponUpgrade upgrade)
    //{
    //    upgrades.Add(upgrade);
    //    upgrade.Equip(this);
    //}
    
    //public void RemoveUpgrade(RangeWeaponUpgrade upgrade)
    //{
    //    upgrade.Unequip();
    //    upgrades.Remove(upgrade);
    //}
}
