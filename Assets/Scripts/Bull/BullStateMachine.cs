using UnityEngine;

public class BullStateMachine
{
    private Bull _bull;
    public States CurrentState{get; private set;}
    
    public BullIdle _idle{get; private set;}
    public BullWalk _walk{get; private set;}
    public BullGallop _run{get; private set;}
    public BullAttack _attack{get; private set;}
    
    public BullStateMachine(Bull _bull ,Animator animator,PlayerInput playerInput,PlayerMovement playerMovement)
    {
        _idle = new BullIdle(this,_bull,animator,playerMovement,playerInput);
        _walk = new BullWalk(this, _bull, animator, playerMovement, playerInput);
        _run = new BullGallop(this, _bull, animator, playerMovement, playerInput);
        _attack = new BullAttack(this, _bull, animator, playerMovement, playerInput);
        EventManager.OnAttack += Attack;
    }
    public void UpdateState()
    {
        CurrentState.UpdateState();
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

    private void Attack()
    {
        SwitchState(_attack);
    }
}
