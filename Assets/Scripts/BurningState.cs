using System.Collections;
using UnityEngine;

public class BurningState : IState
{
    private int _burningTime;
    private int _burningDamage;
    private IDamageable _damageable;
    private Coroutine _coroutine = null;

    public StateMachine StateMachine { get; set; }

    public BurningState(IDamageable damageable, int burningTime, int burningDamage)
    {
        _damageable = damageable;
        _burningTime = burningTime;
        _burningDamage = burningDamage;
    }
    
    public void Enter()
    {
        _coroutine = CoroutineManager.Start(Burn());
    }

    public void Exit()
    {
        if (_coroutine != null)
        {
            CoroutineManager.Stop(_coroutine);
            _coroutine = null;
        }
    }

    private IEnumerator Burn()
    {
        for (int i = 0; i < _burningTime * 2; i++)
        {
            _damageable.TakeDamage(new Damage(DamageType.Magical, _burningDamage));
            yield return new WaitForSeconds(0.5f);
        }
        
        StateMachine.DeleteState<BurningState>();
    }
}