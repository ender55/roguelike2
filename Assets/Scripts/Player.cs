using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit, IUpgradeCollector, IWeaponCollector, IInputHandler, IEnergyUser, IWeaponUser, IItemDropper
{
    [SerializeField] private Energy energy;
    [SerializeField] private Transform weaponSlot; //todo: maybe should move to IWeaponUser
    [SerializeField] private WeaponInventory weaponInventory;
    [SerializeField] private UpgradeInventory upgradeInventory;
    [SerializeField] private List<InventoryWeaponItem> starterWeapons;
    [SerializeField] private ItemSpawner itemSpawner;

    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private InputController _inputController;
    private InventorySlot _currentSlot;
    private Coroutine _weaponEquipCooldown; //todo: rework using coroutine manager

    public Energy Energy => energy;
    public UpgradeInventory UpgradeInventory => upgradeInventory;
    public WeaponInventory WeaponInventory => weaponInventory;
    public InputController InputController => _inputController;

    public Weapon Weapon { get; private set; }

    public ItemSpawner ItemSpawner => itemSpawner;

    public static event Action OnGameOver;

    private void Awake()
    {
        _inputController = gameObject.AddComponent<InputController>();
        _inputController.SetActionMap("Gameplay");
        upgradeInventory = new UpgradeInventory(10);
        weaponInventory = new WeaponInventory(4);
        itemSpawner = Instantiate(itemSpawner, gameObject.transform);
    }

    private void Start()
    {
        foreach(InventoryWeaponItem weapon in starterWeapons)
        {
            weapon.Awake();
            weaponInventory.TryAddItem(weapon);
        }
        if (weaponInventory.GetInventorySlots()[0].Item != null)
        {
            //weaponInventory.GetInventorySlots()[0].Item.Awake();
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
        _inputController.OnMove += SetAnimator;
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
        _inputController.OnMove -= SetAnimator;
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
        weaponSlot.parent.rotation = Quaternion.FromToRotation(Vector2.right, direction.Value);
        if(direction.Value.x >= 0)
        {
            spriteRenderer.flipX = false;
            if (Weapon != null)
            {
                Weapon.SpriteRenderer.flipY = false;
            }
        }
        else
        {
            spriteRenderer.flipX = true;
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

                if(direction.Value.x < 0)
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

    public void DropItem(InventoryItem item)
    {
        itemSpawner.SpawnItem(item, transform.position);
    }

    private void SetAnimator(Vector2 moveDirection)
    {
        if (moveDirection != Vector2.zero)
        {
            animator.SetBool("IsMoving", true);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }
    }

    protected override void Death()
    {
        OnGameOver?.Invoke();
        _inputController.SetActionMap("UI");
        base.Death();
    }
}
