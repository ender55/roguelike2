using System;
using UnityEngine;

public class Player : Unit, IUpgradeCollector, IWeaponCollector
{
    [SerializeField] private Transform weaponSlot;
    [SerializeField] private WeaponInventory weaponInventory;
    
    [SerializeField] private UpgradeInventory upgradeInventory;

    private InputController _inputController;
    private InventorySlot _currentSlot;

    public UpgradeInventory UpgradeInventory => upgradeInventory;
    public WeaponInventory WeaponInventory => weaponInventory;

    public Weapon Weapon { get; private set; } 

    private void Awake()
    {
        _inputController = gameObject.AddComponent<InputController>();
        upgradeInventory = new UpgradeInventory(10);
        weaponInventory = new WeaponInventory(4);
        if (weaponInventory.GetInventorySlots()[0].Item != null)
        {
            EquipWeapon(0);
        }
    }

    private void OnEnable()
    {
        _inputController.OnMove += movement.Move;
        _inputController.OnLook += LookAt;
        if (Weapon != null)
        {
            _inputController.OnAttack += Weapon.Attack;
        }

        _inputController.OnWeaponChoose += EquipWeapon;
    }

    private void OnDisable()
    {
        _inputController.OnMove -= movement.Move;
        _inputController.OnLook -= LookAt;
        if (Weapon != null)
        {
            _inputController.OnAttack -= Weapon.Attack;
        }
        
        _inputController.OnWeaponChoose -= EquipWeapon;
    }

    private void LookAt(Vector2 target)
    {
        direction.LookAt(gameObject.transform, target);
        transform.rotation = Quaternion.FromToRotation(Vector2.right, direction.value);
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
                upgradeInventory.TryAddItem(pickupItem.Item);
                Destroy(pickupItem.gameObject);
            }
            else if (pickupItem.Item is InventoryWeapon)
            {
                weaponInventory.TryAddItem(pickupItem.Item);
                Destroy(pickupItem.gameObject);
            }
        }
    }

    private void EquipWeapon(int weaponNumber) //todo: change attribute to Weapon and create interface IWeaponUser
    {
        var chosenWeaponSlot = weaponInventory.GetInventorySlots()[weaponNumber];
        var chosenWeapon = chosenWeaponSlot.Item as InventoryWeapon;
        if (chosenWeapon != null && chosenWeapon.WeaponPrefab.TryGetComponent(out Weapon weaponComponent))
        {
            if (Weapon != null && _currentSlot != null)
            {
                UnequipWeapon();
            }
            
            Weapon = Instantiate(chosenWeapon.WeaponPrefab, weaponSlot).GetComponent<Weapon>();
            _currentSlot = chosenWeaponSlot;
            _inputController.OnAttack += Weapon.Attack;
            _currentSlot.OnSlotCleared += UnequipWeapon;
        }
    }

    private void UnequipWeapon()
    {
        _inputController.OnAttack -= Weapon.Attack;
        _currentSlot.OnSlotCleared -= UnequipWeapon;
        Destroy(Weapon.gameObject);
        Weapon = null;
        _currentSlot = null;
    }
}