using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class Upgrade : Item
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
            Deactivate();
            currentLevel = value;
            Activate();
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

    protected override void Collect(Player player)
    {
        if (player.UpgradeInventory.TryAddItem(this))
        {
            //Destroy(gameObject, 5f); //todo: rework item collecting system
        }
    }

    public abstract void Activate();
    
    public abstract void Deactivate();
}