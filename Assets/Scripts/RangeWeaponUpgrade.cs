public abstract class RangeWeaponUpgrade : Upgrade
{
    protected RangeWeapon currentWeapon;

    public RangeWeapon CurrentWeapon => currentWeapon;

    public void Equip(RangeWeapon weapon)
    {
        currentWeapon = weapon;
        Activate();
    }

    public void Unequip()
    {
        Deactivate();
        currentWeapon = null;
    }
}