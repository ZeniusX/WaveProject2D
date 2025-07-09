using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }

    public event EventHandler OnReloadPerformed;
    public event EventHandler<OnMouseLeftClickEventArgs> OnMouseLeftClick;
    public class OnMouseLeftClickEventArgs : EventArgs
    {
        public bool isPerformed;

        public OnMouseLeftClickEventArgs(bool isPerformed)
        {
            this.isPerformed = isPerformed;
        }
    }

    private GameInputActions inputActions;

    private void Awake()
    {
        Instance = this;

        inputActions = new GameInputActions();

        SubscribeInputActions();
    }

    private void SubscribeInputActions()
    {
        inputActions.Player.MouseLeftClick.performed += MouseLeftClick_Performed;
        inputActions.Player.MouseLeftClick.canceled += MouseLeftClick_Canceled;
        inputActions.Player.Reload.performed += Reload_Performed;
    }

    private void Reload_Performed(InputAction.CallbackContext context)
    {
        OnReloadPerformed?.Invoke(this, EventArgs.Empty);
    }

    private void MouseLeftClick_Performed(InputAction.CallbackContext context)
    {
        OnMouseLeftClick?.Invoke(this, new OnMouseLeftClickEventArgs(true));
    }

    private void MouseLeftClick_Canceled(InputAction.CallbackContext context)
    {
        OnMouseLeftClick?.Invoke(this, new OnMouseLeftClickEventArgs(false));
    }

    public Vector2 GetMovementInput()
    {
        return inputActions.Player.Movement.ReadValue<Vector2>();
    }

    public Vector2 GetMovementInputNormalized()
    {
        return GetMovementInput().normalized;
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
    }

    private void OnDisable()
    {
        inputActions.Player.Disable();
    }

    private void OnDestroy()
    {
        inputActions.Dispose();
    }
}
