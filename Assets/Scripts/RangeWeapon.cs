using System;
using UnityEngine;

public class RangeWeapon : MonoBehaviour, IWeapon
{
    [SerializeField] private Projectile projectile;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private RangeWeaponStats weaponStats;

    public RangeWeaponStats WeaponStats => weaponStats;

    public event Action OnAttack;
    public event Action OnAlternativeAttack;
    public event Action<IDamageable> OnHit; 

    public virtual void Attack()
    {
        OnAttack?.Invoke();
        var projectileInstance = Instantiate(projectile, attackPoint.position, gameObject.transform.rotation);
        projectileInstance.OnHit += ApplyDamage;
    }

    public virtual void AlternativeAttack()
    {
        OnAlternativeAttack?.Invoke();
    }

    private void ApplyDamage(IDamageable damageable)
    {
        Debug.Log("damaged");
        damageable.TakeDamage(weaponStats.BaseDamage);
        OnHit?.Invoke(damageable);
    }
}
