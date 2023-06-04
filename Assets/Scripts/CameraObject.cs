using UnityEngine;

public class CameraObject : MonoBehaviour, IMovable, IDirectable
{
    [SerializeField] private Movement movement = new LerpMovement();
    [SerializeField] private Direction direction;
    [SerializeField] private Unit target;

    public Movement Movement => movement;
    public Direction Direction => direction;

    private void FixedUpdate()
    {
        direction.SetDirection(target.transform.position);
        movement.Move(direction.Value);
    }
}
