using System;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    private Animator _animator;
    private MainControls _mainControls;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _mainControls = new MainControls();
        _mainControls.Enable();
        _mainControls.UI.Enter.started += SkiptoNext;
    }
    private void SkiptoNext(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        _animator.CrossFade("Disappear",0f,0);
    }

    private void OnDestroy()
    {
        _mainControls.UI.Enter.started -= SkiptoNext;
        _mainControls.Disable();
    }
    public void Destroy() => Destroy(gameObject);
}
