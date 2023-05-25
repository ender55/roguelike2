using System;
using UnityEngine;

[Serializable]
public abstract class Upgrade : InventoryItem
{
    [SerializeField] protected UpgradeRarity upgradeRarity;
    [SerializeField] protected int currentLevel;
    [SerializeField] protected int maxLevel;
    
    public IInventoryItemInfo ItemInfo { get; } //todo: add constructor

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

    public void LevelUpUpgrade()
    {
        if (currentLevel < maxLevel)
        {
            currentLevel++;
            LevelUp();
        }
    }

    protected virtual void LevelUp()
    {
        
    }

    public abstract void Activate();
    
    public abstract void Deactivate();
}