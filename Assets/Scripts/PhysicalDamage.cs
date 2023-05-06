using System;
using UnityEngine;

class PhysicalDamage : IDamage
{
    private int _amount;

    public int Amount => _amount;

    public PhysicalDamage(int amount)
    {
        _amount = amount * -1;
    }
    
    public void ApplyDamage(IDamagable damagable)
    {
        damagable.Health.ChangeCurrentHp(Convert.ToInt32(_amount * damagable.PhysicalProtection.ProtectionMultiplier));
    }
}