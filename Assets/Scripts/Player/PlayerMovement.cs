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

    public PlayerMovement(CharacterController characterController, Player player, PlayerInput playerInput)
    {
        _characterController = characterController;
        _player = player;
        _playerInput = playerInput;
        _speed = _player.Speed;
        _turnSpeed = _player.TurnSpeed;
        _gravity = _player.Gravity;
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
        
        Quaternion targetRotation = Quaternion.LookRotation(_moveInput);
        _player.transform.rotation = Quaternion.RotateTowards(_player.transform.rotation, targetRotation, _turnSpeed * Time.deltaTime);
        _characterController.Move(_player.transform.forward * _speed *Time.deltaTime + Vector3.up * yVelocity);
    }
}
