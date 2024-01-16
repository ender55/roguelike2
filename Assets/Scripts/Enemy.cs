using System;
using System.Collections;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Enemy : Unit, IItemDropper
{
    [SerializeField] private Player player; //todo: inject
    [SerializeField] private EnemyAI enemyAI;
    [Range(0f, 100f)][SerializeField] private float dropChance;

    [SerializeField] private ItemSpawner itemSpawner;
    [SerializeField] private ItemList itemList; //todo: inject

    [SerializeField] private SpriteRenderer spriteRenderer;

    private Coroutine coroutine;

    public ItemSpawner ItemSpawner => itemSpawner;

    public float DropChance => dropChance;

    private void Start()
    {
        itemSpawner = Instantiate(itemSpawner, gameObject.transform);
        player = GameObject.FindObjectOfType<Player>();
        if (player)
        {
            enemyAI.Initialize(player.transform);
        }
    }

    private void Update()
    {
        enemyAI.UpdatePath();
        direction.SetDirection(enemyAI.CalculatePath());
        if (direction.Value.x >= 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }

    private void FixedUpdate()
    {
        movement.Move(direction.Value);
    }

    private void OnCollisionStay2D(Collision2D collision) //todo: remove when enemy starts using weapons
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            player.TakeDamage(new Damage(DamageType.Physical, 10));
        }
    }

    public void DropItem(InventoryItem item)
    {
        if (UnityEngine.Random.Range(0f, 100f) <= dropChance)
        {
            itemSpawner.SpawnItem(item, transform.position);
        }

    }

    protected override void Death()
    {
        DropItem(itemList.GetRandomUpgrade());
        base.Death();
    }
}
