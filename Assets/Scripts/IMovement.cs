using UnityEngine;

public interface IMovement
{
    public int MoveSpeed { get; set; }
    public void Move(Vector2 moveDirection);
}