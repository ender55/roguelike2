using System;
using UnityEngine;

[Serializable]
public class Movement
{
    [SerializeField] private int moveSpeed;
    private Rigidbody2D _rigidbody2D;

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

    public void Init(Rigidbody2D rigidbody2D)
    {
        _rigidbody2D = rigidbody2D;
    }

    public virtual void Move(Vector2 moveDirection)
    {
        _rigidbody2D.velocity = moveDirection * MoveSpeed * Time.fixedDeltaTime;
    }
}