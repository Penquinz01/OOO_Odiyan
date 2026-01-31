using UnityEngine;

public class BullWalk:States
{
    private Bull _bull;
    private BullStateMachine _stateMachine;
    private Animator _animator;
    private PlayerMovement _playerMovement;
    private PlayerInput _playerInput;
    
    private static int WalkAnimationId = Animator.StringToHash("Walk");
    
    public BullWalk(BullStateMachine _stateMachine,Bull _bull,Animator animator,PlayerMovement playerMovement,PlayerInput playerInput)
    {
        this._bull = _bull;
        this._stateMachine = _stateMachine;
        _animator = animator;
        _playerMovement = playerMovement;
        _playerInput = playerInput;
    }
    public override void EnterState()
    {
        _animator.CrossFade(WalkAnimationId,0.1f,0);
    }

    public override void ExitState()
    {
        
    }

    public override void UpdateState()
    {
        if(_playerInput.IsSprinting)
        {
            _stateMachine.SwitchState(_stateMachine._run);
        }
        else if(_playerMovement.GetVelocity() <0.01f)
        {
            _stateMachine.SwitchState(_stateMachine._idle);
        }
    }
}
