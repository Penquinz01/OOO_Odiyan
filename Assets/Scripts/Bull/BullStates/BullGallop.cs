using UnityEngine;

public class BullGallop:States
{
    private Bull _bull;
    private BullStateMachine _stateMachine;
    private Animator _animator;
    private PlayerMovement _playerMovement;
    private PlayerInput _playerInput;
    
    private static int GallopAnimationId = Animator.StringToHash("Gallop");
    
    public BullGallop(BullStateMachine _stateMachine,Bull _bull,Animator animator,PlayerMovement playerMovement,PlayerInput playerInput)
    {
        this._bull = _bull;
        this._stateMachine = _stateMachine;
        _animator = animator;
        _playerMovement = playerMovement;
        _playerInput = playerInput;
    }
    public override void EnterState()
    {
        _animator.CrossFade(GallopAnimationId, 0.1f,0);
    }

    public override void ExitState()
    {
       
    }

    public override void UpdateState()
    {
        if (_playerMovement.GetVelocity() <= _bull.WalkSpeed)
        {
            _stateMachine.SwitchState(_stateMachine._walk);
        }
    }
}
