using AYellowpaper.SerializedCollections;
using UnityEngine;

public class Unit : MonoBehaviour, IMovable, IDamageable, IDirectable, IStateHandler
{
    [SerializeField] protected Health health;
    [SerializeField] protected SerializedDictionary<DamageType, int> damageResistances;
    [SerializeField] protected float invulnerabilityTime;
    [SerializeField] protected Movement movement;
    [SerializeField] protected Direction direction;

    private Coroutine _healthRegenerationCoroutine;

    public Health Health => health;
    public SerializedDictionary<DamageType, int> DamageResistances => damageResistances;
    public float InvulnerabilityTime => invulnerabilityTime;
    public Movement Movement => movement;
    public Direction Direction => direction;
    public StateMachine StateMachine { get; } = new StateMachine();

    public void TakeDamage(Damage damage)
    {
        if (!StateMachine.HasState<DamagedState>())
        {
            StateMachine.SetState(new DamagedState(invulnerabilityTime));
            var resistance = GetResistance(damage.DamageType);
            health.ChangeCurrentHp(-damage.DamageValue * (1 - resistance / (50 + resistance)));
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

    protected virtual void OnEnable()
    {
        Health.StartRegenerateHp();
        health.OnZeroHp += Death;
    }

    protected virtual void OnDisable()
    {
        Health.StopRegenerateHp();
        health.OnZeroHp -= Death;
    }

    protected virtual void Death()
    {
        Destroy(gameObject);
    }
}
