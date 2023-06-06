using System;
using System.Collections;
using UnityEngine;

public class Enemy : Unit
{
    [SerializeField] private Player player;
    [SerializeField] private EnemyAI enemyAI;

    protected override void OnEnable()
    {
        base.OnEnable();
        player.Movement.OnMove += enemyAI.UpdatePath;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        player.Movement.OnMove -= enemyAI.UpdatePath;
    }
    
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
        direction.SetDirection(enemyAI.CalculatePath());
    }

    private void FixedUpdate()
    {
        movement.Move(direction.Value);
    }
}
