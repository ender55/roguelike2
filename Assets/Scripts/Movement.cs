using System;
using UnityEngine;

[Serializable]
public class Movement
{
    [SerializeField] protected int moveSpeed;
    [SerializeField] protected Rigidbody2D rigidbody2D;

    private Vector2 moveDirection = Vector2.zero;

    public int MoveSpeed
    {
        get => moveSpeed;
        set
        {
            moveSpeed = Mathf.Max(100, value); 
            OnMoveSpeedChange?.Invoke();
        }
    }

    public Vector2 MoveDirection => moveDirection;

    public event Action OnMoveSpeedChange;
    public event Action OnMove;

    public virtual void Move(Vector2 moveDirection)
    {
        rigidbody2D.velocity = moveDirection * (MoveSpeed * Time.fixedDeltaTime);
        OnMove?.Invoke();
    }

    public void SetMoveDirection(Vector2 moveDirection)
    {
        this.moveDirection = moveDirection;
    }

    public void Move() //todo: rework
    {
        rigidbody2D.velocity = moveDirection * (MoveSpeed * Time.fixedDeltaTime);
        OnMove?.Invoke();
    }
}