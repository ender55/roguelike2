public abstract class GeneralWeaponUpgrade : Upgrade
{
    protected Weapon currentWeapon;

    public Weapon CurrentWeapon => currentWeapon;

    public void Equip(Weapon weapon)
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