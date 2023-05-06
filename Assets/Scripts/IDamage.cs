public interface IDamage
{
    public int Amount { get; }
    public void ApplyDamage(IDamagable damagable);
}