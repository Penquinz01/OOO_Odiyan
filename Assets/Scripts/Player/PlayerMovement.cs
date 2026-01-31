using UnityEngine;

public class PlayerMovement
{
    private CharacterController _characterController;
    private Player _player;
    private PlayerInput _playerInput;
    private float _speed;
    private Vector3 _moveInput;
    private float _turnSpeed = 360f;
    private float _gravity;
    private float _sprintSpeed;
    private float _crouchSpeed;

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
    }

    public void Move()
    {
        float yVelocity = _characterController.velocity.y + _gravity * Time.deltaTime;
        if (_characterController.isGrounded)
        {
            yVelocity = 0;
        }
        _moveInput = new Vector3(_playerInput._moveInput.x,0,_playerInput._moveInput.y);
        if (_moveInput == Vector3.zero)
        {
            return;
        }

        float speed;
        speed = _playerInput.IsSprinting ? _sprintSpeed: _speed;
        Quaternion targetRotation = Quaternion.LookRotation(_moveInput);
        _player.transform.rotation = Quaternion.RotateTowards(_player.transform.rotation, targetRotation, _turnSpeed * Time.deltaTime);
        _characterController.Move(_player.transform.forward * speed *Time.deltaTime + Vector3.up * yVelocity);
    }

    public float GetVelocity()
    {
        return _characterController.velocity.magnitude;
    }
}
