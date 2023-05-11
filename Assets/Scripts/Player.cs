using System;
using UnityEngine;

public class Player : MonoBehaviour, IMovable, IDamageable, IDirectionable
{
    [SerializeField] private Health health;
    [SerializeField] private PhysicalProtection physicalProtection;
    [SerializeField] private Movement movement;
    [SerializeField] private Direction direction;
    
    private InputController _inputController;

    public Health Health => health;
    public PhysicalProtection PhysicalProtection => physicalProtection;
    public Movement Movement => movement;
    public Direction Direction => direction;

    [SerializeReference] public IWeapon Weapon;
    [SerializeField] private Projectile projectile;

    private void Awake()
    {
        _inputController = gameObject.AddComponent<InputController>();
        physicalProtection.Init(); //todo: убрать init и просто сделать метод CalculateProtection
    }

    private void OnEnable()
    {
        _inputController.OnMove += movement.Move;
        _inputController.OnLook += LookAt;
    }

    private void OnDisable()
    {
        _inputController.OnMove -= movement.Move;
        _inputController.OnLook -= LookAt;
    }

    private void LookAt(Vector2 target)
    {
        direction.LookAt(gameObject.transform, target);
    }

    private void Update()
    {
        Shoot();
    }

    private void Shoot() //todo: remove
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Instantiate(projectile, transform.position, Quaternion.LookRotation(Vector3.forward, Direction.value));
        }
    }
}