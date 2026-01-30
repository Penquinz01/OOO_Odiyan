using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerInput _playerInput;
    private CharacterController _characterController;
    private PlayerMovement _playerMovement;
    [SerializeField]private float _speed;

    public float Speed
    {
        get => _speed;
        private set => _speed = value;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        _playerInput = new PlayerInput();
        _characterController = GetComponent<CharacterController>();
        _playerMovement = new PlayerMovement(_characterController,this,_playerInput);
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
