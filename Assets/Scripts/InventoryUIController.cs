using System;
using TNRD;
using UnityEngine;

public class InventoryUIController : MonoBehaviour
{
    [SerializeField] private SerializableInterface<IInputHandler> inputHandler;
    [SerializeField] private GameObject weaponInventory;

    private InputController _inputController;

    private void Start()
    {
        weaponInventory.SetActive(false);
        _inputController = inputHandler.Value.InputController;
        _inputController.OnInventoryOpen += Activate;
        _inputController.OnInventoryClose += Deactivate;
    }

    private void OnDisable()
    {
        _inputController.OnInventoryOpen -= Activate;
        _inputController.OnInventoryClose -= Deactivate;
    }

    private void Activate()
    {
        weaponInventory.SetActive(true);
    }

    private void Deactivate()
    {
        weaponInventory.SetActive(false);
    }
}
