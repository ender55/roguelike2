﻿using TNRD;
using UnityEngine;

public class InventoryUIController : MonoBehaviour
{
    [SerializeField] private SerializableInterface<IInputHandler> inputHandler; //todo: inject
    [SerializeField] private GameObject weaponInventory;

    private InputController _inputController;

    private void Start()
    {
        weaponInventory.SetActive(false);
        _inputController = inputHandler.Value.InputController;
        _inputController.OnInventoryOpen += Activate;
        _inputController.OnInventoryClose += Deactivate;
        _inputController.OnMenuClose += Deactivate;
    }

    private void OnDisable()
    {
        _inputController.OnInventoryOpen -= Activate;
        _inputController.OnInventoryClose -= Deactivate;
        _inputController.OnMenuClose -= Deactivate;
    }

    private void Activate()
    {
        weaponInventory.SetActive(true);
        _inputController.SetActionMap("UI");
    }

    private void Deactivate()
    {
        weaponInventory.SetActive(false);
        _inputController.SetActionMap("Gameplay");
    }
}
