public interface IDamage
{
    public int Amount { get; set; }
    public void ApplyDamage(IDamageable damageable);
}