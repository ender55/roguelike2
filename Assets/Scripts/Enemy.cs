using System;
using System.Collections;
using UnityEngine;

public class Enemy : Unit, IItemDropper
{
    [SerializeField] private Player player; //todo: inject
    [SerializeField] private EnemyAI enemyAI;
    [SerializeField] private ItemSpawner itemSpawner; //todo: inject
    [Range(0f, 100f)][SerializeField] private float dropChance;

    private Coroutine coroutine;

    public ItemSpawner ItemSpawner => itemSpawner;

    public float DropChance => dropChance;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(1f);
        if (player)
        {
            enemyAI.Initialize(player.transform);
        }
    }

    private void Update()
    {
        if(coroutine == null)
        {
            coroutine = StartCoroutine(UpdatePath());
        }

        direction.SetDirection(enemyAI.CalculatePath());
        transform.rotation = Quaternion.FromToRotation(Vector2.right, direction.Value); //rework when will be sprites
    }

    private void FixedUpdate()
    {
        movement.Move(direction.Value);
    }

    private IEnumerator UpdatePath()
    {
        enemyAI.UpdatePath();
        yield return new WaitForSeconds(0.5f);
        coroutine = null;
    }

    private void OnCollisionStay2D(Collision2D collision) //todo: remove when enemy starts using weapons
    {
        if(collision.gameObject.TryGetComponent(out Player player))
        {
            player.TakeDamage(new Damage(DamageType.Physical, 10));
        }
    }

    public void DropItem()
    {
        if(itemSpawner != null)
        {
            if(UnityEngine.Random.Range(0f, 100f) <= dropChance)
            {
                itemSpawner.SpawnRandomUpgrade(transform.position);
            }
        }
    }

    protected override void Death()
    {
        DropItem();
        base.Death();
    }
}
