class PureDamage : IDamage
{
    private int _amount;

    public int Amount
    {
        get => _amount;
        set => _amount = value;
    }

    public PureDamage(int amount)
    {
        _amount = amount * -1;
    }
    
    public void ApplyDamage(IDamageable damageable)
    {
        damageable.Health.ChangeCurrentHp(_amount);
    }
}