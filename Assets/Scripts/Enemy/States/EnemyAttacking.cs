using UnityEngine;
using UnityEngine.AI;

public class EnemyAttacking : States
{
    private EnemyStateMachine _stateMachine;
    private Enemy _enemy;
    private NavMeshAgent _agent;
    private Animator _animator;
    
    private float _attackCooldown = 1.5f;
    private float _attackTimer;
    private float _attackRange = 3.5f;
    private bool _hasAttacked;
    
    private static readonly int AttackAnimationId = Animator.StringToHash("Attack");

    public EnemyAttacking(EnemyStateMachine stateMachine, Enemy enemy, NavMeshAgent agent, Animator animator)
    {
        _stateMachine = stateMachine;
        _enemy = enemy;
        _agent = agent;
        _animator = animator;
    }

    public override void EnterState()
    {
        _agent.isStopped = true;
        _attackTimer = 0f;
        _hasAttacked = false;
        _animator?.CrossFade(AttackAnimationId, 0.1f, 0);
    }

    public override void ExitState()
    {
        _agent.isStopped = false;
    }

    public override void UpdateState()
    {
        if (_enemy == null) return;
        
        _attackTimer += Time.deltaTime;
        
        // Deal damage at specific point in animation (e.g., 0.5 seconds in)
        if (!_hasAttacked && _attackTimer >= 0.5f)
        {
            _hasAttacked = true;
            PerformAttack();
        }
        
        // After attack cooldown, decide next state
        if (_attackTimer >= _attackCooldown)
        {
            // Check if player still in range
            if (Player.Instance == null)
            {
                _stateMachine.SwitchState(_stateMachine._idle);
                return;
            }
            
            float distanceToPlayer = Vector3.Distance(_enemy.transform.position, Player.Instance.transform.position);
            
            if (distanceToPlayer <= _attackRange)
            {
                // Attack again
                EnterState();
            }
            else
            {
                // Chase player
                _stateMachine.SwitchState(_stateMachine._chase);
            }
        }
        
        // Face the player during attack
        if (Player.Instance != null)
        {
            Vector3 lookDirection = (Player.Instance.transform.position - _enemy.transform.position).normalized;
            lookDirection.y = 0;
            if (lookDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
                _enemy.transform.rotation = Quaternion.Slerp(_enemy.transform.rotation, targetRotation, Time.deltaTime * 10f);
            }
        }
    }

    private void PerformAttack()
    {
        if (Player.Instance == null) return;
        
        float distanceToPlayer = Vector3.Distance(_enemy.transform.position, Player.Instance.transform.position);
        if (distanceToPlayer <= _attackRange)
        {
            Player.Instance.Die(); // Deal 10 damage to player
        }
    }
}
