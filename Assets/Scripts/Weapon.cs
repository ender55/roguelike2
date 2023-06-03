using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class Weapon : MonoBehaviour, IUpgradeCollector, IUpgradable
{
    [SerializeField] protected WeaponData weaponData;
    [SerializeField] protected WeaponModifiers weaponModifiers;
    [SerializeField] private SpriteRenderer spriteRenderer;
    
    private UpgradeInventory _upgradeInventory; 
    protected List<Upgrade> _upgrades = new List<Upgrade>();

    public WeaponData WeaponData
    {
        get => weaponData;
        set => weaponData = value;
    }
    
    public WeaponModifiers WeaponModifiers => weaponModifiers;

    public SpriteRenderer SpriteRenderer => spriteRenderer;
    public UpgradeInventory UpgradeInventory => _upgradeInventory;
    public List<Upgrade> Upgrades => _upgrades;

    public event Action OnAttack;
    public event Action OnAlternativeAttack;
    public event Action<IDamageable> OnHit;

    private void OnEnable()
    {
        if (_upgradeInventory != null)
        {
            _upgradeInventory.OnInventoryChange += RefreshUpgrades;
        }
    }

    private void OnDisable()
    {
        if (_upgradeInventory != null)
        {
            _upgradeInventory.OnInventoryChange -= RefreshUpgrades;
        }
    }

    public virtual void Attack()
    {
        Debug.Log(weaponModifiers.DamageModifier);
        OnAttack?.Invoke();
    }

    public virtual void AlternativeAttack()
    {
        OnAlternativeAttack?.Invoke();
    }

    protected virtual void ApplyDamage(IDamageable damageable)
    {
        Damage tempDamage = weaponData.BaseDamage;
        tempDamage.DamageValue = (int)(tempDamage.DamageValue * weaponModifiers.DamageModifier);
        damageable.TakeDamage(tempDamage);
        OnHit?.Invoke(damageable);
    }

    public void SetUpgradeInventory(UpgradeInventory inventory)
    {
        _upgradeInventory = inventory;
        _upgradeInventory.OnInventoryChange += RefreshUpgrades;
    }

    public void RefreshUpgrades()
    {
        foreach (var upgrade in _upgrades)
        {
            upgrade.Deactivate();
        }
        
        _upgrades.Clear();

        foreach (var slot in _upgradeInventory.GetInventorySlots())
        {
            if (slot.Item != null)
            {
                AddUpgrade(slot.Item as Upgrade);
            }
        }
    }

    public virtual void AddUpgrade(Upgrade upgrade)
    {
        if (upgrade is GeneralWeaponUpgrade weaponUpgrade)
        {
            _upgrades.Add(weaponUpgrade);
            weaponUpgrade.Equip(this);
        }
    }

    public void RemoveUpgrade(Upgrade upgrade)
    {
        if (_upgrades.Contains(upgrade))
        {
            _upgrades.Find(item => item == upgrade).Deactivate();
            _upgrades.Remove(upgrade);
        }
    }
}