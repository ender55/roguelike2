using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class Weapon : MonoBehaviour, IUpgradeCollector, IUpgradable //todo: clear code
{
    [SerializeField] protected WeaponData weaponData;
    [SerializeField] protected WeaponModifiers weaponModifiers;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [ReadOnly] public UpgradeInventory upgradeInventory; //todo: make private
    protected List<Upgrade> _upgrades = new List<Upgrade>();

    public WeaponData WeaponData
    {
        get => weaponData;
        set => weaponData = value;
    }
    
    public WeaponModifiers WeaponModifiers => weaponModifiers;

    public SpriteRenderer SpriteRenderer => spriteRenderer;
    public UpgradeInventory UpgradeInventory => upgradeInventory;
    public List<Upgrade> Upgrades => _upgrades;

    public event Action OnAttack;
    public event Action OnAlternativeAttack;
    public event Action<IDamageable> OnHit;

    private void OnEnable()
    {
        if (upgradeInventory != null)
        {
            upgradeInventory.OnInventoryChange += RefreshUpgrades;
        }
    }

    private void OnDisable()
    {
        if (upgradeInventory != null)
        {
            upgradeInventory.OnInventoryChange -= RefreshUpgrades;
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
        upgradeInventory = inventory;
        upgradeInventory.OnInventoryChange += RefreshUpgrades;
    }

    public void RefreshUpgrades()
    {
        foreach (var upgrade in _upgrades)
        {
            upgrade.Deactivate();
        }
        
        _upgrades.Clear();

        foreach (var slot in upgradeInventory.GetInventorySlots())
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