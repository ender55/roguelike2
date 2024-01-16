using System;
using UnityEngine;

public class TestWeapon : MonoBehaviour
{
    //private IDamage _damage = new PhysicalDamage(30);

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.TryGetComponent(out IDamageable component))
        {
            //_damage.ApplyDamage(component);
        }
    }
}
