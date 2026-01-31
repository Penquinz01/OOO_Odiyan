using System;
using UnityEngine;


public class Player : MonoBehaviour
{
    public static Player Instance;
    [SerializeField] private float _sprintSpeed = 8f;
    [SerializeField]private float crouchSpeed = 3f;
    private PlayerInput _playerInput;
    [SerializeField]private CharacterController _characterController;
    [SerializeField] private CapsuleCollider _collider;
    [SerializeField]private SkinnedMeshRenderer _playerRenderer;
    private PlayerMovement _playerMovement;
    private PlayerTransformation _playerTransformation;
    [SerializeField]private float _speed;
    [SerializeField] private GameObject _t2;
    [SerializeField] private float _turnSpeed = 360;
    [SerializeField]private float _gravity = -9.81f;
    private PlayerStateMachine _playerStateMachine;
    private Animator _playerAnimator;
    private PlayerAnimation _playerAnimation;
    
    public float Gravity
    {
        get => _gravity;
        private set => _gravity = value;
    }
    public float Speed
    {
        get => _speed;
        private set => _speed = value;
    }
    public float SprintSpeed
    {
        get => _sprintSpeed;
        private set => _sprintSpeed = value;
    }

    public float CrouchSpeed
    {
        get => crouchSpeed;
        private set => crouchSpeed = value;
    }

    public float TurnSpeed
    {
        get => _turnSpeed;
        private set => _turnSpeed = value;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        Instance = this;
        _playerInput = new PlayerInput();
        _playerMovement = new PlayerMovement(_characterController,this,_playerInput);
        _playerTransformation = new PlayerTransformation(this, _t2, _collider,_playerRenderer);
        _playerAnimator = GetComponent<Animator>();
        _playerAnimation = new PlayerAnimation(_playerAnimator);
        _playerStateMachine = new PlayerStateMachine(this,_playerInput,_playerMovement,_playerAnimation);
    }


    void Update()
    {
        _playerMovement.Move();
        _playerStateMachine.UpdateState();
    }
}
