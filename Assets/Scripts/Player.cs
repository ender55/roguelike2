using UnityEngine;

public class Player : Unit
{
    private InputController _inputController;
    
    [SerializeField] private GameObject weapon;
    [SerializeField] private Transform weaponSlot;

    [SerializeField] private UpgradeInventory upgradeInventory = new UpgradeInventory(10);

    public UpgradeInventory UpgradeInventory => upgradeInventory;

    public Weapon Weapon { get; set; } //todo: find out if we need it

    private void Awake()
    {
        _inputController = gameObject.AddComponent<InputController>();
        weapon = Instantiate(weapon, weaponSlot);
        Weapon = weapon.GetComponent(typeof(Weapon)) as Weapon;
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