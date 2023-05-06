using System;

class PhysicalDamage : IDamage
{
    private int _amount;

    public int Amount
    {
        get => _amount;
        set => _amount = value;
    }

    public PhysicalDamage(int amount)
    {
        _amount = amount * -1;
    }
    
    public void ApplyDamage(IDamageable damageable)
    {
        damageable.Health.ChangeCurrentHp(Convert.ToInt32(_amount * damageable.PhysicalProtection.ProtectionMultiplier));
    }
}