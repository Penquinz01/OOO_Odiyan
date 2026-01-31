using UnityEngine;

public class PlayerCrouchWalking:States
{
    private PlayerAnimation _playerAnimation;
    private PlayerMovement _playerMovement;
    private PlayerMovement _playerMove;
    private Player _player;
    private PlayerStateMachine _playerStateMachine;
    private PlayerInput _playerInput;

    private static int CrouchWalkingAnimationId = Animator.StringToHash("Crouched Walking");
    public PlayerCrouchWalking(PlayerStateMachine playerStateMachine,PlayerMovement playerMovement,Player player,PlayerAnimation playerAnimation,PlayerInput playerInput)
    {
        _playerStateMachine = playerStateMachine;
        _playerMovement = playerMovement;
        _player = player;
        _playerAnimation = playerAnimation;
        _playerInput = playerInput;
    }
    public override void EnterState()
    {
        _playerAnimation.ChangeAnimation(CrouchWalkingAnimationId);
    }

    public override void ExitState()
    {
        
    }

    public override void UpdateState()
    {
        if (!_playerInput.IsCrouching || _playerInput._moveInput == Vector2.zero)
        {
            _playerStateMachine.SwitchState(_playerStateMachine._idle);
        }
        
    }
}
