using UnityEngine;
using System;

[Serializable]
public class Health
{
    [SerializeField] private int currentHp;
    [SerializeField] private int maxHp;
    
    public int CurrentHp => currentHp;
    public int MaxHp => maxHp;
    
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

    public void SetCurrentHp(int value)
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
    
    public void SetMaxHp(int value)
    {
        int newMaxHp = value;
        if(newMaxHp < currentHp)
        {
            SetCurrentHp(newMaxHp);
        }
        maxHp = newMaxHp;
        OnMaxHpChange?.Invoke();
    }
}
