using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine
{
    private Enemy _enemy;
    private NavMeshAgent _agent;
    private Animator _animator;
    
    public States CurrentState { get; private set; }
    
    // States
    public EnemyIdle _idle { get; private set; }
    public EnemyPatrolling _patrol { get; private set; }
    public EnemyChasing _chase { get; private set; }
    public EnemyAttacking _attack { get; private set; }
    
    public EnemyStateMachine(Enemy enemy, NavMeshAgent agent, Animator animator)
    {
        _enemy = enemy;
        _agent = agent;
        _animator = animator;
        
        // Initialize all states
        _idle = new EnemyIdle(this, _enemy, _agent, _animator);
        _patrol = new EnemyPatrolling(this, _enemy, _agent, _animator);
        _chase = new EnemyChasing(this, _enemy, _agent, _animator);
        _attack = new EnemyAttacking(this, _enemy, _agent, _animator);
    }
    
    public void UpdateState()
    {
        CurrentState?.UpdateState();
    }
    
    public void FixedUpdateState()
    {
        CurrentState?.FixedUpdateState();
    }
    
    public void SwitchState(States newState)
    {
        if (CurrentState != null)
        {
            CurrentState.ExitState();
        }
        CurrentState = newState;
        CurrentState?.EnterState();
    }
}
