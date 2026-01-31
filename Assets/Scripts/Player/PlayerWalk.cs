using UnityEngine;

public class PlayerWalk:States
{
    private PlayerAnimation _playerAnimation;
    private PlayerMovement _playerMovement;
    private PlayerMovement _playerMove;
    private Player _player;
    private PlayerStateMachine _playerStateMachine;
    private PlayerInput _playerInput;
    
    private static int WalkAnimationId = Animator.StringToHash("Walking");

    public PlayerWalk(PlayerStateMachine playerStateMachine,PlayerMovement playerMovement,Player player,PlayerAnimation playerAnimation,PlayerInput playerInput)
    {
        _playerStateMachine = playerStateMachine;
        _playerMovement = playerMovement;
        _player = player;
        _playerAnimation = playerAnimation;
        _playerInput = playerInput;
    }
    public override void EnterState()
    {
        _playerAnimation.ChangeAnimation(WalkAnimationId);
    }

    public override void ExitState()
    {
        
    }

    public override void UpdateState()
    {
        if (_playerInput._moveInput == Vector2.zero)
        {
            _playerStateMachine.SwitchState(_playerStateMachine._idle);
        }

        if (_playerInput.IsCrouching)
        {
            _playerStateMachine.SwitchState(_playerStateMachine._crouchWalking);
        }
        else if(_playerMovement.GetVelocity() > 5f)
        {
            _playerStateMachine.SwitchState(_playerStateMachine._run);
        }
    }
}
