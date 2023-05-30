using System;
using UnityEngine;

[Serializable]
public abstract class Upgrade : InventoryItem
{
    [SerializeField] protected UpgradeRarity upgradeRarity;
    [SerializeField] protected int currentLevel;
    [SerializeField] protected int maxLevel;

    public UpgradeRarity UpgradeRarity
    {
        get => upgradeRarity;
        set
        {
            Deactivate();
            upgradeRarity = value;
            Activate();
        }
    }
    
    public int CurrentLevel
    {
        get => currentLevel;
        set
        {
            if (value > 0 && value <= maxLevel)
            {
                Deactivate();
                currentLevel = value;
                Activate();
            }
        }
    }

    public abstract void Activate();
    
    public abstract void Deactivate();
}