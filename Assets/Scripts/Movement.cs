using System;
using UnityEngine;

[Serializable]
public class Movement
{
    [SerializeField] private int moveSpeed;
    [SerializeField] private Rigidbody2D rigidbody2D;

    public int MoveSpeed
    {
        get => moveSpeed;
        set
        {
            moveSpeed = Mathf.Max(100, value); 
            OnMoveSpeedChange?.Invoke();
        }
    }

    public event Action OnMoveSpeedChange;

    public virtual void Move(Vector2 moveDirection)
    {
        rigidbody2D.velocity = moveDirection * (MoveSpeed * Time.fixedDeltaTime);
    }
}