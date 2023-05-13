using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour, GameInput.IGameplayActions, GameInput.IUIActions
{
    private GameInput _gameInput;
    
    public event Action<Vector2> OnMove;
    public event Action<Vector2> OnLook;
    public event Action OnAttack;

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
}
