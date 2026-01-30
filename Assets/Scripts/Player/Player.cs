using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(CapsuleCollider))]
public class Player : MonoBehaviour
{
    private PlayerInput _playerInput;
    private CharacterController _characterController;
    private PlayerMovement _playerMovement;
    private PlayerTransformation _playerTransformation;
    private Collider _collider;
    [SerializeField]private float _speed;
    [SerializeField] private GameObject _t2;
    [SerializeField] private float _turnSpeed = 360;
    public float Speed
    {
        get => _speed;
        private set => _speed = value;
    }

    public float TurnSpeed
    {
        get => _turnSpeed;
        private set => _turnSpeed = value;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _playerInput = new PlayerInput();
        _characterController = GetComponent<CharacterController>();
        _playerMovement = new PlayerMovement(_characterController,this,_playerInput);
        _playerTransformation = new PlayerTransformation(this, _t2,_collider);
        
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _playerMovement.Move();
    }
}
