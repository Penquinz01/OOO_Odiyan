using Unity.Mathematics.Geometry;
using UnityEngine;

public class PlayerMovement
{
    private CharacterController _characterController;
    private Player _player;
    private PlayerInput _playerInput;
    public float _speed { get;private set; }
    private Vector3 _moveInput;
    private float _turnSpeed = 360f;
    private float _gravity;
    private float _sprintSpeed;
    private float _crouchSpeed;
    private Camera _mainCamera;
    private float turnSmoothVelocity;
    private float turnSmoothTime = 0.1f;
    private AudioSource _audioSource;
    [SerializeField] private AudioClip _walkSound;
    [SerializeField] private AudioClip _runSound;
    

    public PlayerMovement(CharacterController characterController, Player player, PlayerInput playerInput)
    {
        _characterController = characterController;
        _player = player;
        _playerInput = playerInput;
        _speed = _player.Speed;
        _turnSpeed = _player.TurnSpeed;
        _gravity = _player.Gravity;
        _sprintSpeed = _player.SprintSpeed;
        _crouchSpeed = _player.CrouchSpeed;
        _mainCamera = Camera.main;
        _audioSource = _player.AudioSource;
        _walkSound = _player.WalkSound;
        _runSound = _player.RunSound;
    }

    public void Move()
    {
        float yVelocity = _characterController.velocity.y + _gravity * Time.deltaTime;
        if (_characterController.isGrounded)
        {
            yVelocity = 0;
        }

        _moveInput = new Vector3(_playerInput._moveInput.x, 0, _playerInput._moveInput.y);
        float speed;
        speed = _playerInput.IsSprinting ? _sprintSpeed: _speed;
        if (_audioSource != null) {        
            if (!_audioSource.isPlaying && _moveInput.magnitude > 0.1f && !_playerInput.IsCrouching)
            {
                _audioSource.clip = _playerInput.IsSprinting ? _runSound : _walkSound;
                _audioSource.Play();
            }
            else if (_moveInput.magnitude < 0.1f || (!_playerInput.IsSprinting && _audioSource.clip == _runSound) || (_playerInput.IsSprinting && _audioSource.clip == _walkSound) || _playerInput.IsCrouching)
            {
                _audioSource.Stop();
            }
        }

        if (_playerInput.IsCrouching)
        {
            speed = _crouchSpeed;
        }
        float angle = Mathf.Atan2(_moveInput.x, _moveInput.z) * Mathf.Rad2Deg + _mainCamera.transform.eulerAngles.y;
        float targetAngle = Mathf.SmoothDampAngle(_player.transform.eulerAngles.y, angle, ref turnSmoothVelocity,
            turnSmoothTime);
        //Quaternion targetRotation = Quaternion.LookRotation(_moveInput);
        //_player.transform.rotation = Quaternion.RotateTowards(_player.transform.rotation, targetRotation, _turnSpeed * Time.deltaTime);
        _player.transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
        _characterController.Move(_player.transform.forward * speed *Time.deltaTime  * _moveInput.magnitude+ Vector3.up * yVelocity*Time.deltaTime);
    }

    public float GetVelocity()
    {
        return _characterController.velocity.magnitude;
    }

    public void ChangeSpeed(float _walkSpeed, float _runSpeed)
    {
        _speed = _walkSpeed;
        _sprintSpeed = _runSpeed;
    }
    public void ResetSpeed()
    {
        _speed = _player.Speed;
        _sprintSpeed = _player.SprintSpeed;
    }
}
