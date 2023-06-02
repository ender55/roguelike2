using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour, GameInput.IGameplayActions, GameInput.IUIActions
{
    private GameInput _gameInput;
    
    public event Action<Vector2> OnMove;
    public event Action<Vector2> OnLook;
    public event Action OnAttack;
    public event Action<int> OnWeaponChoose;
    public event Action OnInventoryOpen;
    public event Action OnInventoryClose;

    private void Awake()
    {
        _gameInput = new GameInput();
        _gameInput.Gameplay.SetCallbacks(this);
        _gameInput.UI.SetCallbacks(this);
        SetActionMap("Gameplay");
    }

    private void OnEnable()
    {
        _gameInput.Enable();
    }

    private void OnDisable()
    {
        _gameInput.Disable();
    }

    public void SetActionMap(string newInputActionMap)
    {
        foreach (InputActionMap inputActionMap in _gameInput.asset.actionMaps)
        {
            if(inputActionMap.name == newInputActionMap)
            {
                inputActionMap.Enable();
            }
            else
            {
                inputActionMap.Disable();
            }
        }
    }

    public string GetCurrentActionMap()
    {
        foreach (InputActionMap inputActionMap in _gameInput.asset.actionMaps)
        {
            if(inputActionMap.enabled)
            {
                return inputActionMap.name;
            }
        }
        return null;
    }
    
    void GameInput.IGameplayActions.OnMove(InputAction.CallbackContext context)
    {
        OnMove?.Invoke(context.ReadValue<Vector2>());
    }

    void GameInput.IGameplayActions.OnLook(InputAction.CallbackContext context)
    {
        if (Camera.main != null)
        {
            OnLook?.Invoke(Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>()));
        }
    }

    void GameInput.IGameplayActions.OnAttack(InputAction.CallbackContext context)
    {
        OnAttack?.Invoke();
    }

    public void OnChooseFirstWeapon(InputAction.CallbackContext context)
    {
        OnWeaponChoose?.Invoke(0);
    }

    public void OnChooseSecondWeapon(InputAction.CallbackContext context)
    {
        OnWeaponChoose?.Invoke(1);
    }

    public void OnChooseThirdWeapon(InputAction.CallbackContext context)
    {
        OnWeaponChoose?.Invoke(2);
    }

    public void OnChooseFourthWeapon(InputAction.CallbackContext context)
    {
        OnWeaponChoose?.Invoke(3);
    }

    public void OnOpenInventory(InputAction.CallbackContext context)
    {
        SetActionMap("UI");
        OnInventoryOpen?.Invoke();
    }

    public void OnCloseInventory(InputAction.CallbackContext context)
    {
        SetActionMap("Gameplay");
        OnInventoryClose?.Invoke();
    }
}
