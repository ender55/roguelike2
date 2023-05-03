using System;
using UnityEngine;

public class Player : MonoBehaviour, IMovable
{
    [SerializeField] private float moveSpeed;
    private IMovement _movement; 
    private InputController _inputController;

    public IMovement Movement => _movement;

    private void Awake()
    {
        _inputController = gameObject.AddComponent<InputController>();
        _movement = new DefaultMovement(gameObject.GetComponent<Rigidbody2D>());
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
        _movement.Move(moveDirection * moveSpeed * 10);
    }
}