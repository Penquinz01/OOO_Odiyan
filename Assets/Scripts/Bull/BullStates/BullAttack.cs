using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BullAttack:States
{
    private Bull _bull;
    private BullStateMachine _stateMachine;
    private Animator _animator;
    private PlayerMovement _playerMovement;
    private PlayerInput _playerInput;
    
    private static int AttackAnimationId = Animator.StringToHash("Attack");
    
    public BullAttack(BullStateMachine _stateMachine,Bull _bull,Animator animator,PlayerMovement playerMovement,PlayerInput playerInput)
    {
        this._bull = _bull;
        this._stateMachine = _stateMachine;
        _animator = animator;
        _playerMovement = playerMovement;
        _playerInput = playerInput;
    }
    
    public override void EnterState()
    {
        _bull.StartCoroutine(EndAttack());
    }

    public override void ExitState()
    {
        
    }

    public override void UpdateState()
    {
        
    }

    private IEnumerator EndAttack()
    {
        _animator.CrossFade(AttackAnimationId, 0.1f,0);
        yield return new WaitForSeconds(31f);
        _stateMachine.SwitchState(_stateMachine._idle);
    }
}
