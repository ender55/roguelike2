using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class Energy
{
    [SerializeField] private int currentEnergy;
    [SerializeField] private int maxEnergy;
    [SerializeField] private int energyRegeneration;
    
    private Coroutine _energyRegenerationCoroutine;
    
    public int CurrentEnergy => currentEnergy;
    public int MaxEnergy => maxEnergy;

    public int EnergyRegeneration
    {
        get => energyRegeneration;
        set
        {
            energyRegeneration = value;
            if (energyRegeneration < 0)
            {
                energyRegeneration = 0;
            }
        }
    }

    public event Action OnEnergyChange;
    public event Action OnMaxEnergyChange;

    public void ChangeCurrentEnergy(int value)
    {
        int newCurrentHp = currentEnergy + value;
        if(newCurrentHp <= 0)
        {
            currentEnergy = 0;
        }
        else if(newCurrentHp > maxEnergy)
        {
            currentEnergy = maxEnergy;
        }
        else
        {
            currentEnergy = newCurrentHp;
        }
        OnEnergyChange?.Invoke();
    }

    public void ChangeMaxEnergy(int value)
    {
        int newMaxHp = maxEnergy + value;
        if(newMaxHp < currentEnergy)
        {
            SetCurrentEnergy(newMaxHp);
        }
        maxEnergy = newMaxHp;
        OnMaxEnergyChange?.Invoke();
    }

    private void SetCurrentEnergy(int value)
    {
        int newCurrentHp = value;
        if(newCurrentHp <= 0)
        {
            currentEnergy = 0;
        }
        else if(newCurrentHp > maxEnergy)
        {
            currentEnergy = maxEnergy;
        }
        else
        {
            currentEnergy = newCurrentHp;
        }
        OnEnergyChange?.Invoke();
    }
    
    private void SetMaxEnergy(int value)
    {
        int newMaxHp = value;
        if(newMaxHp < currentEnergy)
        {
            SetCurrentEnergy(newMaxHp);
        }
        maxEnergy = newMaxHp;
        OnMaxEnergyChange?.Invoke();
    }

    private IEnumerator RegenerateEnergy()
    {
        while(true)
        {
            yield return new WaitForSeconds(1f);
            ChangeCurrentEnergy(energyRegeneration);
        }
    }

    public void StartRegenerateEnergy() //todo: try find another solution for energy and health regeneration
    {
        if (_energyRegenerationCoroutine == null)
        {
            _energyRegenerationCoroutine = CoroutineManager.Start(RegenerateEnergy());
        }
    }

    public void StopRegenerateEnergy()
    {
        if (_energyRegenerationCoroutine != null && CoroutineManager.instance != null)
        {
            CoroutineManager.Stop(_energyRegenerationCoroutine);
            _energyRegenerationCoroutine = null;
        }
    }
}
