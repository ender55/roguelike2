using System;
using UnityEngine;

[Serializable]
public class Direction
{
    [SerializeField] private Vector2 value;

    public Vector2 Value => value;

    public void LookAt(Transform objectTransform, Vector2 target)
    {
        value = target - new Vector2(objectTransform.position.x, objectTransform.position.y);
    }
}
