using UnityEngine;

public class RangeWeapon : Weapon
{
    [SerializeField] private RangeWeaponData rangeWeaponData;
    [SerializeField] private RangeWeaponModifiers rangeWeaponModifiers;
    [SerializeField] private Projectile projectile;
    [SerializeField] private Transform attackPointTransform;

    public RangeWeaponData RangeWeaponStats => rangeWeaponData;
    public Projectile Projectile => projectile;
    public Transform AttackPointTransform => attackPointTransform;

    public RangeWeaponModifiers RangeWeaponModifiers => rangeWeaponModifiers;

    protected override void Attack()
    {
        base.Attack();
        for (int i = 0; i < rangeWeaponData.ProjectilesPerShot; i++)
        {
            var projectileInstance =
                Instantiate(projectile, attackPointTransform.position, gameObject.transform.rotation);
            var spread = Random.Range(-rangeWeaponData.Spread, rangeWeaponData.Spread);
            projectileInstance.Direction.RotateByAngle(spread * (1 / rangeWeaponModifiers.SpreadModifier));
            projectileInstance.transform.rotation =
                Quaternion.FromToRotation(Vector2.right, projectileInstance.Direction.Value);
            projectileInstance.transform.localScale *= rangeWeaponModifiers.ProjectileSizeModifier;
            projectileInstance.Movement.MoveSpeed = rangeWeaponData.ProjectileSpeed;
            projectileInstance.OnHit += ApplyDamage; //todo: check for memory leak if weapon is destroyed
        }
    }

    public override void AlternativeAttack()
    {
        base.AlternativeAttack();
    }

    public override void AddUpgrade(Upgrade upgrade)
    {
        if (upgrade is RangeWeaponUpgrade weaponUpgrade)
        {
            upgrades.Add(weaponUpgrade);
            weaponUpgrade.Equip(this);
        }

        base.AddUpgrade(upgrade);
    }
}