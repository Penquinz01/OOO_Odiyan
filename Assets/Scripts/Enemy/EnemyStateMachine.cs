using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine
{
    public States CurrentState{get; private set;}

    public Idle _idle{get; private set;}
    public Chasing _chase{get; private set;}
    public Pattroling _pattrol{get; private set;}
    private Enemy _enemy;
    public EnemyStateMachine(Enemy enemy,NavMeshAgent navMeshAgent,EnemyPathManager pathManager)
    {
        _enemy = enemy;
        _idle = new Idle(this,enemy);
        _chase = new Chasing(this,enemy,pathManager);
        _pattrol = new Pattroling(this, enemy, pathManager);
        
        SwitchState(_idle);
        
    }

    public void UpdateState()
    {
        CurrentState?.UpdateState();
    }
    public void SwitchState(States nextState)
    {
        if (CurrentState != null)
        {
            CurrentState.ExitState();
        }
        
        CurrentState = nextState;
        CurrentState.EnterState();
    }
    
    
}
