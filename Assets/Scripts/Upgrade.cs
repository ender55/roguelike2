using System;
using UnityEngine;

[Serializable]
public abstract class Upgrade : InventoryItem
{
    [SerializeField] protected UpgradeRarity currentUpgradeRarity;
    [SerializeField] protected int currentUpgradeLevel;
    [SerializeField] protected int maxLevel;

    public UpgradeRarity CurrentUpgradeRarity
    {
        get => currentUpgradeRarity;
        set
        {
            Deactivate();
            currentUpgradeRarity = value;
            Activate();
        }
    }
    
    public int CurrentUpgradeLevel
    {
        get => currentUpgradeLevel;
        set
        {
            if (value > 0 && value <= maxLevel)
            {
                Deactivate();
                currentUpgradeLevel = value;
                Activate();
            }
        }
    }

    public abstract void Activate();
    
    public abstract void Deactivate();
}