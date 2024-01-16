using System.Collections;
using UnityEngine;

class DamagedState : IState
{
    private float _invulnerabilityTime;
    private Coroutine _coroutine;
    
    public StateMachine StateMachine { get; set; }

    public DamagedState(float invulnerabilityTime)
    {
        _invulnerabilityTime = invulnerabilityTime;
    }
    
    public void Enter()
    {
        _coroutine = CoroutineManager.Start(DamageCooldown());
    }

    public void Exit()
    {
        if (_coroutine != null)
        {
            CoroutineManager.Stop(_coroutine);
            _coroutine = null;
        }
    }

    private IEnumerator DamageCooldown()
    {
        yield return new WaitForSeconds(_invulnerabilityTime);
        StateMachine.DeleteState<DamagedState>();
    }
}