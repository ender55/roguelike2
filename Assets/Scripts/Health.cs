using UnityEngine;
using System;
using System.Collections;

[Serializable]
public class Health
{
    [SerializeField] private int currentHp;
    [SerializeField] private int maxHp;
    [SerializeField] private int hpRegeneration;
    
    private Coroutine _healthRegenerationCoroutine;
    
    public int CurrentHp => currentHp;
    public int MaxHp => maxHp;

    public int HpRegeneration
    {
        get => hpRegeneration;
        set
        {
            hpRegeneration = value;
            if (hpRegeneration < 0)
            {
                hpRegeneration = 0;
            }
        }
    }

    public event Action OnHpChange;
    public event Action OnMaxHpChange;
    public event Action OnZeroHp;
    
    public void ChangeCurrentHp(int value)
    {
        int newCurrentHp = currentHp + value;
        if(newCurrentHp <= 0)
        {
            currentHp = 0;
            OnZeroHp?.Invoke();
        }
        else if(newCurrentHp > maxHp)
        {
            currentHp = maxHp;
        }
        else
        {
            currentHp = newCurrentHp;
        }
        OnHpChange?.Invoke();
    }

    public void ChangeMaxHp(int value)
    {
        int newMaxHp = maxHp + value;
        if(newMaxHp < currentHp)
        {
            SetCurrentHp(newMaxHp);
        }
        maxHp = newMaxHp;
        OnMaxHpChange?.Invoke();
    }

    private void SetCurrentHp(int value)
    {
        int newCurrentHp = value;
        if(newCurrentHp <= 0)
        {
            currentHp = 0;
            OnZeroHp?.Invoke();
        }
        else if(newCurrentHp > maxHp)
        {
            currentHp = maxHp;
        }
        else
        {
            currentHp = newCurrentHp;
        }
        OnHpChange?.Invoke();
    }
    
    private void SetMaxHp(int value)
    {
        int newMaxHp = value;
        if(newMaxHp < currentHp)
        {
            SetCurrentHp(newMaxHp);
        }
        maxHp = newMaxHp;
        OnMaxHpChange?.Invoke();
    }

    private IEnumerator RegenerateHp()
    {
        while(true)
        {
            yield return new WaitForSeconds(1f);
            ChangeCurrentHp(hpRegeneration);
        }
    }

    public void StartRegenerateHp()
    {
        if (_healthRegenerationCoroutine == null)
        {
            _healthRegenerationCoroutine = CoroutineManager.Start(RegenerateHp());
        }
    }

    public void StopRegenerateHp()
    {
        if (_healthRegenerationCoroutine != null && CoroutineManager.instance != null)
        {
            CoroutineManager.Stop(_healthRegenerationCoroutine);
            _healthRegenerationCoroutine = null;
        }
    }
}
