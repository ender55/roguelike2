using AYellowpaper.SerializedCollections;

public interface IDamageable
{
    public Health Health { get; }
    public SerializedDictionary<DamageType, int> DamageResistances { get; }
    public float InvulnerabilityTime { get; }
    public void TakeDamage(Damage damage);
}
