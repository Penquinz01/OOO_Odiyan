using UnityEngine;

public class PlayerMovement
{
    private CharacterController _characterController;
    private Player _player;
    private PlayerInput _playerInput;
    private float _speed;
    private Vector2 _moveInput;
    private float _turnSmoothVelocity = 0.25f;

    public PlayerMovement(CharacterController characterController, Player player, PlayerInput playerInput)
    {
        _characterController = characterController;
        _player = player;
        _playerInput = playerInput;
    }

    public void Move()
    {
        _moveInput = _playerInput._moveInput;
        if (_moveInput == Vector2.zero)
        {
            return;
        }
        if (_turnSmoothVelocity > 1)
        {
            _turnSmoothVelocity = 0 ;
        }

        _player.transform.rotation = Quaternion.Slerp(_player.transform.rotation,Quaternion.LookRotation(new Vector3(_moveInput.x, 0, _moveInput.y)),_turnSmoothVelocity);
        _characterController.Move(_player.transform.forward * 10 *Time.deltaTime );
    }
}
