using UnityEngine;
using UnityEngine.AI;

public class EnemyIdle : States
{
    private EnemyStateMachine _stateMachine;
    private Enemy _enemy;
    private NavMeshAgent _agent;
    private Animator _animator;
    private LayerMask _playerMask;
    
    private float _idleTimer;
    private float _idleDuration = 2f;
    private float _detectionRadius = 15f;
    
    private static readonly int IdleAnimationId = Animator.StringToHash("Idle");

    public EnemyIdle(EnemyStateMachine stateMachine, Enemy enemy, NavMeshAgent agent, Animator animator)
    {
        _stateMachine = stateMachine;
        _enemy = enemy;
        _agent = agent;
        _animator = animator;
        _playerMask = LayerMask.GetMask("Player");
    }

    public override void EnterState()
    {
        _idleTimer = 0f;
        _agent.isStopped = true;
        _animator?.CrossFade(IdleAnimationId, 0.1f, 0);
    }

    public override void ExitState()
    {
        _agent.isStopped = false;
    }

    public override void UpdateState()
    {
        if (_enemy == null) return;
        
        if (DetectPlayer())
        {
            _stateMachine.SwitchState(_stateMachine._chase);
            return;
        }
        
        _idleTimer += Time.deltaTime;
        if (_idleTimer >= _idleDuration)
        {
            _stateMachine.SwitchState(_stateMachine._patrol);
        }
    }

    private bool DetectPlayer()
    {
        if (Player.Instance == null) return false;
        
        Collider[] cols = Physics.OverlapSphere(_enemy.transform.position, _detectionRadius, _playerMask);
        foreach (var col in cols)
        {
            Vector3 directionToPlayer = (col.transform.position - _enemy.transform.position).normalized;
            if (Physics.Raycast(_enemy.transform.position, directionToPlayer, out RaycastHit hit, _detectionRadius))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    return true;
                }
            }
        }
        return false;
    }
}
