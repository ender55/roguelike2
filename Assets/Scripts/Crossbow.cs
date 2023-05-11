using UnityEngine;

public class Crossbow : MonoBehaviour, IWeapon
{
    [SerializeField] private IDamage damage = new PureDamage(5);
    [SerializeField] private Projectile projectile;
    
    public void Attack()
    {
        var projectileInstance = Instantiate(projectile);
        projectileInstance.OnHit += ApplyDamage;
    }

    public void AlternativeAttack()
    {
    }

    private void ApplyDamage(IDamageable damageable)
    {
        damage.ApplyDamage(damageable);
    }
}
