using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private InputController _inputController;

    private void Awake()
    {
        _inputController = gameObject.AddComponent<InputController>();
    }

    private void OnEnable()
    {
        _inputController.OnMove += Move;
    }

    private void OnDisable()
    {
        _inputController.OnMove -= Move;
    }

    private void Move(Vector2 moveDirection)
    {
        Debug.Log(moveDirection);
    }
}