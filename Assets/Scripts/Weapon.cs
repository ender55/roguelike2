using System;
using UnityEngine;

[Serializable]
public abstract class Weapon : MonoBehaviour, IUpgradeCollector //todo: clear code
{
    [SerializeField] protected WeaponData weaponData;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [ReadOnly] public UpgradeInventory upgradeInventory;

    public WeaponData WeaponData
    {
        get => weaponData;
        set => weaponData = value;
    }

    public SpriteRenderer SpriteRenderer => spriteRenderer;
    public UpgradeInventory UpgradeInventory => upgradeInventory;

    public event Action OnAttack;
    public event Action OnAlternativeAttack;
    public event Action<IDamageable> OnHit;

    public virtual void Attack()
    {
        OnAttack?.Invoke();
    }

    public virtual void AlternativeAttack()
    {
        OnAlternativeAttack?.Invoke();
    }

    //public void AddUpgrade(WeaponUpgrade upgrade)
    //{
    //    upgrades.Add(upgrade);
    //    upgrade.Equip(this);
    //}

    //public void RemoveUpgrade(WeaponUpgrade upgrade)
    //{
    //    upgrade.Unequip();
    //    upgrades.Remove(upgrade);
    //}
    
    protected virtual void ApplyDamage(IDamageable damageable)
    {
        Damage tempDamage = weaponData.BaseDamage;
        tempDamage.DamageValue = (int)(tempDamage.DamageValue * weaponData.DamageModifier);
        damageable.TakeDamage(tempDamage);
        OnHit?.Invoke(damageable);
    }

    public void SetUpgradeInventory(UpgradeInventory inventory)
    {
        upgradeInventory = inventory;
    }
}