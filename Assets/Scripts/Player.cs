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

    private void Awake()
    {
        _inputController = gameObject.AddComponent<InputController>();
        movement.Init(gameObject.GetComponent<Rigidbody2D>());
        physicalProtection.Init();
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
}