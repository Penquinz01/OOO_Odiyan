using UnityEngine;

public class PlayerStateMachine
{
    public States CurrentState { get; private set; }
    
    public PlayerIdle _idle { get; private set; }
    public PlayerWalk _walk { get; private set; }
    public PlayerRun _run { get; private set; }
    public PlayerCrouchWalking _crouchWalking { get; private set; }
    
    private Player _player;
    private PlayerInput _playerInput;
    private PlayerMovement _playerMovement;
    private PlayerAnimation _playerAnimation;
    public PlayerStateMachine(Player player,PlayerInput playerInput,PlayerMovement playerMovement,PlayerAnimation playerAnimation)
    {
        _idle = new PlayerIdle(this,playerMovement, player,playerAnimation,playerInput);
        _walk = new PlayerWalk(this,playerMovement, player,playerAnimation,playerInput);
        _run = new PlayerRun(this, playerMovement, player, playerAnimation, playerInput);
        _crouchWalking = new PlayerCrouchWalking(this,playerMovement, player,playerAnimation,playerInput);
        
        SwitchState(_idle);
    }
    
    public void SwitchState(States newState)
    {
        if (CurrentState != null)
        {
            CurrentState.ExitState();
        }
        CurrentState = newState;
        CurrentState.EnterState();
    }
    
    public void UpdateState()
    {
        CurrentState?.UpdateState();
    }
    
}
