using System;
using UnityEngine;

public class TestWeapon : MonoBehaviour
{
    private IDamage _damage = new PhysicalDamage(1);

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.TryGetComponent(out IDamagable component))
        {
            _damage.ApplyDamage(component);
        }
    }
}
