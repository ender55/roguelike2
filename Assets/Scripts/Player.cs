using UnityEngine;

public class Player : MonoBehaviour, IMovable, IDamageable
{
    [SerializeField] private Health health;
    [SerializeField] private PhysicalProtection physicalProtection;
    [SerializeField] private Movement movement;
    
    private InputController _inputController;

    public Health Health => health;
    public PhysicalProtection PhysicalProtection => physicalProtection;
    public Movement Movement => movement;

    private void Awake()
    {
        _inputController = gameObject.AddComponent<InputController>();
        movement.Init(gameObject.GetComponent<Rigidbody2D>());
        physicalProtection.Init();
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