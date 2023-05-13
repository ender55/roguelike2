using UnityEngine;

public class Player : Unit
{
    private InputController _inputController;
    
    [SerializeField] private GameObject weapon;

    public IWeapon Weapon { get; set; }

    private void Awake()
    {
        _inputController = gameObject.AddComponent<InputController>();
        Weapon = weapon.GetComponent(typeof(IWeapon)) as IWeapon;
    }

    private void OnEnable()
    {
        _inputController.OnMove += movement.Move;
        _inputController.OnLook += LookAt;
        _inputController.OnAttack += Weapon.Attack;
    }

    private void OnDisable()
    {
        _inputController.OnMove -= movement.Move;
        _inputController.OnLook -= LookAt;
        _inputController.OnAttack -= Weapon.Attack;
    }

    private void LookAt(Vector2 target)
    {
        direction.LookAt(gameObject.transform, target);
        transform.rotation = Quaternion.FromToRotation(Vector2.right, direction.value);
    }
}