using UnityEngine;

public class BullIdle:States
{
    private Bull _bull;
    private BullStateMachine _stateMachine;
    private Animator _animator;
    private PlayerMovement _playerMovement;
    private PlayerInput _playerInput;
    
    private static int IdleAnimationId = Animator.StringToHash("Idle");
    
    public BullIdle(BullStateMachine _stateMachine,Bull _bull,Animator animator,PlayerMovement playerMovement,PlayerInput playerInput)
    {
        this._bull = _bull;
        this._stateMachine = _stateMachine;
        _animator = animator;
        _playerMovement = playerMovement;
        _playerInput = playerInput;
    }
    public override void EnterState()
    {
        _animator.CrossFade(IdleAnimationId,0.1f,0);   
    }

    public override void ExitState()
    {
        
    }

    public override void UpdateState()
    {
        if(_playerMovement.GetVelocity() > 0 && _playerMovement.GetVelocity() <= _bull.WalkSpeed)
        {
            _stateMachine.SwitchState(_stateMachine._walk);
        }
        else if(_playerMovement.GetVelocity() > _bull.WalkSpeed)
        {
            _stateMachine.SwitchState(_stateMachine._run);
        }
    }
}
