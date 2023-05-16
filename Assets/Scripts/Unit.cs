using AYellowpaper.SerializedCollections;
using UnityEngine;

public class Unit : MonoBehaviour, IMovable, IDamageable, IDirectionable
{
    [SerializeField] protected Health health;
    [SerializeField] protected SerializedDictionary<DamageType, int> damageResistances;
    [SerializeField] protected Movement movement;
    [SerializeField] protected Direction direction;
    
    public Health Health => health;
    public SerializedDictionary<DamageType, int> DamageResistances => damageResistances;
    public Movement Movement => movement;
    public Direction Direction => direction;
    
    public void TakeDamage(Damage damage)
    {
        foreach (var damageValue in damage.DamageValues)
        {
            var resistance = GetResistance(damageValue.Key);
            health.ChangeCurrentHp(-damageValue.Value * (1 - resistance/(50 + resistance)));
        }
    }

    private int GetResistance(DamageType damageType)
    {
        if (DamageResistances.TryGetValue(damageType, out var value))
        {
            return value;
        }
        return 0;
    }
}
