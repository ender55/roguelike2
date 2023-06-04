using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class Weapon : MonoBehaviour, IUpgradeCollector, IUpgradable
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
        if (upgradeInventory != null)
        {
            upgradeInventory.OnInventoryChange -= RefreshUpgrades;
        }
    }

    public void PerformAttack()
    {
        if (attackCoroutine == null)
        {
            Attack();
        }
    }

    protected virtual void Attack()
    {
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