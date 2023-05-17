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

    protected override void Collect(ICollector collector)
    {
        //var sameItemAmount = 0;
        //var type = this.GetType();
        //foreach (var item in collector.Items)
        //{
        //    if(item.GetType() == type)
        //    {
        //        
        //    }
        //}
        collector.Items.Add(this);
    }

    public abstract void Activate();
    
    public abstract void Deactivate();
}