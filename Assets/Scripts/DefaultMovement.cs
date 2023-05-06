using System;
using UnityEngine;

[Serializable]
public class DefaultMovement : IMovement
{
    private Rigidbody2D _rigidbody2D;

    public int MoveSpeed { get; set; } = 100;

    public DefaultMovement(Rigidbody2D rigidbody2D)
    {
        _rigidbody2D = rigidbody2D;
    }
     
    public void Move(Vector2 moveDirection)
    {
        _rigidbody2D.velocity = moveDirection * MoveSpeed * Time.fixedDeltaTime;
    }
}