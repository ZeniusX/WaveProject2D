using UnityEngine;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }

    private GameInputActions inputActions;

    private void Awake()
    {
        Instance = this;

        inputActions = new GameInputActions();
    }

    private void Update()
    {
        Debug.Log(GetMovementInputNormalized());
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
