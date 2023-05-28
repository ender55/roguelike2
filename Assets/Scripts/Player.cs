using System;
using UnityEngine;

public class Player : Unit, IUpgradeCollector, IWeaponCollector
{
    private InputController _inputController;
    
    [SerializeField] private GameObject weapon; //todo: deal with it
    [SerializeField] private Transform weaponSlot;

    [SerializeField] private UpgradeInventory upgradeInventory;
    [SerializeField] private WeaponInventory weaponInventory;

    public UpgradeInventory UpgradeInventory => upgradeInventory;
    public WeaponInventory WeaponInventory => weaponInventory;

    public Weapon Weapon { get; set; } 

    private void Awake()
    {
        _inputController = gameObject.AddComponent<InputController>();
        upgradeInventory = new UpgradeInventory(10);
        weaponInventory = new WeaponInventory(4);
        //weapon = Instantiate(weapon, weaponSlot);
        //Weapon = weapon.GetComponent(typeof(Weapon)) as Weapon;
    }

    private void OnEnable()
    {
        _inputController.OnMove += movement.Move;
        _inputController.OnLook += LookAt;
        if (Weapon != null)
        {
            _inputController.OnAttack += Weapon.Attack;
        }

        _inputController.OnWeaponChoose += ChooseWeapon;
    }

    private void OnDisable()
    {
        _inputController.OnMove -= movement.Move;
        _inputController.OnLook -= LookAt;
        if (Weapon != null)
        {
            _inputController.OnAttack -= Weapon.Attack;
        }
        
        _inputController.OnWeaponChoose -= ChooseWeapon;
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

    private void ChooseWeapon(int weaponNumber) //todo: check this method
    {
        var chosenWeapon = weaponInventory.GetInventorySlots()[weaponNumber].Item as InventoryWeapon;
        if (chosenWeapon != null && chosenWeapon.Weapon.TryGetComponent(out Weapon weapon))
        {
            if (Weapon != null)
            {
                _inputController.OnAttack -= Weapon.Attack;
                Destroy(Weapon.gameObject);
            }

            this.weapon = Instantiate(chosenWeapon.Weapon, weaponSlot);
            Weapon = this.weapon.GetComponent(typeof(Weapon)) as Weapon;
            _inputController.OnAttack += Weapon.Attack;
        }
    }
}