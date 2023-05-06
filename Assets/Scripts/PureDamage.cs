class PureDamage : IDamage
{
    private int _amount;

    public int Amount => _amount;
    
    public PureDamage(int amount)
    {
        _amount = amount * -1;
    }
    
    public void ApplyDamage(IDamagable damagable)
    {
        damagable.Health.ChangeCurrentHp(_amount);
    }
}