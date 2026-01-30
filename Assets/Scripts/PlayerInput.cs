using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput
{
    MainControls _mainControls;
    public Vector2 _moveInput{get; private set;}
    

    public PlayerInput()
    {
        _mainControls = new MainControls();
        EnableControls();
    }

    public void EnableControls()
    {
        _mainControls.Enable();
        _mainControls.Player.Move.performed += Move;
        _mainControls.Player.Move.canceled += MoveCancel;
    }

    public void DisableControls()
    {
        _mainControls.Player.Move.performed -= Move;
        _mainControls.Player.Move.canceled -= MoveCancel;
    }

    private void Move(InputAction.CallbackContext ctx)
    {
        _moveInput = ctx.ReadValue<Vector2>();
    }

    private void MoveCancel(InputAction.CallbackContext cxt)
    {
        _moveInput = Vector2.zero;
    }
}
