using System;
using UnityEngine;

public class Projectile : MonoBehaviour, IMovable, IDirectable
{
    [SerializeField] private Movement movement;
    [SerializeField] private Direction direction;
    [SerializeField] private float lifeTime;

    public Movement Movement => movement;
    public Direction Direction => direction;

    public event Action<IDamageable> OnHit;

    private void Awake()
    {
        direction.SetDirection(transform.right);
        Destroy(gameObject, lifeTime);
    }

    private void FixedUpdate()
    {
        movement.Move(direction.Value);
    }

    //private void OnTriggerEnter2D(Collider2D col)
    //{
    //    if (col.gameObject.TryGetComponent(out IDamageable damageable))
    //    {
    //        OnHit?.Invoke(damageable);
    //    }
    //    Destroy(gameObject);
    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IDamageable damageable))
        {
            OnHit?.Invoke(damageable);
        }
        if (!collision.gameObject.TryGetComponent(out Projectile projectile))
        {
            Destroy(gameObject);
        }
    }
}
