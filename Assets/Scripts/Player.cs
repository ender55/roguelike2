using System;
using UnityEngine;

public class Player : MonoBehaviour, IMovable, IDamagable
{
    private IMovement movement;
    [SerializeField] private Health health;
    [SerializeField] private PhysicalProtection physicalProtection;
    private InputController _inputController;

    public Health Health => health;
    public PhysicalProtection PhysicalProtection => physicalProtection;
    public IMovement Movement => movement;

    private void Awake()
    {
        _inputController = gameObject.AddComponent<InputController>();
        movement = new DefaultMovement(gameObject.GetComponent<Rigidbody2D>());
        physicalProtection.ProtectionValue = physicalProtection.ProtectionValue;
    }

    private void OnEnable()
    {
        _inputController.OnMove += movement.Move;
    }

    private void OnDisable()
    {
        _inputController.OnMove -= movement.Move;
    }
}