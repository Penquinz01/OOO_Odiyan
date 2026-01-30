using UnityEngine;

public class PlayerMovement
{
    private CharacterController _characterController;
    private Player _player;
    private PlayerInput _playerInput;
    private float _speed;
    private Vector2 _moveInput;

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
        Debug.Log(_moveInput);
        float angle = Mathf.Atan2(_moveInput.y, _moveInput.x) * Mathf.Rad2Deg - 90.00f;
        _player.transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
        _characterController.Move(_player.transform.forward * 10 *Time.deltaTime );
    }
}
