using System;
using UnityEngine;

public class Projectile : MonoBehaviour, IMovable, IDirectionable
{
    [SerializeField] private Movement movement;
    [SerializeField] private Direction direction;
    [SerializeField] private float lifeTime;

    public Movement Movement => movement;
    public Direction Direction => direction;

    public event Action<IDamageable> OnHit;

    private void Awake()
    {
        direction.value = transform.right;
        Destroy(gameObject, lifeTime);
    }

    private void FixedUpdate()
    {
        movement.Move(direction.value);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.TryGetComponent(out IDamageable damageable))
        {
            OnHit?.Invoke(damageable);
        }
    }
}
