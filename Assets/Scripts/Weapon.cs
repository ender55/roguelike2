using System;
using System.Collections;
using System.Collections.Generic;
using TNRD;
using UnityEngine;

[Serializable]
public abstract class Weapon : MonoBehaviour, IUpgradeCollector, IUpgradable, IEnergyConsumer
{
    [SerializeField] protected WeaponData weaponData;
    [SerializeField] protected WeaponModifiers weaponModifiers;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private int upgradesAmount;
    
    private UpgradeInventory upgradeInventory; 
    protected List<Upgrade> upgrades = new List<Upgrade>();

    protected Coroutine attackCoroutine;

    public WeaponData WeaponData
    {
        get => weaponData;
        set => weaponData = value;
    }

    public IEnergyUser EnergySource
    {
        get;
        set;
    }
    
    public WeaponModifiers WeaponModifiers => weaponModifiers;
    public SpriteRenderer SpriteRenderer => spriteRenderer;
    public UpgradeInventory UpgradeInventory => upgradeInventory;
    public List<Upgrade> Upgrades => upgrades;
    public int UpgradesAmount {
        get 
        {
            if (upgradesAmount < 0)
            {
                upgradesAmount = 0;
            }

            return upgradesAmount;
        }
    }

    public event Action OnAttack;
    public event Action OnAlternativeAttack;
    public event Action<IDamageable> OnHit;

    private void OnEnable()
    {
        attackCoroutine = StartCoroutine(EquipCooldown());
        if (upgradeInventory != null)
        {
            upgradeInventory.OnInventoryChange += RefreshUpgrades;
        }
    }

    private void OnDisable()
    {
        foreach (var upgrade in upgrades)
        {
            upgrade.Deactivate();
        }
        
        if (upgradeInventory != null)
        {
            upgradeInventory.OnInventoryChange -= RefreshUpgrades;
        }
    }

    public void PerformAttack()
    {
        if (attackCoroutine == null && EnergySource.Energy.CurrentEnergy - weaponData.EnergyConsumption >= 0)
        {
            Attack();
        }
    }

    protected virtual void Attack()
    {
        EnergySource.Energy.ChangeCurrentEnergy(-weaponData.EnergyConsumption);
        attackCoroutine = StartCoroutine(AttackCooldown());
        OnAttack?.Invoke();
    }

    protected IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(100 / (weaponData.AttackSpeed * weaponModifiers.AttackSpeedModifier));
        attackCoroutine = null;
    }
    
    protected IEnumerator EquipCooldown()
    {
        yield return new WaitForSeconds(0.5f);
        attackCoroutine = null;
    }

    public virtual void AlternativeAttack()
    {
        OnAlternativeAttack?.Invoke();
    }

    protected virtual void ApplyDamage(IDamageable damageable)
    {
        Damage tempDamage = new Damage(weaponData.BaseDamage.DamageType, weaponData.BaseDamage.DamageValue);
        tempDamage.DamageValue = (int)(tempDamage.DamageValue * weaponModifiers.DamageModifier);
        damageable.TakeDamage(tempDamage);
        OnHit?.Invoke(damageable);
    }

    public void SetUpgradeInventory(UpgradeInventory inventory)
    {
        upgradeInventory = inventory;
        RefreshUpgrades();
        upgradeInventory.OnInventoryChange += RefreshUpgrades;
    }

    public void RefreshUpgrades()
    {
        foreach (var upgrade in upgrades)
        {
            upgrade.Deactivate();
        }
        
        upgrades.Clear();

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
            upgrades.Add(weaponUpgrade);
            weaponUpgrade.Equip(this);
        }
    }

    public void RemoveUpgrade(Upgrade upgrade)
    {
        if (upgrades.Contains(upgrade))
        {
            upgrades.Find(item => item == upgrade).Deactivate();
            upgrades.Remove(upgrade);
        }
    }
}