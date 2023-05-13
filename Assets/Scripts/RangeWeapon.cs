using System;
using UnityEngine;

public class RangeWeapon : MonoBehaviour, IWeapon
{
    [SerializeField] private int damageValue;
    [SerializeField] private DamageType damageType;
    [SerializeField] private Projectile projectile;
    [SerializeField] private Transform attackPoint;

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
        damageable.TakeDamage(damageValue, damageType);
        OnHit?.Invoke(damageable);
    }
}
