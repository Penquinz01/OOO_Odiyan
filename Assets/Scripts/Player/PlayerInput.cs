using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlayerInput
{
    MainControls _mainControls;
    public Vector2 _moveInput{get; private set;}
    public bool IsSprinting{get; private set;}
    public bool IsCrouching {get; private set;}
    

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
        _mainControls.Player.Crouch.started += ToggleCrouch;
        _mainControls.Player.Attack.started += AttackTrigger;
    }
    

    public void DisableControls()
    {
        _mainControls.Player.Move.performed -= Move;
        _mainControls.Player.Move.canceled -= MoveCancel;
        _mainControls.Player.Switch.started -= Transform;
        _mainControls.Player.Sprint.started -= SprintStart;
        _mainControls.Player.Sprint.canceled -= SprintCancel;
        _mainControls.Player.Crouch.started -= ToggleCrouch;
        _mainControls.Player.Attack.started -= AttackTrigger;
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
        if (Player.Instance._isBullNow)
        {
            return;
        }
        EventManager.Instance.InvokeTransformEvent();
    }
    private void ToggleCrouch(InputAction.CallbackContext cxt)
    {
        IsCrouching = !IsCrouching;
    }

    private void AttackTrigger(InputAction.CallbackContext cxt)
    {
        if (!Player.Instance._isBullNow)
        {
            return;
        }
        EventManager.Instance.InvokeOnAttackEvent();
    }
    
}
