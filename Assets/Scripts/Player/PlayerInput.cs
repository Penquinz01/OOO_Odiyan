using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlayerInput
{
    MainControls _mainControls;
    public Vector2 _moveInput{get; private set;}
    public bool IsSprinting{get; private set;}

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
        _mainControls.Player.Switch.started += Transform;
        _mainControls.Player.Sprint.started += SprintStart;
        _mainControls.Player.Sprint.canceled += SprintCancel;
    }
    

    public void DisableControls()
    {
        _mainControls.Player.Move.performed -= Move;
        _mainControls.Player.Move.canceled -= MoveCancel;
        _mainControls.Player.Switch.started -= Transform;
        _mainControls.Player.Sprint.started -= SprintStart;
        _mainControls.Player.Sprint.canceled -= SprintCancel;
        _mainControls.Disable();
    }

    private void SprintStart(InputAction.CallbackContext cxt)
    {
        IsSprinting = true;
    }

    private void SprintCancel(InputAction.CallbackContext cxt)
    {
        IsSprinting = false;
    }
    private void Move(InputAction.CallbackContext ctx)
    {
        _moveInput = ctx.ReadValue<Vector2>();
    }

    private void MoveCancel(InputAction.CallbackContext cxt)
    {
        _moveInput = Vector2.zero;
    }

    private void Transform(InputAction.CallbackContext cxt)
    {
        EventManager.Instance.InvokeTransformEvent();
    }
    
}
