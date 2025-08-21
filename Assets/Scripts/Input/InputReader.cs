using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static InputActions;

[CreateAssetMenu(fileName = "New Input Reader", menuName = "Input/Input Reader")]

public class InputReader : ScriptableObject, IPlayerActions
{

    public event Action<bool> PrimaryFireEvent;
    public event Action<Vector2> MoveEvent;
    public Vector2 AimPosition { get; private set; }

    private InputActions inputActions;
    void OnEnable()
    {
        if (inputActions == null)
        {
            inputActions = new InputActions();
            inputActions.Player.SetCallbacks(this);
        }

        inputActions.Player.Enable();
    }


    public void OnMove(InputAction.CallbackContext context)
    {

        MoveEvent?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnPrimaryFire(InputAction.CallbackContext context)
    {

        if (context.performed)
        {

            PrimaryFireEvent?.Invoke(true);
        }
        if (context.canceled)
        {
            PrimaryFireEvent?.Invoke(false);
        }
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        AimPosition = context.ReadValue<Vector2>();
    }

    void OnDisable()
    {
        inputActions.Player.Disable();

    }
}
