using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }

    public event EventHandler OnHotbar_1_Performed;
    public event EventHandler OnHotbar_2_Performed;
    public event EventHandler OnHotbar_3_Performed;
    public event EventHandler OnHotbar_4_Performed;
    public event EventHandler OnPausePerformed;
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

        inputActions.UI.Hotbar_1.performed += Hotbar_1_Performed;
        inputActions.UI.Hotbar_2.performed += Hotbar_2_Performed;
        inputActions.UI.Hotbar_3.performed += Hotbar_3_Performed;
        inputActions.UI.Hotbar_4.performed += Hotbar_4_Performed;
        inputActions.UI.Pause.performed += Pause_Performed;
    }

    private void Pause_Performed(InputAction.CallbackContext context)
        => OnPausePerformed?.Invoke(this, EventArgs.Empty);

    private void Hotbar_1_Performed(InputAction.CallbackContext context)
        => OnHotbar_1_Performed?.Invoke(this, EventArgs.Empty);

    private void Hotbar_2_Performed(InputAction.CallbackContext context)
        => OnHotbar_2_Performed?.Invoke(this, EventArgs.Empty);

    private void Hotbar_3_Performed(InputAction.CallbackContext context)
        => OnHotbar_3_Performed?.Invoke(this, EventArgs.Empty);

    private void Hotbar_4_Performed(InputAction.CallbackContext context)
        => OnHotbar_4_Performed?.Invoke(this, EventArgs.Empty);

    private void Reload_Performed(InputAction.CallbackContext context)
        => OnReloadPerformed?.Invoke(this, EventArgs.Empty);

    private void MouseLeftClick_Performed(InputAction.CallbackContext context)
        => OnMouseLeftClick?.Invoke(this, new OnMouseLeftClickEventArgs(true));

    private void MouseLeftClick_Canceled(InputAction.CallbackContext context)
        => OnMouseLeftClick?.Invoke(this, new OnMouseLeftClickEventArgs(false));

    public Vector2 GetMovementInput() => inputActions.Player.Movement.ReadValue<Vector2>();

    public Vector2 GetMovementInputNormalized() => GetMovementInput().normalized;

    private void OnEnable() => inputActions.Enable();

    private void OnDisable() => inputActions.Disable();

    private void OnDestroy() => inputActions.Dispose();
}
