﻿using UnityEngine;

public class CameraObject : MonoBehaviour, IMovable, IDirectable
{
    [SerializeField] private Movement movement = new LerpMovement();
    [SerializeField] private Direction direction;
    [SerializeField] private Unit target; //todo: inject

    public Movement Movement => movement;
    public Direction Direction => direction;

    private void FixedUpdate()
    {
        if (target != null)
        {
            direction.SetDirection(target.transform.position);
            movement.Move(direction.Value);
        }
    }
}
