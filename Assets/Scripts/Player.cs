using System.Collections;
using UnityEngine;

public class Player : Unit, IUpgradeCollector, IWeaponCollector, IInputHandler, IEnergyUser, IWeaponUser
{
    [SerializeField] private Energy energy;
    [SerializeField] private Transform weaponSlot;
    [SerializeField] private WeaponInventory weaponInventory;
    [SerializeField] private UpgradeInventory upgradeInventory;

    private InputController _inputController;
    private InventorySlot _currentSlot;
    private Coroutine _weaponEquipCooldown; //todo: rework using coroutine manager

    public Energy Energy => energy;
    public UpgradeInventory UpgradeInventory => upgradeInventory;
    public WeaponInventory WeaponInventory => weaponInventory;
    public InputController InputController => _inputController;

    public Weapon Weapon { get; private set; }
    
    private void Awake()
    {
        _inputController = gameObject.AddComponent<InputController>();
        _inputController.SetActionMap("Gameplay");
        upgradeInventory = new UpgradeInventory(10);
        weaponInventory = new WeaponInventory(4);
        if (weaponInventory.GetInventorySlots()[0].Item != null)
        {
            EquipWeapon(0);
        }
    }

    private void FixedUpdate()
    {
        movement.Move();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        energy.StartRegenerateEnergy();
        _inputController.OnMove += movement.SetMoveDirection;
        _inputController.OnLook += LookAt;
        if (Weapon != null)
        {
            _inputController.OnAttack += Weapon.PerformAttack;
        }

        _inputController.OnWeaponChoose += EquipWeapon;
    }

    protected override void OnDisable()
    {
        energy.StopRegenerateEnergy();
        base.OnDisable();
        _inputController.OnMove -= movement.SetMoveDirection;
        _inputController.OnLook -= LookAt;
        if (Weapon != null)
        {
            _inputController.OnAttack -= Weapon.PerformAttack;
        }
        
        _inputController.OnWeaponChoose -= EquipWeapon;
    }

    private void LookAt(Vector2 target)
    {
        direction.LookAt(gameObject.transform, target);
        transform.rotation = Quaternion.FromToRotation(Vector2.right, direction.Value);
        if(gameObject.transform.rotation.eulerAngles.z <= 90 || gameObject.transform.rotation.eulerAngles.z >= 270)
        {
            if (Weapon != null)
            {
                Weapon.SpriteRenderer.flipY = false;
            }
        }
        else
        {
            if (Weapon != null)
            {
                Weapon.SpriteRenderer.flipY = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.TryGetComponent(out PickupItem pickupItem))
        {
            if (pickupItem.Item is Upgrade)
            {
                if(upgradeInventory.TryAddItem(pickupItem.Item))
                {
                    Destroy(pickupItem.gameObject);
                }
            }
            else if (pickupItem.Item is InventoryWeaponItem)
            {
                if (weaponInventory.TryAddItem(pickupItem.Item))
                {
                    Destroy(pickupItem.gameObject);
                }
            }
        }
    }

    private void EquipWeapon(int weaponNumber) //todo: change attribute to Weapon
    {
        if (_weaponEquipCooldown == null)
        {
            _weaponEquipCooldown = StartCoroutine(WeaponEquipCooldown());
            var chosenWeaponSlot = weaponInventory.GetInventorySlots()[weaponNumber];
            var chosenWeapon = chosenWeaponSlot.Item as InventoryWeaponItem;
            if (chosenWeapon != null && chosenWeapon.WeaponPrefab.TryGetComponent(out Weapon weaponComponent))
            {
                if (Weapon != null && _currentSlot != null)
                {
                    UnequipWeapon();
                }

                Weapon = Instantiate(chosenWeapon.WeaponPrefab, weaponSlot).GetComponent<Weapon>();
                Weapon.EnergySource = this;
                if (chosenWeapon is InventoryRangeWeaponItem)
                {
                    var chosenRangeWeapon = chosenWeapon as InventoryRangeWeaponItem;
                    Weapon.SetUpgradeInventory(chosenRangeWeapon.UpgradeInventory);
                }

                _currentSlot = chosenWeaponSlot;
                _inputController.OnAttack += Weapon.PerformAttack;
                _currentSlot.OnSlotChanged += UnequipWeapon;
                if (gameObject.transform.rotation.eulerAngles.z > 90 &&
                    gameObject.transform.rotation.eulerAngles.z < 270)
                {
                    Weapon.SpriteRenderer.flipY = true;
                }
            }
        }
    }

    private void UnequipWeapon()
    {
        _inputController.OnAttack -= Weapon.PerformAttack;
        _currentSlot.OnSlotChanged -= UnequipWeapon;
        Destroy(Weapon.gameObject);
        Weapon = null;
        _currentSlot = null;
    }

    private IEnumerator WeaponEquipCooldown() //todo: handle with weapon equip cooldown using coroutine for cooldown
    {
        yield return new WaitForSeconds(0.5f);
        _weaponEquipCooldown = null;
    }
}