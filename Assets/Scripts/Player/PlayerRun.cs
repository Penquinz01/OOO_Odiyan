using UnityEngine;

public class PlayerRun:States
{
    private PlayerAnimation _playerAnimation;
    private PlayerMovement _playerMovement;
    private PlayerMovement _playerMove;
    private Player _player;
    private PlayerStateMachine _playerStateMachine;
    private PlayerInput _playerInput;
    
    private static int RunAnimationId = Animator.StringToHash("Running");
    
    public PlayerRun(PlayerStateMachine playerStateMachine,PlayerMovement playerMovement,Player player,PlayerAnimation playerAnimation,PlayerInput playerInput)
    {
        _playerStateMachine = playerStateMachine;
        _playerMovement = playerMovement;
        _player = player;
        _playerAnimation = playerAnimation;
        _playerInput = playerInput;
    }
    public override void EnterState()
    {
        _playerAnimation.ChangeAnimation(RunAnimationId);
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
    }
}
