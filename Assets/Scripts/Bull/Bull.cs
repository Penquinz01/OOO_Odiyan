using System;
using System.Collections;
using UnityEngine;

public class Bull : MonoBehaviour
{
    [SerializeField] private float _walkSpeed = 9f;
    [SerializeField] private float _runSpeed = 14f;
    private BullStateMachine _stateMachine;
    [SerializeField] private Player _player;
    private PlayerMovement _playerMovement;
    [SerializeField] private float _timer = 1f;
    private PlayerTransformation _playerTransformation;
    private PlayerInput _playerInput;
    public bool CurrentlyActive { get; private set; } = false;

    public float WalkSpeed
    {
        get { return _walkSpeed; }
        private set { _walkSpeed = value; }
    }
    public float RunSpeed
    {
        get { return _runSpeed; }
        private set { _runSpeed = value; }
    }

    private void Awake()
    {
        _playerMovement = _player._playerMovement;
        _playerInput = _player._playerInput;
        _playerTransformation = _player._playerTransformation;
        _stateMachine = new BullStateMachine(this, GetComponent<Animator>(), _playerInput, _playerMovement);
        
    }

    private void OnEnable()
    {
        CurrentlyActive = true;
        _stateMachine.SwitchState(_stateMachine._idle);
        StartCoroutine(EndOfBull());
    }
    private void OnDisable()
    {
        CurrentlyActive = false;
        _playerTransformation?.Transform();
        _playerMovement?.ResetSpeed();
    }

    private void Update()
    {
        if (!CurrentlyActive)
        {
            return;
        }
        _stateMachine.UpdateState();
    }

    private IEnumerator EndOfBull()
    {
        yield return new WaitForSeconds(_timer);
        gameObject.SetActive(false);
    }
}
