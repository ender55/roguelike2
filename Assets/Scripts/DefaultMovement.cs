using UnityEngine;

public class DefaultMovement : IMovement
{
    private Rigidbody2D _rigidbody2D;

    public DefaultMovement(Rigidbody2D rigidbody2D)
    {
        _rigidbody2D = rigidbody2D;
    }
     
    public void Move(Vector2 moveDirection)
    {
        _rigidbody2D.velocity = moveDirection * Time.fixedDeltaTime;
    }
}