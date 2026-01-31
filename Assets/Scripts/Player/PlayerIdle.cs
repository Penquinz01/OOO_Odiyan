using UnityEngine;

public class PlayerIdle:States
{
    private PlayerAnimation _playerAnimation;
    private PlayerMovement _playerMovement;
    private PlayerMovement _playerMove;
    private Player _player;
    private PlayerStateMachine _playerStateMachine;
    private PlayerInput _playerInput;
    
    static int IdleAnimationId = Animator.StringToHash("Idle");
    public PlayerIdle(PlayerStateMachine playerStateMachine,PlayerMovement playerMovement,Player player,PlayerAnimation playerAnimation,PlayerInput playerInput)
    {
        _playerStateMachine = playerStateMachine;
        _playerMovement = playerMovement;
        _player = player;
        _playerAnimation = playerAnimation;
        _playerInput = playerInput;
    }
    public override void EnterState()
    {
        _playerAnimation.ChangeAnimation(IdleAnimationId);
    }

    public override void ExitState()
    {
        
    }

    public override void UpdateState()
    {
        if(_playerInput._moveInput != Vector2.zero && _playerMovement.GetVelocity() > 0.1f)
        {
            _playerStateMachine.SwitchState(_playerStateMachine._walk);
        }
    }
}
