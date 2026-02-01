using UnityEngine;

public class PlayerDeath: States
{
    private PlayerAnimation _playerAnimation;
    private PlayerMovement _playerMovement;
    private PlayerMovement _playerMove;
    private Player _player;
    private PlayerStateMachine _playerStateMachine;
    private PlayerInput _playerInput;
    
    static int DeathAnimationId = Animator.StringToHash("Death");
    public PlayerDeath(PlayerStateMachine playerStateMachine,PlayerMovement playerMovement,Player player,PlayerAnimation playerAnimation,PlayerInput playerInput)
    {
        _playerStateMachine = playerStateMachine;
        _playerMovement = playerMovement;
        _player = player;
        _playerAnimation = playerAnimation;
        _playerInput = playerInput;
    }
    public override void EnterState()
    {
        _player.transform.position -=Vector3.up* 2f;
        _playerAnimation.ChangeAnimation(DeathAnimationId);
    }

    public override void ExitState()
    {
        
    }

    public override void UpdateState()
    {
        
    }
}
