using System;
using UnityEngine;

public class RangeWeapon : MonoBehaviour, IWeapon
{
    [SerializeField] private IDamage damage = new PureDamage(5);
    [SerializeField] private Projectile projectile;
    [SerializeField] private Transform attackPoint;

    public event Action OnAttack;
    public event Action OnAlternativeAttack;
    public event Action<IDamageable> OnHit; 

    public virtual void Attack()
    {
        OnAttack?.Invoke();
        var projectileInstance = Instantiate(projectile, attackPoint);
        projectileInstance.OnHit += ApplyDamage;
    }

    public virtual void AlternativeAttack()
    {
        OnAlternativeAttack?.Invoke();
    }

    private void ApplyDamage(IDamageable damageable)
    {
        damage.ApplyDamage(damageable);
        OnHit?.Invoke(damageable);
    }
}
