using System;
using UnityEngine;

[Serializable]
public class Direction
{
    private Vector2 value;

    public Vector2 Value => value;

    public void SetDirection(Vector2 value)
    {
        this.value = value;
    }

    public void RotateByAngle(float angle)
    {
        var radian = angle * Mathf.Deg2Rad;
        var newDirection = new Vector2(
            value.x * Mathf.Cos(radian) - value.y * Mathf.Sin(radian),
            value.x * Mathf.Sin(radian) + value.y * Mathf.Cos(radian)
        );
        value = newDirection;
    }

    public void LookAt(Transform objectTransform, Vector2 target)
    {
        value = target - new Vector2(objectTransform.position.x, objectTransform.position.y);
    }
}
