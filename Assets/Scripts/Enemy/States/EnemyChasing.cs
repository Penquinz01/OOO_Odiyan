using UnityEngine;
using UnityEngine.AI;

public class EnemyChasing : States
{
    private EnemyStateMachine _stateMachine;
    private Enemy _enemy;
    private NavMeshAgent _agent;
    private Animator _animator;
    
    private float _attackRange = 3f;
    private float _chaseRange = 20f;
    private float _updatePathInterval = 0.5f;
    private float _pathUpdateTimer;
    
    private static readonly int RunAnimationId = Animator.StringToHash("Run");

    public EnemyChasing(EnemyStateMachine stateMachine, Enemy enemy, NavMeshAgent agent, Animator animator)
    {
        _stateMachine = stateMachine;
        _enemy = enemy;
        _agent = agent;
        _animator = animator;
    }

    public override void EnterState()
    {
        if (Player.Instance == null)
        {
            _stateMachine.SwitchState(_stateMachine._idle);
            return;
        }
        
        _animator?.CrossFade(RunAnimationId, 0.1f, 0);
        _agent.isStopped = false;
        _agent.stoppingDistance = _attackRange;
        _pathUpdateTimer = 0f;
    }

    public override void ExitState()
    {
        _agent.stoppingDistance = 0f;
    }

    public override void UpdateState()
    {
        if (_enemy == null) return;
        
        // Check if player is destroyed
        if (Player.Instance == null)
        {
            _stateMachine.SwitchState(_stateMachine._idle);
            return;
        }
        
        float distanceToPlayer = Vector3.Distance(_enemy.transform.position, Player.Instance.transform.position);
        
        // If player is too far, go back to patrol
        if (distanceToPlayer > _chaseRange)
        {
            _stateMachine.SwitchState(_stateMachine._idle);
            return;
        }
        
        // If player is in attack range, switch to attack
        if (distanceToPlayer <= _attackRange)
        {
            _stateMachine.SwitchState(_stateMachine._attack);
            return;
        }
        
        // Update path periodically
        _pathUpdateTimer += Time.deltaTime;
        if (_pathUpdateTimer >= _updatePathInterval)
        {
            _pathUpdateTimer = 0f;
            _agent.SetDestination(Player.Instance.transform.position);
        }
        
        // Look at player
        Vector3 lookDirection = (Player.Instance.transform.position - _enemy.transform.position).normalized;
        lookDirection.y = 0;
        if (lookDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
            _enemy.transform.rotation = Quaternion.Slerp(_enemy.transform.rotation, targetRotation, Time.deltaTime * 5f);
        }
    }
}
