using AYellowpaper.SerializedCollections;

public interface IDamageable
{
    public Health Health { get; }
    public SerializedDictionary<DamageType, int> DamageResistances { get; }
    public void TakeDamage(int damageValue, DamageType damageType);
}
