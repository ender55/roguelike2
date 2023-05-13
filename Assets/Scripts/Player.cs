using UnityEngine;

public class Player : Unit
{
    private InputController _inputController;

    [SerializeReference] public IWeapon Weapon;
    [SerializeField] private Projectile projectile;

    private void Awake()
    {
        _inputController = gameObject.AddComponent<InputController>();
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
        transform.rotation = Quaternion.FromToRotation(Vector2.right, direction.value);
    }

    private void Update() //todo: remove
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