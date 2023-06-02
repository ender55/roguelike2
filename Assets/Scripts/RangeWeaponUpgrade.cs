public abstract class RangeWeaponUpgrade : WeaponUpgrade
{
    public override void Equip(Weapon weapon)
    {
        if (weapon is RangeWeapon)
        {
            currentWeapon = weapon;
            Activate();
        }
    }

}