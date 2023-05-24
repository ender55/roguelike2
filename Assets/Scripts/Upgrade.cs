using System;
using UnityEngine;

[Serializable]
public abstract class Upgrade : IInventoryItem
{
    [SerializeField] protected UpgradeRarity upgradeRarity;
    [SerializeField] protected int currentLevel;
    [SerializeField] protected int maxLevel;

    [SerializeField] protected Sprite sprite;

    public Sprite Icon => sprite;
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

    public abstract void Activate();
    
    public abstract void Deactivate();
}