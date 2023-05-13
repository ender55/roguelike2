using AYellowpaper.SerializedCollections;
using UnityEngine;

public abstract class Unit : MonoBehaviour, IMovable, IDamageable, IDirectionable
{
    [SerializeField] protected Health health;
    [SerializeField] protected SerializedDictionary<DamageType, int> damageResistances;
    [SerializeField] protected Movement movement;
    [SerializeField] protected Direction direction;
    
    public Health Health => health;
    public SerializedDictionary<DamageType, int> DamageResistances => damageResistances;
    public Movement Movement => movement;
    public Direction Direction => direction;
    
    public void TakeDamage(int damageValue, DamageType damageType) //todo: move to Unit class
    {
        if (DamageResistances.ContainsKey(damageType))
        {
            health.ChangeCurrentHp(-damageValue * (1 - DamageResistances[damageType] / 100));
        }
        else
        {
            health.ChangeCurrentHp(-damageValue);
        }
    }
}
