public abstract class WeaponUpgrade: Upgrade
{
    protected Weapon currentWeapon;

    public Weapon CurrentWeapon => currentWeapon;

    public virtual void Equip(Weapon weapon)
    {
        currentWeapon = weapon;
        Activate();
    }

    public virtual void Unequip()
    {
        Deactivate();
        currentWeapon = null;
    }
}
